using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.VFX;

public abstract class Character : MonoBehaviour
{
    protected CharacterManager cm = CharacterManager.Instance;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform attackTransform;
    protected SpriteRenderer spriteRender;
    public StatData statData;
    public Image HpImage;
    public VisualEffect[] effect;

    public ISharedSkill SelectedSharedSkill;
    protected bool attackCombo = false;
    protected bool hit = false;

    private Vector3 curPosition;
    private Vector3 prevPosition;
    private const float WALL_MARGIN = 0.35f;    //대쉬시 벽에 부딪히면 0.35만큼 거리를 두고 정지
    bool alive;

    public Dictionary<string, IState<Character>> dicState = new Dictionary<string, IState<Character>>();
    public StateMachine<Character> sm;
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = statData.curMovementSpeed;

        dicState.Add("Attack", new AttackState());
        dicState.Add("Move", new MoveState());
        dicState.Add("Idle", new IdleState());
        dicState.Add("Dash", new DashState());
        dicState.Add("SharedSkill", new SharedSkillState());
        sm = new StateMachine<Character>(this, dicState["Idle"]);

        alive = true;
        
        agent.updateRotation = false;
        prevPosition = transform.position;
    }
    public void SetSharedSkill(ISharedSkill skill)
    {
        SelectedSharedSkill = skill;
    }

    public virtual void BasicAttack()
    {

    }
    public virtual void SkillAttack1()
    {

    }
    public virtual void SkillAttack2()
    {

    }
    public virtual void SharedSkill()
    {
        
    }
    public virtual void ResetCombo() 
    {

    }

    public void SelectSharedSkill(string skillName)
    {
        switch (skillName)
        {
            case "Fireball":
                SelectedSharedSkill = new DaggerThrow();
                break;
            case "IceBlast":
                SelectedSharedSkill = new IceBlastSkill();
                break;
            case "LightningStrike":
                SelectedSharedSkill = new LightningStrikeSkill();
                break;
            default:
                Debug.LogWarning($"알 수 없는 스킬 이름: {skillName}");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttack"))
        {
            statData.ModifyCurrentHp(-10f);
            
        }
    }
    #region 대쉬관련
    public void Dash()
    {
        //네비메쉬 정지후 끄기
        agent.SetDestination(transform.position);
        
        //대쉬 목표지 정하기
        Vector3 dashDestination = CalculateDashDestination();
        if (dashDestination == Vector3.zero) return;            //대쉬목표가이상하면 정지
        FlipSpriteByMousePosition();
        //대쉬 켜기
        animator.SetBool("Dash", true);
        Debug.Log(dashDestination);
        StartCoroutine(DashCoroutine(transform.position, dashDestination));
    }
    
    private Vector3 CalculateDashDestination()
    {
        Vector3 curPosition = transform.position;
        Vector3 mousePosition = MousePosition();
        if (mousePosition == null) return Vector3.zero;

        //대쉬 방향
        Vector3 dashDestDir = (mousePosition - curPosition).normalized;

        //대쉬가 벽에 막히게
        if (Physics.Raycast(curPosition, dashDestDir, out RaycastHit dashHit, statData.curDashPower, 1 << LayerMask.NameToLayer("Wall")))
        {
            return curPosition + dashDestDir * (dashHit.distance - WALL_MARGIN);
        }
        return curPosition + dashDestDir * statData.curDashPower;
    }
    //대쉬 실행
    private IEnumerator DashCoroutine(Vector3 curPosition, Vector3 dashDestination)
    {
        agent.enabled = false;
        float duration = 1f/ statData.curDashSpeed;
        float time = 0;
        while (time <= duration)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(curPosition, dashDestination, time/duration);

            yield return null;
        }
        transform.position = dashDestination;
        animator.SetBool("Dash", false);
        agent.enabled = true;
    }
    #endregion

    #region 마우스 위치 인식
    public Vector3 MousePosition()
    {
        Vector3 mousePosition;
        if (IsMouseOverLayer("Ground", out RaycastHit hit))
        {
            mousePosition = hit.point; // Ground 레이어와 충돌한 지점 반환
        }
        else
        {
            mousePosition = transform.position; // 현재 오브젝트 위치 반환
        }
        return mousePosition;
    }

    public bool CheckMousePosition()
    {
        return IsMouseOverLayer("Ground", out _); // Ground 레이어와 충돌 여부만 반환
    }

    // 공통적으로 사용하는 메서드 추가
    private bool IsMouseOverLayer(string layerName, out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer(layerName);
        return Physics.Raycast(ray, out hit, 100, layerMask);
    }
    #endregion

    #region 방향회전들
    //이동시 좌우전환
    public void FlipSprite()
    {
        // 현재 위치와 이전 위치를 비교
        curPosition = transform.position;

        // 캐릭터가 오른쪽으로 이동하면 flipX를 false로, 왼쪽으로 이동하면 true로 설정
        if (curPosition.x > prevPosition.x)
        {
            spriteRender.flipX = true; // 오른쪽을 보고 있도록 설정
        }
        else if (curPosition.x < prevPosition.x)
        {
            spriteRender.flipX = false; // 왼쪽을 보고 있도록 설정
        }

        // 이전 위치를 현재 위치로 업데이트
        prevPosition = curPosition;
    }
    //공격시 이동때와는 다른 방법으로 좌우변환해야함
    public void FlipSpriteByMousePosition()
    {
        if (MousePosition().x < transform.position.x)
        {
            spriteRender.flipX = false;
        }
        else
        {
            spriteRender.flipX = true;
        }
    }

    //공격콜라이더 방향 전환
    public void SetAttackDirection()
    {
        Vector3 dir = (MousePosition() - transform.position).normalized;
        dir.y = 0;
        attackTransform.rotation = Quaternion.LookRotation(dir);
    }
    #endregion

    public void ComboEnable()
    {
        
        attackCombo = true;
    }

    public void ComboDisable()
    {
        
        effect[0].Play();
        effect[1].Play();
        attackCombo = false;
    }
}
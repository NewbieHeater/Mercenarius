using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    private Camera mainCamera;
    public ISharedSkill SelectedSharedSkill;
    protected bool attackCombo = false;
    protected bool hit = false;

    private Vector3 curPosition;
    private Vector3 prevPosition;
    bool alive;

    public Dictionary<string, IState<Character>> dicState = new Dictionary<string, IState<Character>>();
    public StateMachine<Character> sm;
    protected virtual void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        //HpImage = GameManager
        agent.speed = statData.curMovementSpeed;
        mainCamera = Camera.main;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttack"))
        {
            statData.ModifyCurrentHp(-10f);
            
        }
    }
    #region 대쉬관련
    
    #endregion

    #region 마우스 위치 인식
    /// <summary>
    /// 마우스 위치로부터 Ground 레이어에 충돌한 지점을 반환합니다.
    /// 충돌에 성공하면 true, 실패하면 false를 반환하며 out 매개변수에 결과값을 전달합니다.
    /// </summary>
    public bool TryGetGroundPosition(out Vector3 groundPos)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        int layerMask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            groundPos = hit.point;
            return true;
        }
        groundPos = Vector3.zero; // 필요에 따라 다른 기본값 설정 가능
        return false;
    }

    /// <summary>
    /// 마우스가 땅 위에 있는지 여부만 체크하는 함수.
    /// </summary>
    public bool IsMouseOverGround()
    {
        return TryGetGroundPosition(out _);
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
        if (TryGetGroundPosition(out Vector3 targetPos))
        {
            spriteRender.flipX = (targetPos.x >= transform.position.x);
        }
    }
    Vector3 dir;
    //공격콜라이더 방향 전환
    public void SetAttackDirection()
    {
        if (TryGetGroundPosition(out Vector3 targetPos))
        {
            dir = (targetPos - transform.position).normalized;
        }
        
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
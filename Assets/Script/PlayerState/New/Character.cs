using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    protected CharacterManager cm = CharacterManager.Instance;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform attackTransform;
    protected SpriteRenderer spriteRender;
    public StatData statData;


    protected bool hit = false;
    private bool isFacingRight = true;
    private Vector3 curPosition;
    private Vector3 prevPosition;
    //대쉬시 벽에 부딪히면 0.35만큼 거리를 두고 정지
    private const float WALL_MARGIN = 0.35f;

    bool alive;

    protected virtual void Start()
    {
        alive = true;
        animator = GetComponentInChildren<Animator>();
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        prevPosition = transform.position;
    }

    public abstract void Attack();

    public void Move()
    {
        agent.speed = statData.curMovementSpeed;
        agent.SetDestination(MousePosition());
        animator.SetBool("Move", true);
    }
    public void Hit()
    {
        if (alive)
        {
            statData.ModifyCurrentHp(4f);
            hit = false;
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

    #region 아마도 모든 캐릭터에 필요할거 같아서 만든부분
    protected Vector3 MousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            return hit.point;
        }  
        else
            return transform.position;
    }
    protected void OnAttackAnimeEnd()
    {
        animator.SetBool("Attack", false);
    }
    //공격콜라이더 방향 전환
    protected void SetAttackDirection()
    {
        Vector3 dir = (MousePosition() - transform.position).normalized;
        dir.y = 0;
        attackTransform.rotation = Quaternion.LookRotation(dir);
    }
    #endregion

    //이동시 좌우전환
    protected void FlipSprite()
    {
        // 현재 위치와 이전 위치를 비교
        curPosition = transform.position;

        // 캐릭터가 오른쪽으로 이동하면 flipX를 false로, 왼쪽으로 이동하면 true로 설정
        if (curPosition.x > prevPosition.x && !isFacingRight)
        {
            spriteRender.flipX = false; // 오른쪽을 보고 있도록 설정
            isFacingRight = true;
        }
        else if (curPosition.x < prevPosition.x && isFacingRight)
        {
            spriteRender.flipX = true; // 왼쪽을 보고 있도록 설정
            isFacingRight = false;
        }

        // 이전 위치를 현재 위치로 업데이트
        prevPosition = curPosition;
    }

    
}
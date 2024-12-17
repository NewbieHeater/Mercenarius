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
    //�뽬�� ���� �ε����� 0.35��ŭ �Ÿ��� �ΰ� ����
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

    #region �뽬����
    public void Dash()
    {
        //�׺�޽� ������ ����
        agent.SetDestination(transform.position);
        

        //�뽬 ��ǥ�� ���ϱ�
        Vector3 dashDestination = CalculateDashDestination();
        if (dashDestination == Vector3.zero) return;            //�뽬��ǥ���̻��ϸ� ����

        //�뽬 �ѱ�
        animator.SetBool("Dash", true);
        Debug.Log(dashDestination);
        StartCoroutine(DashCoroutine(transform.position, dashDestination));
    }
    
    private Vector3 CalculateDashDestination()
    {
        Vector3 curPosition = transform.position;
        Vector3 mousePosition = MousePosition();
        if (mousePosition == null) return Vector3.zero;

        //�뽬 ����
        Vector3 dashDestDir = (mousePosition - curPosition).normalized;

        //�뽬�� ���� ������
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

    #region �Ƹ��� ��� ĳ���Ϳ� �ʿ��Ұ� ���Ƽ� ����κ�
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
    //�����ݶ��̴� ���� ��ȯ
    protected void SetAttackDirection()
    {
        Vector3 dir = (MousePosition() - transform.position).normalized;
        dir.y = 0;
        attackTransform.rotation = Quaternion.LookRotation(dir);
    }
    #endregion

    //�̵��� �¿���ȯ
    protected void FlipSprite()
    {
        // ���� ��ġ�� ���� ��ġ�� ��
        curPosition = transform.position;

        // ĳ���Ͱ� ���������� �̵��ϸ� flipX�� false��, �������� �̵��ϸ� true�� ����
        if (curPosition.x > prevPosition.x && !isFacingRight)
        {
            spriteRender.flipX = false; // �������� ���� �ֵ��� ����
            isFacingRight = true;
        }
        else if (curPosition.x < prevPosition.x && isFacingRight)
        {
            spriteRender.flipX = true; // ������ ���� �ֵ��� ����
            isFacingRight = false;
        }

        // ���� ��ġ�� ���� ��ġ�� ������Ʈ
        prevPosition = curPosition;
    }

    
}
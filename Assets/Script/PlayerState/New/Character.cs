using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public abstract class Character : MonoBehaviour
{
    protected CharacterManager cm = CharacterManager.Instance;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform attackTransform;
    protected SpriteRenderer spriteRender;
    public StatData statData;


    protected bool hit = false;
    private Vector3 curPosition;
    private Vector3 prevPosition;
    private const float WALL_MARGIN = 0.35f;    //�뽬�� ���� �ε����� 0.35��ŭ �Ÿ��� �ΰ� ����
    bool alive;

    public Dictionary<string, IState<Character>> dicState = new Dictionary<string, IState<Character>>();
    public StateMachine<Character> sm;
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = statData.curMovementSpeed;

        dicState.Add("Attack", new AttackState());
        //dicState.Add("SkillAttack1", new SkillAttack1State());
        //dicState.Add("SkillAttack2", new SkillAttack2State());
        dicState.Add("Move", new MoveState());
        dicState.Add("Idle", new IdleState());
        dicState.Add("Dash", new DashState());
        sm = new StateMachine<Character>(this, dicState["Idle"]);

        alive = true;
        animator = GetComponentInChildren<Animator>();
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        prevPosition = transform.position;
    }

    protected virtual void BasicAttack()
    {

    }
    protected virtual void SkillAttack1()
    {

    }
    protected virtual void SkillAttack2()
    {

    }

    #region �뽬����
    public void Dash()
    {
        //�׺�޽� ������ ����
        agent.SetDestination(transform.position);
        
        //�뽬 ��ǥ�� ���ϱ�
        Vector3 dashDestination = CalculateDashDestination();
        if (dashDestination == Vector3.zero) return;            //�뽬��ǥ���̻��ϸ� ����
        FlipSpriteByMousePosition();
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
    //�뽬 ����
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
    public Vector3 MousePosition()
    {
        Vector3 mousePosition;
        if (IsMouseOverLayer("Ground", out RaycastHit hit))
        {
            mousePosition = hit.point; // Ground ���̾�� �浹�� ���� ��ȯ
        }
        else
        {
            mousePosition = transform.position; // ���� ������Ʈ ��ġ ��ȯ
        }
        return mousePosition;
    }

    public bool CheckMousePosition()
    {
        return IsMouseOverLayer("Ground", out _); // Ground ���̾�� �浹 ���θ� ��ȯ
    }

    // ���������� ����ϴ� �޼��� �߰�
    private bool IsMouseOverLayer(string layerName, out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer(layerName);
        return Physics.Raycast(ray, out hit, 100, layerMask);
    }

    protected void OnAttackAnimeEnd()
    {
        animator.SetBool("Attack", false);
    }

    //�����ݶ��̴� ���� ��ȯ
    public void SetAttackDirection()
    {
        Vector3 dir = (MousePosition() - transform.position).normalized;
        dir.y = 0;
        attackTransform.rotation = Quaternion.LookRotation(dir);
    }

    //�̵��� �¿���ȯ
    public void FlipSprite()
    {
        // ���� ��ġ�� ���� ��ġ�� ��
        curPosition = transform.position;

        // ĳ���Ͱ� ���������� �̵��ϸ� flipX�� false��, �������� �̵��ϸ� true�� ����
        if (curPosition.x > prevPosition.x)
        {
            spriteRender.flipX = false; // �������� ���� �ֵ��� ����
        }
        else if (curPosition.x < prevPosition.x)
        {
            spriteRender.flipX = true; // ������ ���� �ֵ��� ����
        }

        // ���� ��ġ�� ���� ��ġ�� ������Ʈ
        prevPosition = curPosition;
    }
    //���ݽ� �̵����ʹ� �ٸ� ������� �¿캯ȯ�ؾ���
    public void FlipSpriteByMousePosition()
    {
        if (MousePosition().x < transform.position.x)
        {
            spriteRender.flipX = true;
        }
        else
        {
            spriteRender.flipX = false;
        }
    }
    #endregion
}
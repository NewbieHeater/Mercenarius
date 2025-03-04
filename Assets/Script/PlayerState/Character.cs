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
    #region �뽬����
    
    #endregion

    #region ���콺 ��ġ �ν�
    /// <summary>
    /// ���콺 ��ġ�κ��� Ground ���̾ �浹�� ������ ��ȯ�մϴ�.
    /// �浹�� �����ϸ� true, �����ϸ� false�� ��ȯ�ϸ� out �Ű������� ������� �����մϴ�.
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
        groundPos = Vector3.zero; // �ʿ信 ���� �ٸ� �⺻�� ���� ����
        return false;
    }

    /// <summary>
    /// ���콺�� �� ���� �ִ��� ���θ� üũ�ϴ� �Լ�.
    /// </summary>
    public bool IsMouseOverGround()
    {
        return TryGetGroundPosition(out _);
    }
    #endregion

    #region ����ȸ����
    //�̵��� �¿���ȯ
    public void FlipSprite()
    {
        // ���� ��ġ�� ���� ��ġ�� ��
        curPosition = transform.position;

        // ĳ���Ͱ� ���������� �̵��ϸ� flipX�� false��, �������� �̵��ϸ� true�� ����
        if (curPosition.x > prevPosition.x)
        {
            spriteRender.flipX = true; // �������� ���� �ֵ��� ����
        }
        else if (curPosition.x < prevPosition.x)
        {
            spriteRender.flipX = false; // ������ ���� �ֵ��� ����
        }

        // ���� ��ġ�� ���� ��ġ�� ������Ʈ
        prevPosition = curPosition;
    }
    //���ݽ� �̵����ʹ� �ٸ� ������� �¿캯ȯ�ؾ���
    public void FlipSpriteByMousePosition()
    {
        if (TryGetGroundPosition(out Vector3 targetPos))
        {
            spriteRender.flipX = (targetPos.x >= transform.position.x);
        }
    }
    Vector3 dir;
    //�����ݶ��̴� ���� ��ȯ
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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.VFX;

public abstract class Character : MonoBehaviour
{
    protected CharacterManager cm = CharacterManager.Instance;
    public NavMeshAgent agent { get; private set; }
    public Animator animator { get; private set; }
    public Transform attackTransform;
    protected SpriteRenderer spriteRender;
    public StatData statData;
    public Image HpImage;
    public VisualEffect[] effect;
    protected Camera mainCamera;
    public ISharedSkill SelectedSharedSkill;
    public bool attackCombo = false;
    protected bool hit = false;
    public int attackComboValue = 0;

    // 최적화를 위한 OverlapSphere/Box 결과 배열
    protected Collider[] overlapResults = new Collider[50];
    protected int enemyLayerMask;
    protected Vector3 curPosition;
    protected Vector3 prevPosition;
    protected bool alive;
    public float dashCoolDown = 0f;
    public Dictionary<string, IState<Character>> dicState = new Dictionary<string, IState<Character>>();
    public StateMachine<Character> sm;

    // DrawAttackBox에서 사용할 코너 배열(매 호출 시 할당하지 않도록)
    private Vector3[] corners = new Vector3[8];

    #region 초기화 및 컴포넌트 캐싱
    protected virtual void OnEnable()
    {
        enemyLayerMask = LayerMask.GetMask("Enemy");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        agent.speed = statData.curMovementSpeed;
        mainCamera = Camera.main;

        dicState.Clear();
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
    #endregion

    #region 공용 메서드
    public void SetSharedSkill(ISharedSkill skill)
    {
        SelectedSharedSkill = skill;
    }

    public virtual void BasicAttack() { }
    public virtual void SkillAttack1() { }
    public virtual void SkillAttack2() { }
    public virtual void SharedSkill() { }
    public virtual void ResetCombo() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttack"))
        {
            statData.ModifyCurrentHp(-10f);
        }
    }
    #endregion

    #region 마우스 위치 및 방향 계산
    public bool TryGetGroundPosition(out Vector3 groundPos)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        int layerMask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            groundPos = hit.point;
            return true;
        }
        groundPos = Vector3.zero;
        return false;
    }

    public bool IsMouseOverGround() => TryGetGroundPosition(out _);

    // 공격 방향 계산: 캐릭터 위치에서 마우스 월드 좌표까지의 단위 벡터 (수평 유지)
    protected Vector3 GetAttackDirection()
    {
        if (TryGetGroundPosition(out Vector3 mousePos))
        {
            mousePos.y = transform.position.y;
            Vector3 dir = mousePos - transform.position;
            if (dir.sqrMagnitude > 0.001f)
                return dir.normalized;
        }
        return transform.forward;
    }
    #endregion

    #region 방향 전환
    public void FlipSprite()
    {
        curPosition = transform.position;
        if (curPosition.x > prevPosition.x)
            spriteRender.flipX = true;
        else if (curPosition.x < prevPosition.x)
            spriteRender.flipX = false;
        prevPosition = curPosition;
    }

    public void FlipSpriteByMousePosition()
    {
        if (TryGetGroundPosition(out Vector3 targetPos))
            spriteRender.flipX = (targetPos.x >= transform.position.x);
    }
    #endregion

    #region 공격 범위 및 판정
    // 반원 공격: origin을 중심으로 OverlapSphereNonAlloc을 사용하여, 공격 방향 내의 적에게 데미지 적용
    protected void PerformOptimizedHemisphereAttack(Vector3 origin, float radius, float damage, int hitCount)
    {
        FlipSpriteByMousePosition();
        int count = Physics.OverlapSphereNonAlloc(origin, radius, overlapResults, enemyLayerMask);
        Vector3 attackDirection = GetAttackDirection();
        for (int i = 0; i < count; i++)
        {
            Collider col = overlapResults[i];
            if (col == null) continue;
            Vector3 toTarget = (col.transform.position - origin).normalized;
            if (Vector3.Dot(attackDirection, toTarget) >= 0f)
            {
                EnemyGolemController enemy = col.GetComponent<EnemyGolemController>();
                if (enemy != null)
                {
                    for (int j = 0; j < hitCount; j++)
                        enemy.TakeDamage(damage);
                }
            }
        }
    }

    // 원형 공격: origin을 중심으로 OverlapSphereNonAlloc을 사용하여, 범위 내의 모든 적에게 데미지 적용
    protected void PerformOptimizedSphericalAttack(Vector3 origin, float radius, float damage, int hitCount)
    {
        FlipSpriteByMousePosition();
        int count = Physics.OverlapSphereNonAlloc(origin, radius, overlapResults, enemyLayerMask);
        for (int i = 0; i < count; i++)
        {
            Collider col = overlapResults[i];
            if (col == null) continue;
            EnemyGolemController enemy = col.GetComponent<EnemyGolemController>();
            if (enemy != null)
            {
                for (int j = 0; j < hitCount; j++)
                    enemy.TakeDamage(damage);
            }
        }
    }

    // 직육면체 공격: 공격자의 위치(origin)에서 공격 방향으로 halfExtents.z만큼 떨어진 center를 기준으로 OverlapBoxNonAlloc 사용
    protected void PerformOptimizedBoxAttackInFront(Vector3 origin, Vector3 halfExtents, float damage, int hitCount)
    {
        FlipSpriteByMousePosition();
        Vector3 attackDirection = GetAttackDirection();
        Vector3 center = origin + attackDirection * halfExtents.z;
        Quaternion orientation = Quaternion.LookRotation(attackDirection);
        DrawAttackBox(center, halfExtents, orientation, Color.green, 0.1f);
        Debug.Log($"Attack Box - halfExtents: {halfExtents}, center: {center}");
        int count = Physics.OverlapBoxNonAlloc(center, halfExtents, overlapResults, orientation, enemyLayerMask);
        for (int i = 0; i < count; i++)
        {
            Collider col = overlapResults[i];
            if (col == null) continue;
            EnemyGolemController enemy = col.GetComponent<EnemyGolemController>();
            if (enemy != null)
            {
                for (int j = 0; j < hitCount; j++)
                    enemy.TakeDamage(damage);
            }
        }
    }

    // 공격 범위를 시각화: 캐릭터 공격 범위를 Debug.DrawLine으로 그립니다.
    private void DrawAttackBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, Color color, float duration = 0f)
    {
        // 캐싱된 corners 배열을 사용하여 8개 코너 계산
        corners[0] = center + orientation * new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
        corners[1] = center + orientation * new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);
        corners[2] = center + orientation * new Vector3(halfExtents.x, -halfExtents.y, halfExtents.z);
        corners[3] = center + orientation * new Vector3(-halfExtents.x, -halfExtents.y, halfExtents.z);
        corners[4] = center + orientation * new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
        corners[5] = center + orientation * new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
        corners[6] = center + orientation * new Vector3(halfExtents.x, halfExtents.y, halfExtents.z);
        corners[7] = center + orientation * new Vector3(-halfExtents.x, halfExtents.y, halfExtents.z);

        // 밑면
        Debug.DrawLine(corners[0], corners[1], color, duration);
        Debug.DrawLine(corners[1], corners[2], color, duration);
        Debug.DrawLine(corners[2], corners[3], color, duration);
        Debug.DrawLine(corners[3], corners[0], color, duration);
        // 윗면
        Debug.DrawLine(corners[4], corners[5], color, duration);
        Debug.DrawLine(corners[5], corners[6], color, duration);
        Debug.DrawLine(corners[6], corners[7], color, duration);
        Debug.DrawLine(corners[7], corners[4], color, duration);
        // 수직선
        Debug.DrawLine(corners[0], corners[4], color, duration);
        Debug.DrawLine(corners[1], corners[5], color, duration);
        Debug.DrawLine(corners[2], corners[6], color, duration);
        Debug.DrawLine(corners[3], corners[7], color, duration);
    }
    #endregion

    #region 애니메이션 및 콤보 관리
    public void ComboEnable()
    {
        Debug.Log("attackEnter");
        
        
        BasicAttack();
        attackCombo = true;
    }

    public void ComboDisable()
    {
        attackCombo = false;
    }

    public bool nextAttack = false;
    public void ComboExit()
    {
        nextAttack = true;
    }

    public void AttackEnd()
    {
        sm.SetState(dicState["Idle"]);
        animator.SetBool("Attack", false);
        Debug.Log("attackEnd");
    }
    #endregion
}

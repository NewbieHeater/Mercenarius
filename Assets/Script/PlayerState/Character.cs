using System;
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
    public Transform attackHemisphereTransform;
    public Transform attackSphereTransform;
    public Transform attackBoxTransform;
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

    #region 아이템
    //public GenericItemEffect genericItemEffect { get; private set; }
    public event Action<List<EnemyGolemController>> OnAttack;
    // 피격 이벤트: 플레이어가 피해를 입을 때 호출, 피해량 인자 포함
    public event Action<float> OnHit;

    // 피해 처리 함수 (예시)
    public void OnHitExecute(float damage)
    {
        TakeDamage(damage);
        OnHit?.Invoke(damage);
    }
    public void TakeDamage(float damage)
    {
        statData.ModifyCurrentHp(-damage);
        HpImage.fillAmount = statData.curHp / statData.maxHp;
    }
    public void ApplyBleedingEffect(float value)
    {
        TakeDamage(value);
    }
    #endregion

    #region 초기화 및 컴포넌트 캐싱
    protected virtual void OnEnable()
    {
        //genericItemEffect = GetComponent<GenericItemEffect>();
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

    public virtual void BasicAttack() {  }
    public virtual void SkillAttack1() { }
    public virtual void SkillAttack2() { }
    public virtual void SharedSkill() { }
    public virtual void ResetCombo() { }

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
    public Vector3 GetAttackDirection()
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
    public bool bleed = false;

    // 반원 공격: origin을 중심으로 OverlapSphereNonAlloc을 사용하여, 공격 방향 내의 적에게 데미지 적용
    /// <summary>
    /// 반구 범위 내 적들을 반환합니다.
    /// </summary>
    public List<EnemyGolemController> GetEnemiesInHemisphere(Vector3 origin, float radius)
    {
        FlipSpriteByMousePosition();
        Vector3 attackDirection;
        List<EnemyGolemController> enemies = new List<EnemyGolemController>();
        int count = Physics.OverlapSphereNonAlloc(origin, radius, overlapResults, enemyLayerMask);
        if(TryGetGroundPosition(out attackDirection))
        {
            attackHemisphereTransform.localScale = new Vector3(radius, radius, radius);
            attackHemisphereTransform.LookAt(new Vector3(attackDirection.x, 0.5696364f, attackDirection.z));
            attackHemisphereTransform.gameObject.SetActive(true);
        }
        attackDirection = GetAttackDirection();
        for (int i = 0; i < count; i++)
        {
            Collider col = overlapResults[i];
            if (col == null)
                continue;
            Vector3 toTarget = (col.transform.position - origin).normalized;
            if (Vector3.Dot(attackDirection, toTarget) >= 0f)
            {
                EnemyGolemController enemy = col.GetComponent<EnemyGolemController>();
                if (enemy != null)
                    enemies.Add(enemy);
            }
        }
        OnAttack?.Invoke(enemies);
        return enemies;
    }

    /// <summary>
    /// 구 형태 범위 내 적들을 반환합니다.
    /// </summary>
    public List<EnemyGolemController> GetEnemiesInSphere(Vector3 origin, float radius)
    {
        FlipSpriteByMousePosition();
        List<EnemyGolemController> enemies = new List<EnemyGolemController>();
        int count = Physics.OverlapSphereNonAlloc(origin, radius, overlapResults, enemyLayerMask);
        attackSphereTransform.localScale = new Vector3(radius, radius, radius);
        attackSphereTransform.gameObject.SetActive(true);
        for (int i = 0; i < count; i++)
        {
            Collider col = overlapResults[i];
            if (col == null)
                continue;
            EnemyGolemController enemy = col.GetComponent<EnemyGolemController>();
            if (enemy != null)
                enemies.Add(enemy);
        }
        OnAttack?.Invoke(enemies);
        return enemies;
    }

    /// <summary>
    /// 직육면체 범위 내 적들을 반환합니다.
    /// </summary>
    public List<EnemyGolemController> GetEnemiesInBox(Vector3 center, Vector3 halfExtents, Quaternion orientation)
    {
        FlipSpriteByMousePosition();
        List<EnemyGolemController> enemies = new List<EnemyGolemController>();
        int count = Physics.OverlapBoxNonAlloc(center, halfExtents, overlapResults, orientation, enemyLayerMask);
        Vector3 attackDirection;
        if (TryGetGroundPosition(out attackDirection))
        {
            attackSphereTransform.localScale = new Vector3(halfExtents.x, 1, halfExtents.z);
            attackBoxTransform.LookAt(new Vector3(attackDirection.x, 0.5696364f, attackDirection.z));
            attackBoxTransform.gameObject.SetActive(true);
        }
        for (int i = 0; i < count; i++)
        {
            Collider col = overlapResults[i];
            if (col == null)
                continue;
            EnemyGolemController enemy = col.GetComponent<EnemyGolemController>();
            if (enemy != null)
                enemies.Add(enemy);
        }
        OnAttack?.Invoke(enemies);
        return enemies;
    }

    /// <summary>
    /// 리스트로 반환된 적들에게 데미지를 적용합니다.
    /// hitCount: 한 적에게 몇 번의 데미지를 줄 것인가 (중복 타격)
    /// </summary>
    public void DamageEnemies(List<EnemyGolemController> enemies, float damage, int hitCount)
    {
        foreach (var enemy in enemies)
        {
            for (int i = 0; i < hitCount; i++)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    #endregion

        #region 애니메이션 및 콤보 관리
    public void ComboEnable()
    {

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
        attackHemisphereTransform.gameObject.SetActive(false);
        attackSphereTransform.gameObject.SetActive(false);
        attackBoxTransform.gameObject.SetActive(false);
    }

    public void AttackEnd()
    {
        sm.SetState(dicState["Idle"]);
        animator.SetBool("Attack", false);
    }
    #endregion
}

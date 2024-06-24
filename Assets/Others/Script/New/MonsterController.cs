using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MonsterController : MonoBehaviour
{
    public enum MonsterState
    {
        Idle,
        Move,
        Attack,
        RangeAttack,
        SpecialAttack,
        Hit,
        Dead,
    }

    public bool isLive = false;
    public bool isHit = false;
    public bool MoveAble = true;

    public string enemytype = "Null";
    public float originalHealth { get; set; }
    public float health { get; set; }
    public float originalSpeed { get; set; }
    public float attackRange { get; set; }
    public float attackSpeed { get; set; }
    public float beforCastDelay { get; set; }
    public float CurSpeed { get; set; }

    public Transform AttackPoint;
    public SpriteRenderer sprite;
    public UnityEvent onPlayerDead;
    public Rigidbody enemyRb;//적 프리팹의 리지드바디
    public Rigidbody target;//플레이어 캐릭터의 리지드 바디
    public NavMeshAgent nav;
    public NavMeshObstacle navObs;
    public Animator anim;
    //public Transform MainBody;
    public UnitCode unitCode;
    public Stat stat;
    
    private Dictionary<MonsterState, IState<MonsterController>> dicState = new Dictionary<MonsterState, IState<MonsterController>>();
    private StateMachine<MonsterController> sm;
    private void Awake()
    {
        stat = new Stat();
        stat = stat.SetUnitStat(unitCode);
    }
    // Start is called before the first frame update
    void Start()
    {
        //GetComponentInParent
        //AttackPoint = transform.GetChild(0);
        sprite = GetComponentInChildren<SpriteRenderer>();
        nav = GetComponent<NavMeshAgent>();
        navObs = GetComponent<NavMeshObstacle>();
        anim = GetComponentInChildren<Animator>();
        nav.updateRotation = false; //회전막기
        //MainBody = transform.parent;
        enemyRb = GetComponent<Rigidbody>();
        //적 정보 받아오기
        enemytype = stat.name;
        originalHealth = stat.maxHp;
        health = originalHealth; //체력 초기화
        originalSpeed = stat.originalSpeed;
        attackRange = stat.AttackRange;
        attackSpeed = stat.AttackSpeed;
        beforCastDelay = stat.beforeCastDelay;
        CurSpeed = originalSpeed;
        

        IState<MonsterController> idle = gameObject.AddComponent<IdleState>();
        IState<MonsterController> move = gameObject.AddComponent<MoveState>();
        IState<MonsterController> attack = gameObject.AddComponent<AttackState>();
        IState<MonsterController> hit = gameObject.AddComponent<HitState>();
        IState<MonsterController> dead = gameObject.AddComponent<DeadState>();
        IState<MonsterController> rangeAttack = gameObject.AddComponent<RangeAttackState>();
        IState<MonsterController> specialAttack = gameObject.AddComponent<SpecialAttackState>();

        dicState.Add(MonsterState.Idle, idle);
        dicState.Add(MonsterState.Move, move);
        dicState.Add(MonsterState.Attack, attack);
        dicState.Add(MonsterState.Hit, hit);
        dicState.Add(MonsterState.Dead, dead);
        dicState.Add(MonsterState.RangeAttack, rangeAttack);
        dicState.Add(MonsterState.SpecialAttack, specialAttack);

        sm = new StateMachine<MonsterController>(this, dicState[MonsterState.Idle]);
        
    }

    void OnEnable()
    {
        MoveAble = true;
        target = GameManager.instance.player.GetComponent<Rigidbody>(); //프리팹 상태에서는 플레이어의 리지드바디를 못가지고 옴 그래서 활성화후 가져오기
        health = originalHealth; //재사용시 체력 초기화
        isLive = true;
    }

    void Update()
    {

        if (health <= 0) //만약 체력이 0이 아니라면
        {
            //onPlayerDead.Invoke();
            sm.SetState(dicState[MonsterState.Dead]);
            return;
        }
        
        float dist = Vector3.Distance(target.transform.position, transform.position);
        if (isHit == true)
        {
            sm.SetState(dicState[MonsterState.Hit]);
            return;
        }
            
        switch (enemytype)
        {
            case "Vengeful_Warrior":
                if (dist >= attackRange && MoveAble)
                {
                    sm.SetState(dicState[MonsterState.Move]);
                }
                else if (dist <= attackRange)
                {
                    sm.SetState(dicState[MonsterState.Attack]);
                }
                break;
            case "Grudge_Archer":
                if (dist >= attackRange && MoveAble)
                {
                    sm.SetState(dicState[MonsterState.Move]);
                }
                else if (dist <= attackRange)
                {
                    sm.SetState(dicState[MonsterState.RangeAttack]);
                }
                break;
            case "Slime":
                if (dist >= attackRange && MoveAble)
                {
                    sm.SetState(dicState[MonsterState.Move]);
                }
                else if (dist <= attackRange)
                {
                    sm.SetState(dicState[MonsterState.SpecialAttack]);
                }
                break;
        }
        sm.DoOperateUpdate();
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면 
    }

    public void AnimeEnded() //애니메이션 이벤트로 유닛의 애니메이션이 끝나면 발동
    {
        MoveAble = true;

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("PlayerAttack"))
        {
            isHit = true;
        }
    }
}

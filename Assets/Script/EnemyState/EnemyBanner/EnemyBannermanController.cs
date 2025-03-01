using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyBannermanController : MonoBehaviour
{
    public enum enemyBannermanState
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
    public float maxHealth { get; set; }
    public float curHealth { get; set; }
    public float originalSpeed { get; set; }
    public float attackRange { get; set; }
    public float attackSpeed { get; set; }
    public float beforCastDelay { get; set; }
    public float CurSpeed { get; set; }

    public Image Hpbar;
    public Transform AttackPoint;
    public UnityEvent onPlayerDead;
    public Rigidbody target;
    private SpriteRenderer sprite;
    private Rigidbody enemyRb;
    private NavMeshAgent nav;
    private Animator anim;
    //public Transform MainBody;
    public UnitCode unitCode;
    public Stat stat;

    private Dictionary<enemyBannermanState, IState<EnemyBannermanController>> dicState = new Dictionary<enemyBannermanState, IState<EnemyBannermanController>>();
    private StateMachine<EnemyBannermanController> stateMachineBannerman;
    private void Awake()
    {
        stat = new Stat();
        stat = stat.SetUnitStat(unitCode);
    }
    void Start()
    {
        //GetComponentInParent
        //AttackPoint = transform.GetChild(0);
        sprite = GetComponentInChildren<SpriteRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        nav.updateRotation = false;
        //MainBody = transform.parent;
        enemyRb = GetComponent<Rigidbody>();
        //적종류
        enemytype = stat.name;
        //체력
        maxHealth = stat.maxHp;
        curHealth = stat.curHp;
        maxHealth = 100f; 
        curHealth = 100f;
        //이동속도
        originalSpeed = stat.originalSpeed;
        CurSpeed = originalSpeed;
        //공격범위
        attackRange = stat.AttackRange;
        //공격속도
        attackSpeed = stat.AttackSpeed;
        //공격 딜레이
        beforCastDelay = stat.beforeCastDelay;

        IState<EnemyBannermanController> BannermanIdle = gameObject.AddComponent<IdleStateBannerman>();
        IState<EnemyBannermanController> BannermanMove = gameObject.AddComponent<MoveStateBannerman>();
        IState<EnemyBannermanController> BannermanCast = gameObject.AddComponent< CastingStateBannerman>();
        IState<EnemyBannermanController> BannermanHit = gameObject.AddComponent<HitStateBannerman>();
        IState<EnemyBannermanController> BannermanDead = gameObject.AddComponent<DeadStateBannerman>();
        //IState<EnemyBannermanController> BannermanSpecialAttack = gameObject.AddComponent<SpecialAttackStateBannerman>();

        dicState.Add(enemyBannermanState.Idle, BannermanIdle);
        dicState.Add(enemyBannermanState.Move, BannermanMove);
        dicState.Add(enemyBannermanState.Attack, BannermanCast);
        dicState.Add(enemyBannermanState.Hit, BannermanHit);
        dicState.Add(enemyBannermanState.Dead, BannermanDead);
        //dicState.Add(enemyBannermanState.SpecialAttack, BannermanSpecialAttack);

        stateMachineBannerman = new StateMachine<EnemyBannermanController>(this, dicState[enemyBannermanState.Idle]);

    }

    void OnEnable()
    {
        MoveAble = true;
        target = GameManager._instance.character.GetComponent<Rigidbody>();
        curHealth = 100f;
        isLive = true;
    }
    void Update()
    {
        if (curHealth <= 0)
        {
            //onPlayerDead.Invoke();
            stateMachineBannerman.SetState(dicState[enemyBannermanState.Dead]);
            return;
        }

        float dist = Vector3.Distance(target.transform.position, transform.position);
        /*
        if ()
        {
            stateMachineBannerman.SetState(dicState[enemyBannermanState.Idle]);

        }
        */

        switch (stateMachineBannerman.CurState)
        {
            case IdleStateBannerman:
                if (dist >= attackRange && anim.GetBool("Hit") == false && anim.GetBool("Attack") == false)
                {
                    stateMachineBannerman.SetState(dicState[enemyBannermanState.Move]);
                }
                if (dist <= attackRange && anim.GetBool("Hit") == false)
                {
                    stateMachineBannerman.SetState(dicState[enemyBannermanState.Attack]);
                }
                break;
            case MoveStateBannerman:
                if (dist >= attackRange && anim.GetBool("Hit") == false && anim.GetBool("Attack") == false)
                {
                    stateMachineBannerman.SetState(dicState[enemyBannermanState.Move]);
                }
                if (dist <= attackRange && anim.GetBool("Hit") == false)
                {
                    stateMachineBannerman.SetState(dicState[enemyBannermanState.Attack]);
                }
                if (anim.GetBool("Hit") == false && anim.GetBool("Attack") == false && anim.GetBool("Move") == false && anim.GetBool("Idle") == true)
                {
                    stateMachineBannerman.SetState(dicState[enemyBannermanState.Idle]);
                }
                break;
            case CastingStateBannerman:
                if (dist >= attackRange && MoveAble && anim.GetBool("Hit") == false && anim.GetBool("Attack") == false)
                {
                    stateMachineBannerman.SetState(dicState[enemyBannermanState.Move]);
                }
                else if (dist <= attackRange && anim.GetBool("Hit") == false)
                {
                    stateMachineBannerman.SetState(dicState[enemyBannermanState.Attack]);
                }
                if (anim.GetBool("Hit") == false && anim.GetBool("Attack") == false && anim.GetBool("Move") == false && anim.GetBool("Idle") == true)
                {
                    stateMachineBannerman.SetState(dicState[enemyBannermanState.Idle]);
                }
                break;
            case HitStateBannerman:
                if (dist >= attackRange && MoveAble && anim.GetBool("Hit") == false && anim.GetBool("Attack") == false)
                {
                    stateMachineBannerman.SetState(dicState[enemyBannermanState.Move]);
                }
                else if (dist <= attackRange && anim.GetBool("Hit") == false)
                {
                    stateMachineBannerman.SetState(dicState[enemyBannermanState.Attack]);
                }
                if (anim.GetBool("Hit") == false && anim.GetBool("Attack") == false && anim.GetBool("Move") == false && anim.GetBool("Idle") == true)
                {
                    stateMachineBannerman.SetState(dicState[enemyBannermanState.Idle]);
                }
                break;
        }
        stateMachineBannerman.DoOperateUpdate();
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();
    }

    public void AnimeEnded()
    {
        anim.SetBool("Attack", false);
        MoveAble = true;
        anim.SetBool("Hit", false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            stateMachineBannerman.SetState(dicState[enemyBannermanState.Hit]);

        }
    }
}

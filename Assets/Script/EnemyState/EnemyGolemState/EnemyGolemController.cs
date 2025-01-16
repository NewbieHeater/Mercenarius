using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyGolemController : MonoBehaviour
{
    public enum enemyGolemState
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
    public StatData stat;


    private Dictionary<enemyGolemState, IState<EnemyGolemController>> dicState = new Dictionary<enemyGolemState, IState<EnemyGolemController>>();
    private StateMachine<EnemyGolemController> stateMachineGolem;
    private void Awake()
    {
        
    }
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        nav.updateRotation = false; 
        //MainBody = transform.parent;
        enemyRb = GetComponent<Rigidbody>();
        //체력
        maxHealth = 100f;
        curHealth = 100f;
        //이동속도
        originalSpeed = stat.baseMovementSpeed;
        CurSpeed = originalSpeed;
        //공격범위
        attackRange = stat.AttackRange;
        //공격속도
        attackSpeed = stat.curAttackSpeed;
        //공격 딜레이
        beforCastDelay = 1f;

        IState<EnemyGolemController> enemyGolemIdle = gameObject.AddComponent<IdleStateEnemyGolem>();
        IState<EnemyGolemController> enemyGolemMove = gameObject.AddComponent<MoveStateEnemyGolem>();
        IState<EnemyGolemController> enemyGolemAttack = gameObject.AddComponent<AttackStateEnemyGolem>();
        IState<EnemyGolemController> enemyGolemHit = gameObject.AddComponent<HitStateEnemyGolem>();
        IState<EnemyGolemController> enemyGolemDead = gameObject.AddComponent<DeadStateEnemyGolem>();
        //IState<EnemyGolemController> enemyGolemSpecialAttack = gameObject.AddComponent<SpecialAttackStateEnemyGolem>();

        dicState.Add(enemyGolemState.Idle, enemyGolemIdle);
        dicState.Add(enemyGolemState.Move, enemyGolemMove);
        dicState.Add(enemyGolemState.Attack, enemyGolemAttack);
        dicState.Add(enemyGolemState.Hit, enemyGolemHit);
        dicState.Add(enemyGolemState.Dead, enemyGolemDead);
        //dicState.Add(enemyGolemState.SpecialAttack, enemyGolemSpecialAttack);

        stateMachineGolem = new StateMachine<EnemyGolemController>(this, dicState[enemyGolemState.Idle]);

    }

    void OnEnable()
    {
        MoveAble = true;
        target = GameManager._instance.character.GetComponent<Rigidbody>();
        curHealth = stat.curHp;
        //이동속도
        originalSpeed = stat.baseMovementSpeed;
        CurSpeed = originalSpeed;
        //공격범위
        attackRange = stat.AttackRange;
        //공격속도
        attackSpeed = stat.curAttackSpeed;
        //공격 딜레이
        beforCastDelay = 1f;
        isLive = true;
    }
    void Update()
    {
        if (curHealth <= 0)
        {
            //onPlayerDead.Invoke();
            stateMachineGolem.SetState(dicState[enemyGolemState.Dead]);
            return;
        }

        float dist = Vector3.Distance(target.transform.position, transform.position);
        /*
        if ()
        {
            stateMachineGolem.SetState(dicState[enemyGolemState.Idle]);

        }
        */

        switch (stateMachineGolem.CurState)
        {
            case IdleStateEnemyGolem:
                if (dist >= attackRange && anim.GetBool("Hit") == false && anim.GetBool("Attack") == false)
                {
                    stateMachineGolem.SetState(dicState[enemyGolemState.Move]);
                }
                if (dist <= attackRange && anim.GetBool("Hit") == false)
                {
                    stateMachineGolem.SetState(dicState[enemyGolemState.Attack]);
                }
                break;
            case MoveStateEnemyGolem:
                if (dist >= attackRange && anim.GetBool("Hit") == false && anim.GetBool("Attack") == false)
                {
                    stateMachineGolem.SetState(dicState[enemyGolemState.Move]);
                }
                if (dist <= attackRange && anim.GetBool("Hit") == false)
                {
                    stateMachineGolem.SetState(dicState[enemyGolemState.Attack]);
                }
                if (anim.GetBool("Hit") == false && anim.GetBool("Attack") == false && anim.GetBool("Move") == false && anim.GetBool("Idle") == true)
                {
                    stateMachineGolem.SetState(dicState[enemyGolemState.Idle]);
                }
                break;
            case AttackStateEnemyGolem:
                if (dist >= attackRange && MoveAble && anim.GetBool("Hit") == false && anim.GetBool("Attack") == false)
                {
                    stateMachineGolem.SetState(dicState[enemyGolemState.Move]);
                }
                else if (dist <= attackRange && anim.GetBool("Hit") == false)
                {
                    stateMachineGolem.SetState(dicState[enemyGolemState.Attack]);
                }
                if (anim.GetBool("Hit") == false && anim.GetBool("Attack") == false && anim.GetBool("Move") == false && anim.GetBool("Idle") == true)
                {
                    stateMachineGolem.SetState(dicState[enemyGolemState.Idle]);
                }
                break;
            case HitStateEnemyGolem:
                if (dist >= attackRange && MoveAble && anim.GetBool("Hit") == false && anim.GetBool("Attack") == false)
                {
                    stateMachineGolem.SetState(dicState[enemyGolemState.Move]);
                }
                else if (dist <= attackRange && anim.GetBool("Hit") == false)
                {
                    stateMachineGolem.SetState(dicState[enemyGolemState.Attack]);
                }
                if (anim.GetBool("Hit") == false && anim.GetBool("Attack") == false && anim.GetBool("Move") == false && anim.GetBool("Idle") == true)
                {
                    stateMachineGolem.SetState(dicState[enemyGolemState.Idle]);
                }
                break;
        }
        stateMachineGolem.DoOperateUpdate();
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
            stateMachineGolem.SetState(dicState[enemyGolemState.Hit]);

        }
    }
}

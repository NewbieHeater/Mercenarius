using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
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
    public SpriteRenderer sprite;
    public UnityEvent onPlayerDead;
    public Rigidbody enemyRb;
    public Rigidbody target;
    public NavMeshAgent nav;
    public Animator anim;
    //public Transform MainBody;
    public UnitCode unitCode;
    public Stat stat;

    private Dictionary<enemyGolemState, IState<EnemyGolemController>> dicState = new Dictionary<enemyGolemState, IState<EnemyGolemController>>();
    private StateMachine<EnemyGolemController> stateMachineGolem;
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
        //������
        enemytype = stat.name;
        //ü��
        maxHealth = stat.maxHp;
        curHealth = maxHealth;
        //�̵��ӵ�
        originalSpeed = stat.originalSpeed;
        CurSpeed = originalSpeed;
        //���ݹ���
        attackRange = stat.AttackRange;
        //���ݼӵ�
        attackSpeed = stat.AttackSpeed;
        //���� ������
        beforCastDelay = stat.beforeCastDelay;

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
        target = GameManager.instance.player.GetComponent<Rigidbody>();
        curHealth = maxHealth;
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
        if (dist >= attackRange && MoveAble && anim.GetBool("Hit") == false)
        {
            stateMachineGolem.SetState(dicState[enemyGolemState.Move]);
        }
        else if (dist <= attackRange && anim.GetBool("Hit") == false)
        {
            stateMachineGolem.SetState(dicState[enemyGolemState.Attack]);
        }
        else if (anim.GetBool("Hit") == false && anim.GetBool("Attack") == false && anim.GetBool("Move") == false && anim.GetBool("Idle") == true)
        {
            stateMachineGolem.SetState(dicState[enemyGolemState.Idle]);
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

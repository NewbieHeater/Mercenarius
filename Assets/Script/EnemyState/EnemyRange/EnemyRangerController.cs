using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyRangerController : MonoBehaviour
{
    public enum enemyRangerState
    {
        Idle,
        Move,
        RangeAttack,
        Hit,
        Dead,
    }

    public bool isLive = false;
    public bool isHit = false;
    public bool MoveAble = true;

    public string enemytype = "Null";

    public float attackRange = 6f;
    public float runAwayRange = 4f;


    public Image Hpbar;
    public Transform AttackPoint;
    public UnityEvent onPlayerDead;
    public Rigidbody target;
    private SpriteRenderer sprite;
    private Rigidbody enemyRb;
    private NavMeshAgent agent;
    private Animator anim;
    //public Transform MainBody;
    public UnitCode unitCode;

    private Dictionary<enemyRangerState, IState<EnemyRangerController>> dicState = new Dictionary<enemyRangerState, IState<EnemyRangerController>>();
    private StateMachine<EnemyRangerController> stateMachineRanger;
    private void Awake()
    {

    }
    void Start()
    {
        target = GameManager._instance.player.GetComponent<Rigidbody>();
        //GetComponentInParent
        //AttackPoint = transform.GetChild(0);
        sprite = GetComponentInChildren<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.updateRotation = false;
        //MainBody = transform.parent;
        enemyRb = GetComponent<Rigidbody>();
        //ÀûÁ¾·ù

        IState<EnemyRangerController> enemyRangerIdle = gameObject.AddComponent<IdleStateEnemyRanger>();
        IState<EnemyRangerController> enemyRangerMove = gameObject.AddComponent<MoveStateEnemyRanger>();
        IState<EnemyRangerController> enemyRangerAttack = gameObject.AddComponent<AttackStateEnemyRanger>();
        //IState<EnemyRangerController> enemyRangerHit = gameObject.AddComponent<HitStateEnemyRanger>();
        //IState<EnemyRangerController> enemyRangerDead = gameObject.AddComponent<DeadStateEnemyRanger>();
        //IState<EnemyGolemController> enemyGolemSpecialAttack = gameObject.AddComponent<SpecialAttackStateEnemyGolem>();

        dicState.Add(enemyRangerState.Idle, enemyRangerIdle);
        dicState.Add(enemyRangerState.Move, enemyRangerMove);
        dicState.Add(enemyRangerState.RangeAttack, enemyRangerAttack);
        //dicState.Add(enemyRangerState.Hit, enemyRangerHit);
        //dicState.Add(enemyRangerState.Dead, enemyRangerDead);
        //dicState.Add(enemyGolemState.SpecialAttack, enemyGolemSpecialAttack);

        stateMachineRanger = new StateMachine<EnemyRangerController>(this, dicState[enemyRangerState.Move]);

    }

    void OnEnable()
    {
        //agent.enabled = true;
        MoveAble = true;
        target = GameManager._instance.player.GetComponent<Rigidbody>();
        isLive = true;
    }
    void Update()
    {
        //if (curHealth <= 0)
        //{
        //    onPlayerDead.Invoke();
        //    stateMachineRanger.SetState(dicState[enemyRangerState.Dead]);
        //    return;
        //}

        float dist = Vector3.Distance(target.transform.position, transform.position);
        /*
        if ()
        {
            stateMachineGolem.SetState(dicState[enemyGolemState.Idle]);

        }
        */

        switch (stateMachineRanger.CurState)
        {
            case IdleStateEnemyRanger:
                if (dist >= attackRange)
                {
                    stateMachineRanger.SetState(dicState[enemyRangerState.Move]);
                }
                if (dist <= attackRange)
                {
                    stateMachineRanger.SetState(dicState[enemyRangerState.RangeAttack]);
                }
                else if (dist <= runAwayRange)
                {
                    stateMachineRanger.SetState(dicState[enemyRangerState.RangeAttack]);
                }
                break;
            case MoveStateEnemyRanger:
                if (dist >= attackRange)
                {
                    stateMachineRanger.SetState(dicState[enemyRangerState.Move]);
                }
                if (dist <= attackRange)
                {
                    stateMachineRanger.SetState(dicState[enemyRangerState.RangeAttack]);
                }
                break;
            case AttackStateEnemyRanger:
                if (dist >= attackRange)
                {
                    stateMachineRanger.SetState(dicState[enemyRangerState.Move]);
                }
                break;
        }
        stateMachineRanger.DoOperateUpdate();
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
            stateMachineRanger.SetState(dicState[enemyRangerState.Hit]);

        }
    }
}

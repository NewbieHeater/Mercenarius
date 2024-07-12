using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    public GameObject[] BasicAttackPrefab;
    public SOSkill Soskill;
    public bool dashUpGrade = false;
    public bool isFacingRight = true;
    public float coolDownDash = 0;
    public float dashPowerOrigin = 0f;
    public float dashPower = 0f;
    public float dashSpeed = 0f;

    public int maxHealth = 0;
    public int curHealth = 0;
    public int atkDamage = 0;
    public float attackSpeed = 0;
    public float orginSpeed = 0;//나중에 플레이어가 느려지는 상황 대비해서 원래 속도와 현재속도 구별
    public float curSpeed;

    //private string[] curState =
    
    public GameObject weaponHitBox;
    public SpriteRenderer spriteRender;
    public RaycastHit DashHit;
    public NavMeshAgent agent;     //네비매쉬
    public Animator anim;
    public Transform spot;  //마우스 클릭시 클릭 위치 표시를 위해 과녁모양가져오기
    [SerializeField]
    private Rigidbody rg = null;
    public bool MoveAble;
    // 스킬 이미지
    public Image imgIcon;
    // Cooldown 이미지
    public Image imgCool;

    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Dash,
        Roll,
        SkillAttack1
    }

    //public string[] strings = ;

    private Dictionary<PlayerState, IState<PlayerController>> dicState = new Dictionary<PlayerState, IState<PlayerController>>();
    private StateMachine<PlayerController> stateMachinePlayer;

    public WeaponTypeCode weaponTypeCode;
    public PlayerStat pStat;
    private void Awake()
    {
        pStat = new PlayerStat();
        pStat = pStat.SetUnitStat(weaponTypeCode);
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        agent = this.GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.speed = orginSpeed;
        agent.updateRotation = false;   //회적막기
    }
    public string weaponType = "Null";
    void Start()
    {
        weaponType = pStat.weaponName;
        maxHealth = pStat.maxHp;
        curHealth = maxHealth; //체력 초기화
        orginSpeed = pStat.originalSpeed;
        attackSpeed = pStat.AttackSpeed;
        curSpeed = orginSpeed;
        coolDownDash = pStat.originalDashCoolDown;
        dashPowerOrigin = pStat.originalDashPower;
        dashPower = dashPowerOrigin;
        dashSpeed = pStat.originalDashSpeed;

        IState<PlayerController> idle = gameObject.AddComponent<PlayerIdleState>();
        IState<PlayerController> move = gameObject.AddComponent<PlayerMoveState>();
        IState<PlayerController> attack = gameObject.AddComponent<PlayerAttackState>();
        IState<PlayerController> dash = gameObject.AddComponent<PlayerDashState>();
        IState<PlayerController> roll = gameObject.AddComponent<PlayerRollState>();

        dicState.Add(PlayerState.Idle, idle);
        dicState.Add(PlayerState.Move, move);
        dicState.Add(PlayerState.Attack, attack);
        dicState.Add(PlayerState.Dash, dash);
        dicState.Add(PlayerState.Roll, roll);

        stateMachinePlayer = new StateMachine<PlayerController>(this, dicState[PlayerState.Idle]);
    }

    void Update()
    {
        //Debug.Log(transform.position.x - curPosition.x);

        if (isFacingRight == true)
        {
            spriteRender.flipX = false;
        }
        else
        {
            spriteRender.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Z) && imgCool.fillAmount == 0 && dashUpGrade == true)
        {
            stateMachinePlayer.SetState(dicState[PlayerState.Dash]);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && imgCool.fillAmount == 0 && dashUpGrade == false)
        {
            dashSpeed = 2f;
            stateMachinePlayer.SetState(dicState[PlayerState.Roll]);
        }
        else if (Input.GetKeyDown(KeyCode.A) && anim.GetBool("Dash") == false)
        {
            AnimationEnd.Instance.atkStart();
            stateMachinePlayer.SetState(dicState[PlayerState.Attack]);
        }
        else if (Input.GetMouseButtonDown(0) && anim.GetBool("Attack") == false)
            stateMachinePlayer.SetState(dicState[PlayerState.Move]);
        else if (anim.GetBool("Run") == false && anim.GetBool("Dash") == false && anim.GetBool("Attack") == false)
            stateMachinePlayer.SetState(dicState[PlayerState.Idle]);
        

        stateMachinePlayer.DoOperateUpdate();
    }

    public void AnimeEnded() //애니메이션 이벤트로 유닛의 애니메이션이 끝나면 발동
    {
        MoveAble = true;

    }
    
    public void SetIdle()
    {
        stateMachinePlayer.SetState(dicState[PlayerState.Idle]);
    }
}

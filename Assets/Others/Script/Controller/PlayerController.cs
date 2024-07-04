using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;
using static SOItem;


public class PlayerController : MonoBehaviour
{
    public GameObject[] BasicAttackPrefab;
    public SOSkill Soskill;
    public bool dashUpGrade = false;

    public float coolDownDash = 0;
    public float dashPowerOrigin = 0f;
    public float dashPower = 0f;
    public float dashSpeed = 0f;
    public bool DashEnable = false;

    public int maxHealth = 0;
    public int curHealth = 0;
    public int atkDamage = 0;
    public float attackSpeed = 0;
    public float orginSpeed = 0;//���߿� �÷��̾ �������� ��Ȳ ����ؼ� ���� �ӵ��� ����ӵ� ����
    public float curSpeed;

    //private string[] curState =

    public GameObject weaponHitBox;
    public SpriteRenderer spriteRender;
    public RaycastHit DashHit;
    public NavMeshAgent agent;     //�׺�Ž�
    public Animator anim;
    public Transform spot;  //���콺 Ŭ���� Ŭ�� ��ġ ǥ�ø� ���� �����簡������
    [SerializeField]
    private Rigidbody rg = null;
    public bool MoveAble;
    // ��ų �̹���
    public Image imgIcon;
    // Cooldown �̹���
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
        agent.updateRotation = false;   //ȸ������
    }
    public string weaponType = "Null";
    void Start()
    {
        weaponType = pStat.weaponName;
        maxHealth = pStat.maxHp;
        curHealth = maxHealth; //ü�� �ʱ�ȭ
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
        if (Input.GetKeyDown(KeyCode.Z) && imgCool.fillAmount == 0 && dashUpGrade == true)
        {
            DashEnable = true;
            stateMachinePlayer.SetState(dicState[PlayerState.Dash]);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && imgCool.fillAmount == 0 && dashUpGrade == false)
        {
            dashSpeed = 2f;
            DashEnable = true;
            stateMachinePlayer.SetState(dicState[PlayerState.Roll]);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            stateMachinePlayer.SetState(dicState[PlayerState.Attack]);
        }
        else if (Input.GetMouseButtonDown(0))
            stateMachinePlayer.SetState(dicState[PlayerState.Move]);
        else if (anim.GetBool("Running") == false && DashEnable == false && anim.GetBool("Idle") == true)
            stateMachinePlayer.SetState(dicState[PlayerState.Idle]);
        

        stateMachinePlayer.DoOperateUpdate();
    }

    public void AnimeEnded() //�ִϸ��̼� �̺�Ʈ�� ������ �ִϸ��̼��� ������ �ߵ�
    {
        MoveAble = true;

    }

    public void SetIdle()
    {
        stateMachinePlayer.SetState(dicState[PlayerState.Idle]);
    }
}

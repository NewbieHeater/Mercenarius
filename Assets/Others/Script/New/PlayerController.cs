using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;
using static MonsterController;


public class PlayerController : MonoBehaviour
{
    public SOSkill Soskill;
    public float coolDownDash = 0;
    public float orginSpeed = 5;//���߿� �÷��̾ �������� ��Ȳ ����ؼ� ���� �ӵ��� ����ӵ� ����
    public float curSpeed;
    public float dashPowerOrigin = 5f;
    public float dashPower = 5f;
    public float dashSpeed = 6f;
    public bool Dash = false;

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
    }

    //public string[] strings = ;

    private Dictionary<PlayerState, IState<PlayerController>> dicState = new Dictionary<PlayerState, IState<PlayerController>>();
    private StateMachine<PlayerController> smp;

    private void Awake()
    {
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        agent = this.GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.speed = orginSpeed;
        curSpeed = orginSpeed;
        agent.updateRotation = false;   //ȸ������
    }
    void Start()
    {
        IState<PlayerController> idle = gameObject.AddComponent<PlayerIdleState>();
        IState<PlayerController> move = gameObject.AddComponent<PlayerMoveState>();
        IState<PlayerController> attack = gameObject.AddComponent<PlayerAttackState>();
        IState<PlayerController> dash = gameObject.AddComponent<PlayerDashState>();

        dicState.Add(PlayerState.Idle, idle);
        dicState.Add(PlayerState.Move, move);
        dicState.Add(PlayerState.Attack, attack);
        dicState.Add(PlayerState.Dash, dash);

        smp = new StateMachine<PlayerController>(this, dicState[PlayerState.Idle]);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            Dash = false;
            smp.SetState(dicState[PlayerState.Dash]);
        }
            
        else if (Input.GetMouseButtonDown(0))
            smp.SetState(dicState[PlayerState.Move]);
        else if (anim.GetBool("Running") == false && Dash == true)
            smp.SetState(dicState[PlayerState.Idle]);



        smp.DoOperateUpdate();
    }

    public void AnimeEnded() //�ִϸ��̼� �̺�Ʈ�� ������ �ִϸ��̼��� ������ �ߵ�
    {
        MoveAble = true;

    }

    public void SetIdle()
    {
        smp.SetState(dicState[PlayerState.Idle]);
    }
}

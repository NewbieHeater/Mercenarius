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
    public float orginSpeed = 5;//나중에 플레이어가 느려지는 상황 대비해서 원래 속도와 현재속도 구별
    public float curSpeed;
    public float dashPowerOrigin = 5f;
    public float dashPower = 5f;
    public float dashSpeed = 5f;
    public bool DashEnable = false;

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
        Roll
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
        agent.updateRotation = false;   //회적막기
    }
    void Start()
    {
        IState<PlayerController> idle = gameObject.AddComponent<PlayerIdleState>();
        IState<PlayerController> move = gameObject.AddComponent<PlayerMoveState>();
        IState<PlayerController> attack = gameObject.AddComponent<PlayerAttackState>();
        IState<PlayerController> dash = gameObject.AddComponent<PlayerDashState>();
        IState<PlayerController> roll = gameObject.AddComponent<PlayerDashState>();

        dicState.Add(PlayerState.Idle, idle);
        dicState.Add(PlayerState.Move, move);
        dicState.Add(PlayerState.Attack, attack);
        dicState.Add(PlayerState.Dash, dash);
        dicState.Add(PlayerState.Roll, roll);

        smp = new StateMachine<PlayerController>(this, dicState[PlayerState.Idle]);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A) && imgCool.fillAmount == 0)
        {
            DashEnable = true;
            smp.SetState(dicState[PlayerState.Dash]);
        }    
        else if (Input.GetMouseButtonDown(0))
            smp.SetState(dicState[PlayerState.Move]);
        else if (anim.GetBool("Running") == false && DashEnable == false)
            smp.SetState(dicState[PlayerState.Idle]);

        smp.DoOperateUpdate();
    }

    public void AnimeEnded() //애니메이션 이벤트로 유닛의 애니메이션이 끝나면 발동
    {
        MoveAble = true;

    }

    public void SetIdle()
    {
        smp.SetState(dicState[PlayerState.Idle]);
    }
}

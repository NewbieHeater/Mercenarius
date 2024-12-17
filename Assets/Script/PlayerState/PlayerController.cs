using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;




public class PlayerController : MonoBehaviour
{
    [Header("객체의 스탯 정보")]
    [SerializeField]
    public GameObject[] BasicAttackPrefab;
    public bool isAttack = false;
    private BuffController buffController;
    public StatData statData;

    public bool isFacingRight = true;


    [Header("아직 미구현")]
    public GameObject weaponHitBox;
    public Rigidbody rigid;
    private SpriteRenderer spriteRender;
    private NavMeshAgent agent;          //네비매쉬
    private Animator anim;
    private Transform spot;              //마우스 클릭시 클릭 위치 표시를 위해 과녁모양가져오기
    public bool isInvincible;

    public Image imgIcon;               // 스킬 이미지
    public Image imgCool;               // Cooldown 이미지

    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Dash,
        Roll,
        SpearThrow
    }

    //public string[] strings = ;

    private Dictionary<PlayerState, IState<PlayerController>> dicState = new Dictionary<PlayerState, IState<PlayerController>>();
    private StateMachine<PlayerController> stateMachinePlayer;

    public WeaponTypeCode weaponTypeCode;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        statData = new StatData();
        statData.SetUnitStat(weaponTypeCode);
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        agent = this.GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.updateRotation = false;   //회전막기
    }
    
    public string weaponType = "Null";
    void Start()
    {
        buffController = gameObject.AddComponent<BuffController>();
        buffController.Initialize(statData);
        isInvincible = false;

        IState<PlayerController> idle = gameObject.AddComponent<PlayerIdleState>();
        IState<PlayerController> move = gameObject.AddComponent<PlayerMoveState>();
        IState<PlayerController> attack = gameObject.AddComponent<PlayerAttackState>();
        IState<PlayerController> dash = gameObject.AddComponent<PlayerDashState>();
        IState<PlayerController> roll = gameObject.AddComponent<PlayerRollState>();
        IState<PlayerController> spearThrow = gameObject.AddComponent<PlayerSpearThrowState>();

        dicState.Add(PlayerState.Idle, idle);
        dicState.Add(PlayerState.Move, move);
        dicState.Add(PlayerState.Attack, attack);
        dicState.Add(PlayerState.Dash, dash);
        dicState.Add(PlayerState.Roll, roll);
        dicState.Add(PlayerState.SpearThrow, spearThrow);

        stateMachinePlayer = new StateMachine<PlayerController>(this, dicState[PlayerState.Idle]);
    }
    

    void Update()
    {
        //버프 시스템 테스트중
        if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("SkillQuickSlot2")))
        {
            Buff attackBuff = new Buff(BuffType.AttackBuff, 10.0f, 10.0f);
            buffController.AddBuff(attackBuff);
            Buff bleedDebuff = new Buff(BuffType.Bleed, -2.0f, 5.0f, false, 1.0f);
            buffController.AddBuff(bleedDebuff);
        }

        if (SettingSystem.isPause)
            return;

        if (isFacingRight == true)
        {
            spriteRender.flipX = false;
        }
        else
        {
            spriteRender.flipX = true;
        }

        //상태전환
        switch (stateMachinePlayer.CurState)
        {
            case PlayerIdleState:
                if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("Dash")) && imgCool.fillAmount == 0)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Dash]);
                }
                else if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("SkillQuickSlot1")))
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.SpearThrow]);
                }
                else if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("BasicAttack")) && !isAttack)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Attack]);
                }
                else if (Input.GetMouseButtonDown(0) && CheckGround(Input.mousePosition) != transform.position)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Move]);
                }
                else if (anim.GetBool("Run") == false && anim.GetBool("Dash") == false && anim.GetBool("Attack") == false)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Idle]);
                }
                
                //else if (!isInvincible)
                break;
            case PlayerMoveState:
                if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("Dash")) && imgCool.fillAmount == 0)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Dash]);
                }
                else if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("BasicAttack")) && !isAttack)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Attack]);
                }
                else if (anim.GetBool("Run") == false && anim.GetBool("Dash") == false && anim.GetBool("Attack") == false)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Idle]);
                }
                break;
            case PlayerAttackState:
                if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("Dash")) && imgCool.fillAmount == 0)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Dash]);
                }
                else if (Input.GetMouseButtonDown(0) && anim.GetBool("Attack") == false)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Move]);
                }
                else if (anim.GetBool("Run") == false && anim.GetBool("Dash") == false && anim.GetBool("Attack") == false)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Idle]);
                }
                break;
            case PlayerDashState:
                if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("Dash")) && imgCool.fillAmount == 0)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Dash]);
                }
                else if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("BasicAttack")) && anim.GetBool("Dash") == false && !isAttack)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Attack]);
                }
                else if (Input.GetMouseButtonDown(0) && anim.GetBool("Dash") == false)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Move]);
                }
                else if (anim.GetBool("Run") == false && anim.GetBool("Dash") == false && anim.GetBool("Attack") == false)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Idle]);
                }
                break;
            case PlayerSpearThrowState:
                if(Input.GetMouseButtonDown(0) && anim.GetBool("Dash") == false)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Move]);
                }
                else if (anim.GetBool("Run") == false && anim.GetBool("Dash") == false && anim.GetBool("Attack") == false)
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Idle]);
                }
                break;

        }
        stateMachinePlayer.DoOperateUpdate();
    }

    public void AnimeEnded() //애니메이션 이벤트로 유닛의 애니메이션이 끝나면 발동
    {
        anim.SetBool("Attack", false);
        isAttack = false;
    }

    public Vector3 CheckGround(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
            return hit.point;
        else
            return transform.position;
    }
}

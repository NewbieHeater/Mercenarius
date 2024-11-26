using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;




public class PlayerController : MonoBehaviour
{
    [Header("��ü�� ���� ����")]
    [SerializeField]
    public GameObject[] BasicAttackPrefab;
    public bool isAttack = false;
    private BuffController buffController;
    public StatData statData;

    public bool isFacingRight = true;


    [Header("���� �̱���")]
    public GameObject weaponHitBox;
    public Rigidbody rigid;
    private SpriteRenderer spriteRender;
    private NavMeshAgent agent;          //�׺�Ž�
    private Animator anim;
    private Transform spot;              //���콺 Ŭ���� Ŭ�� ��ġ ǥ�ø� ���� �����簡������
    public bool isInvincible;

    public Image imgIcon;               // ��ų �̹���
    public Image imgCool;               // Cooldown �̹���

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
        agent.updateRotation = false;   //ȸ������
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
        //���� �ý��� �׽�Ʈ��
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

        //������ȯ
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

    public void AnimeEnded() //�ִϸ��̼� �̺�Ʈ�� ������ �ִϸ��̼��� ������ �ߵ�
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

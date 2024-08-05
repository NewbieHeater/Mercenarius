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

    
    public bool isFacingRight = true;

    private bool dashUpGrade = false;
    public float coolDownDash;
    public float dashPowerOrigin;
    public float dashPower;
    public float dashSpeedOrigin;
    public float dashSpeed;

    public int maxHealth;
    public int curHealth;
    public int atkDamage;
    public float attackSpeed;
    public float orginSpeed;        //���߿� �÷��̾ �������� ��Ȳ ����ؼ� ���� �ӵ��� ����ӵ� ����
    public float curSpeed;

    public GameObject weaponHitBox;
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
        isInvincible = false;

        weaponType = pStat.weaponName;              //����Ÿ��
        maxHealth = pStat.maxHp;                    //�ִ�ü�� ����
        curHealth = maxHealth;                      //����ü�� �ʱ�ȭ       
        attackSpeed = pStat.AttackSpeed;            //���ݼӵ�        
        orginSpeed = pStat.originalSpeed;           //�̵��ӵ�
        curSpeed = orginSpeed;                      //����ӵ� ����
        coolDownDash = pStat.originalDashCoolDown;  //�뽬 ��Ÿ��        
        dashPowerOrigin = pStat.originalDashPower;  //���� �뽬�Ÿ�
        dashPower = dashPowerOrigin;                //���� �뽬�Ÿ�
        dashSpeedOrigin = pStat.originalDashSpeed;  //���� �뽬�ӵ�
        dashSpeed = dashSpeedOrigin;                //���� �뽬�ӵ�

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
    public bool isAttack = false;
    void Update()
    {
        if (SettingSystem.isPause)
            return;
        //Debug.Log(transform.position.x - curPosition.x);
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    bool isAbleToAttack = (!anim.GetBool("Attack")) && (comboCount == 0);
        //}

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
                else if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("BasicAttack")) && !isAttack && (comboCount < 3))
                {
                    stateMachinePlayer.SetState(dicState[PlayerState.Attack]);
                }
                else if (Input.GetMouseButtonDown(0))
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
                else if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("BasicAttack")) && !isAttack && (comboCount < 3))
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
                else if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("BasicAttack")) && anim.GetBool("Dash") == false && !isAttack && (comboCount < 3))
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
        }
        stateMachinePlayer.DoOperateUpdate();
    }

    public void AnimeEnded() //�ִϸ��̼� �̺�Ʈ�� ������ �ִϸ��̼��� ������ �ߵ�
    {
        anim.SetBool("Attack", false);
        isAttack = false;
        //stateMachinePlayer.SetState(dicState[PlayerState.Idle]);
    }
    
    public void SetIdle()
    {
        stateMachinePlayer.SetState(dicState[PlayerState.Idle]);
    }
    private Coroutine checkAttackReInputCor;
    public int comboCount;
    public void CheckAttackReInput(float reInputTime)
    {
        if (checkAttackReInputCor != null)
            StopCoroutine(checkAttackReInputCor);
        checkAttackReInputCor = StartCoroutine(CheckAttackReInputCoroutine(reInputTime));
    }

    private IEnumerator CheckAttackReInputCoroutine(float reInputTime)
    {
        float currentTime = 0f;
        while (true)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= reInputTime)
                break;
            yield return null;
        }

        comboCount = 0;
        anim.SetInteger("AttackCombo", 0);
    }
    
    
}

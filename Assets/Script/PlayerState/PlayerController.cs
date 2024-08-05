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
    public float orginSpeed;        //나중에 플레이어가 느려지는 상황 대비해서 원래 속도와 현재속도 구별
    public float curSpeed;

    public GameObject weaponHitBox;
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
        agent.updateRotation = false;   //회전막기
    }
    public string weaponType = "Null";
    void Start()
    {
        isInvincible = false;

        weaponType = pStat.weaponName;              //무기타입
        maxHealth = pStat.maxHp;                    //최대체력 설정
        curHealth = maxHealth;                      //현재체력 초기화       
        attackSpeed = pStat.AttackSpeed;            //공격속도        
        orginSpeed = pStat.originalSpeed;           //이동속도
        curSpeed = orginSpeed;                      //현재속도 설정
        coolDownDash = pStat.originalDashCoolDown;  //대쉬 쿨타임        
        dashPowerOrigin = pStat.originalDashPower;  //원래 대쉬거리
        dashPower = dashPowerOrigin;                //현재 대쉬거리
        dashSpeedOrigin = pStat.originalDashSpeed;  //원래 대쉬속도
        dashSpeed = dashSpeedOrigin;                //현재 대쉬속도

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

        //상태전환
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

    public void AnimeEnded() //애니메이션 이벤트로 유닛의 애니메이션이 끝나면 발동
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

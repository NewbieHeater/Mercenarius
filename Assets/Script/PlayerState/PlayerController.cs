using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;




public class PlayerController : MonoBehaviour
{
    [Header("객체의 스탯 정보")]
    [SerializeField] 
    public StatData statData;

    public GameObject[] BasicAttackPrefab;
    public SOSkill Soskill;

    
    public bool isFacingRight = true;


    private bool dashUpGrade;
    public float coolDownDash;
    public float dashPowerOrigin;
    public float dashPower;
    public float dashSpeedOrigin;
    public float dashSpeed;
    
    public float maxHealth { get { return statData.maxHp; } }
    public float curHealth { get { return statData.HpCurrent; } }
    public float atkDamage { get { return statData.AttackCurrent; } }
    public float attackSpeed { get { return statData.AttackSpeedCurrent; } }
    public float curSpeed { get { return statData.MovementSpeedCurrent; } }
    public float atkDamages;
    [Header("미구현")]
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
    private void Awake()
    {
        statData.SetUnitStat(weaponTypeCode);
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        agent = this.GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.updateRotation = false;   //회전막기
    }
    
    public string weaponType = "Null";
    void Start()
    {
        character = GetComponent<Character>();
        isInvincible = false;
                 //현재속도 설정
        coolDownDash = statData.baseDashCoolDown;  //대쉬 쿨타임        
        dashPowerOrigin = statData.baseDashPower;  //원래 대쉬거리
        dashPower = dashPowerOrigin;                //현재 대쉬거리
        dashSpeedOrigin = statData.baseDashSpeed;  //원래 대쉬속도
        dashSpeed = dashSpeedOrigin;                //현재 대쉬속도
        agent.speed = curSpeed;

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
    Character character;

    void Update()
    {
        atkDamages = atkDamage;
        if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("SkillQuickSlot2")))
        {
            character.ApplyBuff(new AttackBuff(10f, 20f)); // 10초 동안 공격력 +20
            Debug.Log(character.AttackBuff);
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
                else if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("BasicAttack")) && !isAttack)
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

    //private Coroutine checkAttackReInputCor;
    //public int comboCount;
    //public void CheckAttackReInput(float reInputTime)
    //{
    //    if (checkAttackReInputCor != null)
    //        StopCoroutine(checkAttackReInputCor);
    //    checkAttackReInputCor = StartCoroutine(CheckAttackReInputCoroutine(reInputTime));
    //}
    //private IEnumerator CheckAttackReInputCoroutine(float reInputTime)
    //{
    //    float currentTime = 0f;
    //    while (true)
    //    {
    //        currentTime += Time.deltaTime;
    //        if (currentTime >= reInputTime)
    //            break;
    //        yield return null;
    //    }

    //    comboCount = 0;
    //    anim.SetInteger("AttackCombo", 0);
    //}
}

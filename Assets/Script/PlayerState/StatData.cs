using UnityEngine;

[System.Serializable]
public class StatData 
{
    [field: Header("초기화 시 최대 체력")]
    [field: SerializeField] public float mMaxHp { private set; get; }
    public float maxHp
    {
        get
        {
            return ((mMaxHp + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Hp : 0f)) );
        }
    }
    [SerializeField][HideInInspector] private float mHpCurrent = 100f;
    public float curHp
    { 
        get 
        { 
            return mHpCurrent; 
        } 
    }


    [field: Header("초기화 시 기본 공격력")]
    [field: SerializeField] public float mBaseAttack { set; get; }
    public float curAttack
    {
        get
        {
            return ((mBaseAttack + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Damage : 0f)));
        }
    }
    [field: SerializeField] public float AttackRange { set; get; }

    [field: Header("초기화 시 기본 공격속도")]
    [field: SerializeField] public float mBaseAttackSpeed { private set; get; }
    public float curAttackSpeed
    {
        get
        {
            return ((mBaseAttackSpeed + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Damage : 0f)));
        }
    }

    [field: Header("초기화 시 기본 이동속도")]
    [field: SerializeField] public float mBaseMovementSpeed { private set; get; }
    public float curMovementSpeed
    {
        get
        {
            return mBaseMovementSpeed +
            (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }



    [field: Header("초기화 시 기본 방어력")]
    [field: SerializeField] public float mBaseDefense {  set; get; }
    public float DefenseCurrent
    {
        get
        {
            return mBaseDefense + 
            (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }


    [field: Header("초기화 시 기본 대쉬쿨다운")]
    [field: SerializeField] public float mBaseDashCoolDown {  set; get; }
    public float curDashCoolDown
    {
        get
        {
            return (mBaseDashCoolDown);
        }
    }
    [field: Header("초기화 시 기본 대쉬거리")]
    [field: SerializeField] public float mBaseDashPower {  set; get; }
    public float curDashPower
    {
        get
        {
            return (mBaseDashPower);
        }
    }
    [field: Header("초기화 시 기본 대쉬속도")]
    [field: SerializeField] public float baseDashSpeed {  set; get; }
    public float curDashSpeed
    {
        get
        {
            return (baseDashSpeed);
        }
    }
    public int mInvincibleTime { set; get; }
    public int invincibleTime
    {
        get
        {
            return mInvincibleTime;
        }
    }

    #region 외부 클래스

    //public Character buffController; // 버프 컨트롤러 (모두에게 고유)

    [Space(30)]
    [Header("외부 클래스를 참조하여 스탯에 추가 효과")]
    [Header("해당 객체의 장비인벤토리, 없을경우 null 가능")]
    [SerializeField] public EquipmentInventory? equipmentInventory = null; // 장비 인벤토리 (개별적으로 로드하여 사용 가능)

    #endregion

    /// <summary>
    /// 스탯 정보를 초기값으로 초기화
    /// </summary>
    public void InitStatData()
    {
        mHpCurrent = mHpCurrent == 0 ? maxHp : mHpCurrent;
    }

    /// <summary>
    /// 현재 체력을 조정
    /// </summary>
    /// <param name="amount">조정할 값 (양수일경우 현재 체력 증가)</param>
    /// <returns>체력이 0 미만인가?</returns>
    public bool ModifyCurrentHp(float amount)
    {
        mHpCurrent += amount;
        mHpCurrent = Mathf.Clamp(mHpCurrent, float.MinValue, maxHp);

        return mHpCurrent < 0f;
    }

    
}
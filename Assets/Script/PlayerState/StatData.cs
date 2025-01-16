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
    [field: SerializeField] public float baseAttack { set; get; }
    public float curAttack
    {
        get
        {
            return ((baseAttack + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Damage : 0f)));
        }
    }
    [field: SerializeField] public float AttackRange { set; get; }

    [field: Header("초기화 시 기본 공격속도")]
    [field: SerializeField] public float baseAttackSpeed { private set; get; }
    public float curAttackSpeed
    {
        get
        {
            return ((baseAttackSpeed + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Damage : 0f)));
        }
    }

    [field: Header("초기화 시 기본 이동속도")]
    [field: SerializeField] public float baseMovementSpeed { private set; get; }
    public float curMovementSpeed
    {
        get
        {
            return baseMovementSpeed +
            (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }



    [field: Header("초기화 시 기본 방어력")]
    [field: SerializeField] public float baseDefense {  set; get; }
    public float DefenseCurrent
    {
        get
        {
            return baseDefense + 
            (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }


    [field: Header("초기화 시 기본 대쉬쿨다운")]
    [field: SerializeField] public float baseDashCoolDown {  set; get; }
    public float curDashCoolDown
    {
        get
        {
            return (baseDashCoolDown);
        }
    }
    [field: Header("초기화 시 기본 대쉬거리")]
    [field: SerializeField] public float baseDashPower {  set; get; }
    public float curDashPower
    {
        get
        {
            return (baseDashPower);
        }
    }
    [field: Header("초기화 시 기본 대쉬속도")]
    [field: SerializeField] public float baseDashSpeed {  set; get; }
    public float curDashSpeed
    {
        get
        {
            return (1f / baseDashSpeed);
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
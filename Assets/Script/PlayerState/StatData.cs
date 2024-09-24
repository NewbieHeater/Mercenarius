using UnityEngine;

[System.Serializable]
public class StatData 
{
    public WeaponTypeCode weaponTypeCode { get; set; }
    public string weaponName { get; set; }
    public float baseDashSpeed { get; set; }
    public float baseDashPower { get; set; }
    public float baseDashCoolDown { get; set; }
    public void PlayerStatData(WeaponTypeCode weaponTypeCode, string weaponName, int maxHp, int baseAttack, float baseMovementSpeed, float baseAttackSpeed, float baseDashSpeed, float baseDashPower, float baseDashCoolDown)
    {
        this.weaponTypeCode = weaponTypeCode;
        this.weaponName = weaponName;
        this.mMaxHp = maxHp;
        mHpCurrent = maxHp;
        this.baseAttack = baseAttack;
        this.baseMovementSpeed = baseMovementSpeed;
        this.baseAttackSpeed = baseAttackSpeed;
        this.baseDashSpeed = baseDashSpeed;
        this.baseDashPower = baseDashPower;
        this.baseDashCoolDown = baseDashCoolDown;
    }
    
    public void SetUnitStat(WeaponTypeCode weaponTypeCode)
    {
        switch (weaponTypeCode)
        {
            case WeaponTypeCode.Spear: //이름, 최대체력, 공격력, 속도, 공격속도, 대쉬속도, 대쉬파워, 대쉬쿨타임 _순서
                PlayerStatData(weaponTypeCode, "Spear", 150, 20, 5f, 4f, 6f, 4f, 2f);
                break;
            case WeaponTypeCode.Double_Dager:
                PlayerStatData(weaponTypeCode, "Double_Dager", 100, 5, 4f, 1f, 6f, 5f, 2f);
                break;
            case WeaponTypeCode.Lance:
                PlayerStatData(weaponTypeCode, "Lance", 100, 30, 4f, 8f, 5f, 5f, 2f);
                break;
        }
    }

    [field: Header("초기화 시 최대 체력")]
    [field: SerializeField] public float mMaxHp { private set; get; }
    public float maxHp
    {
        get
        {
            return ((mMaxHp + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Hp : 0f)) + buffController.HpBuff);
        }
    }
    [SerializeField][HideInInspector] private float mHpCurrent;
    public float HpCurrent 
    { 
        get 
        { 
            return mHpCurrent; 
        } 
    }


    [field: Header("초기화 시 기본 공격력")]
    [field: SerializeField] public float baseAttack { private set; get; }
    /// <summary>
    /// 현재 공격력
    /// </summary>
    public float AttackCurrent
    {
        get
        {
            return ((baseAttack + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Damage : 0f)) + buffController.AttackBuff);
        }
    }

    [field: Header("초기화 시 기본 공격속도")]
    [field: SerializeField] public float baseAttackSpeed { private set; get; }
    /// <summary>
    /// 현재 공격속도
    /// </summary>
    public float AttackSpeedCurrent
    {
        get
        {
            return ((baseAttackSpeed + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Damage : 0f)) + buffController.AttackBuff);
        }
    }

    [field: Header("초기화 시 기본 이동속도")]
    [field: SerializeField] public float baseMovementSpeed { private set; get; }
    /// <summary>
    /// 현재 이동속도
    /// </summary>
    public float MovementSpeedCurrent
    {
        get
        {
            return baseMovementSpeed + buffController.MovementSpeedBuff +
            (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }



    [field: Header("초기화 시 기본 방어력")]
    [field: SerializeField] public float baseDefense { private set; get; }
    /// <summary>
    /// 현재 방어력
    /// </summary>
    public float DefenseCurrent
    {
        get
        {
            return baseDefense + buffController.DefenseBuff +
            (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }



    #region 외부 클래스

    public Character buffController; // 버프 컨트롤러 (모두에게 고유)

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

    /// <summary>
    /// BaseStat을 영구적으로 증가
    /// </summary>
    /// <param name="statIndex"></param>
    //public void UpgradeBaseStat(StatType statType)
    //{
    //    switch (statType)
    //    {
    //        case StatType.LEVEL: // 레벨
    //            ++level;
    //            break;

    //        case StatType.HP: // 체력
    //            hpMax += 50;
    //            ModifyCurrentHp(50);
    //            break;

    //        case StatType.MP: // 마나
    //            mpMax += 50;
    //            ModifyCurrentMp(50);
    //            break;

    //        case StatType.ATTACK: // 공격력
    //            baseAttack += 5;
    //            break;

    //        case StatType.MOVEMENT_SPEED: // 속도
    //            baseMovementSpeed += 1;
    //            break;

    //        case StatType.DEFENSE: // 방어
    //            baseDefense += 2.5f;
    //            break;

    //        default:
    //            Debug.LogError($"인덱스 {statType}은 없음!");
    //            break;
    //    }
    //}
}
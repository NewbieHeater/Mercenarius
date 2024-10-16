using UnityEngine;

[System.Serializable]
public class StatData 
{
    public WeaponTypeCode weaponTypeCode { get; set; }
    public string weaponName { get; set; }
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
            case WeaponTypeCode.Spear: //�̸�, �ִ�ü��, ���ݷ�, �ӵ�, ���ݼӵ�, �뽬�ӵ�, �뽬�Ŀ�, �뽬��Ÿ�� _����
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

    [field: Header("�ʱ�ȭ �� �ִ� ü��")]
    [field: SerializeField] public float mMaxHp { private set; get; }
    public float maxHp
    {
        get
        {
            return ((mMaxHp + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Hp : 0f)) );
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


    [field: Header("�ʱ�ȭ �� �⺻ ���ݷ�")]
    [field: SerializeField] public float baseAttack = 10;
    public float AttackCurrent
    {
        get
        {
            return ((baseAttack + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Damage : 0f)));
        }
    }

    [field: Header("�ʱ�ȭ �� �⺻ ���ݼӵ�")]
    [field: SerializeField] public float baseAttackSpeed { private set; get; }
    public float AttackSpeedCurrent
    {
        get
        {
            return ((baseAttackSpeed + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Damage : 0f)));
        }
    }

    [field: Header("�ʱ�ȭ �� �⺻ �̵��ӵ�")]
    [field: SerializeField] public float baseMovementSpeed { private set; get; }
    public float MovementSpeedCurrent
    {
        get
        {
            return baseMovementSpeed +
            (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }



    [field: Header("�ʱ�ȭ �� �⺻ ����")]
    [field: SerializeField] public float baseDefense { private set; get; }
    public float DefenseCurrent
    {
        get
        {
            return baseDefense + 
            (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }

    public bool dashUpGrade;

    public float baseDashCoolDown = 3;
    public float curDashCoolDown
    {
        get
        {
            return (baseDashCoolDown);
        }
    }
    public float baseDashPower = 5;
    public float curDashPower
    {
        get
        {
            return (baseDashPower);
        }
    }
    public float baseDashSpeed = 5;
    public float curDashSpeed
    {
        get
        {
            return (baseDashSpeed);
        }
    }

    #region �ܺ� Ŭ����

    //public Character buffController; // ���� ��Ʈ�ѷ� (��ο��� ����)

    [Space(30)]
    [Header("�ܺ� Ŭ������ �����Ͽ� ���ȿ� �߰� ȿ��")]
    [Header("�ش� ��ü�� ����κ��丮, ������� null ����")]
    [SerializeField] public EquipmentInventory? equipmentInventory = null; // ��� �κ��丮 (���������� �ε��Ͽ� ��� ����)

    #endregion



    /// <summary>
    /// ���� ������ �ʱⰪ���� �ʱ�ȭ
    /// </summary>
    public void InitStatData()
    {
        mHpCurrent = mHpCurrent == 0 ? maxHp : mHpCurrent;
    }

    /// <summary>
    /// ���� ü���� ����
    /// </summary>
    /// <param name="amount">������ �� (����ϰ�� ���� ü�� ����)</param>
    /// <returns>ü���� 0 �̸��ΰ�?</returns>
    public bool ModifyCurrentHp(float amount)
    {
        mHpCurrent += amount;
        mHpCurrent = Mathf.Clamp(mHpCurrent, float.MinValue, maxHp);

        return mHpCurrent < 0f;
    }

    
}
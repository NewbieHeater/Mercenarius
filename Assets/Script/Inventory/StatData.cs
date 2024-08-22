using UnityEngine;

[System.Serializable]
public class StatData
{
    [field: Header("�ʱ�ȭ �� ����")]
    [field: SerializeField] public int level { private set; get; } = 1;


    [field: Header("�ʱ�ȭ �� �ִ� ü��")]
    [field: SerializeField] public float hpMax { private set; get; }
    [SerializeField][HideInInspector] private float mHpCurrent;
    public float HpCurrent
    {
        get
        {
            return mHpCurrent;
        }
    }



    [field: Header("�ʱ�ȭ �� �ִ� ����")]
    [field: SerializeField] public float mpMax { private set; get; }
    [SerializeField][HideInInspector] private float mMpCurrent;
    public float MpCurrent
    {
        get
        {
            return mMpCurrent;
        }
    }



    [field: Header("�ʱ�ȭ �� �⺻ ���ݷ�")]
    [field: SerializeField] public float baseAttack { private set; get; }

    /// <summary>
    /// ���� ���ݷ�
    /// </summary>
    public float AttackCurrent
    {
        get
        {
            return baseAttack + buffController.BuffStat.damage +
            (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Damage : 0f);
        }
    }



    [field: Header("�ʱ�ȭ �� �⺻ �̵��ӵ�")]
    [field: SerializeField] public float baseMovementSpeed { private set; get; }

    /// <summary>
    /// ���� �̵��ӵ�
    /// </summary>
    public float MovementSpeedCurrent
    {
        get
        {
            return baseMovementSpeed + buffController.BuffStat.movementSpeed +
            (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }



    [field: Header("�ʱ�ȭ �� �⺻ ����")]
    [field: SerializeField] public float baseDefense { private set; get; }

    /// <summary>
    /// ���� ����
    /// </summary>
    public float DefenseCurrent
    {
        get
        {
            return baseDefense + buffController.BuffStat.defense +
            (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }



    #region �ܺ� Ŭ����

    [HideInInspector] public BuffController buffController = new BuffController(); // ���� ��Ʈ�ѷ� (��ο��� ����)

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
        mHpCurrent = mHpCurrent == 0 ? hpMax : mHpCurrent;
        mMpCurrent = mMpCurrent == 0 ? mpMax : mMpCurrent;
    }

    /// <summary>
    /// ���� ü���� ����
    /// </summary>
    /// <param name="amount">������ �� (����ϰ�� ���� ü�� ����)</param>
    /// <returns>ü���� 0 �̸��ΰ�?</returns>
    public bool ModifyCurrentHp(float amount)
    {
        mHpCurrent += amount;
        mHpCurrent = Mathf.Clamp(mHpCurrent, float.MinValue, hpMax);

        return mHpCurrent < 0f;
    }

    /// <summary>
    /// ���� ������ ����
    /// </summary>
    /// <param name="amount">������ �� (����ϰ�� ���� ���� ����)</param>
    public void ModifyCurrentMp(float amount)
    {
        mMpCurrent += amount;
        mMpCurrent = Mathf.Clamp(mMpCurrent, float.MinValue, mpMax);
    }

    /// <summary>
    /// BaseStat�� ���������� ����
    /// </summary>
    /// <param name="statIndex"></param>
    //public void UpgradeBaseStat(StatType statType)
    //{
    //    switch (statType)
    //    {
    //        case StatType.LEVEL: // ����
    //            ++level;
    //            break;

    //        case StatType.HP: // ü��
    //            hpMax += 50;
    //            ModifyCurrentHp(50);
    //            break;

    //        case StatType.MP: // ����
    //            mpMax += 50;
    //            ModifyCurrentMp(50);
    //            break;

    //        case StatType.ATTACK: // ���ݷ�
    //            baseAttack += 5;
    //            break;

    //        case StatType.MOVEMENT_SPEED: // �ӵ�
    //            baseMovementSpeed += 1;
    //            break;

    //        case StatType.DEFENSE: // ���
    //            baseDefense += 2.5f;
    //            break;

    //        default:
    //            Debug.LogError($"�ε��� {statType}�� ����!");
    //            break;
    //    }
    //}
}
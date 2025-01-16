using UnityEngine;

[System.Serializable]
public class StatData 
{
    [field: Header("�ʱ�ȭ �� �ִ� ü��")]
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


    [field: Header("�ʱ�ȭ �� �⺻ ���ݷ�")]
    [field: SerializeField] public float baseAttack { set; get; }
    public float curAttack
    {
        get
        {
            return ((baseAttack + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Damage : 0f)));
        }
    }
    [field: SerializeField] public float AttackRange { set; get; }

    [field: Header("�ʱ�ȭ �� �⺻ ���ݼӵ�")]
    [field: SerializeField] public float baseAttackSpeed { private set; get; }
    public float curAttackSpeed
    {
        get
        {
            return ((baseAttackSpeed + (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Damage : 0f)));
        }
    }

    [field: Header("�ʱ�ȭ �� �⺻ �̵��ӵ�")]
    [field: SerializeField] public float baseMovementSpeed { private set; get; }
    public float curMovementSpeed
    {
        get
        {
            return baseMovementSpeed +
            (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }



    [field: Header("�ʱ�ȭ �� �⺻ ����")]
    [field: SerializeField] public float baseDefense {  set; get; }
    public float DefenseCurrent
    {
        get
        {
            return baseDefense + 
            (equipmentInventory is not null ? equipmentInventory.CurrentEquipmentEffect.Defense : 0f);
        }
    }


    [field: Header("�ʱ�ȭ �� �⺻ �뽬��ٿ�")]
    [field: SerializeField] public float baseDashCoolDown {  set; get; }
    public float curDashCoolDown
    {
        get
        {
            return (baseDashCoolDown);
        }
    }
    [field: Header("�ʱ�ȭ �� �⺻ �뽬�Ÿ�")]
    [field: SerializeField] public float baseDashPower {  set; get; }
    public float curDashPower
    {
        get
        {
            return (baseDashPower);
        }
    }
    [field: Header("�ʱ�ȭ �� �⺻ �뽬�ӵ�")]
    [field: SerializeField] public float baseDashSpeed {  set; get; }
    public float curDashSpeed
    {
        get
        {
            return (1f / baseDashSpeed);
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
using UnityEngine;
using TMPro;

public class EquipmentInventory : InventoryBase
{
    public static bool IsInventoryActive = false;  // �κ��丮 Ȱ��ȭ �Ǿ��°�?


    [Header("���� ���� ��ġ�� ǥ���� �ؽ�Ʈ �󺧵�")]
    [SerializeField] private TextMeshProUGUI mDamageLabel;
    [SerializeField] private TextMeshProUGUI mDefenseLabel;

    private EquipmentEffect mCurrentEquipmentEffect;

    /// <summary>
    /// ���� ��� ���������� ���� ���� �߰� ȿ��
    /// </summary>
    /// <value></value>
    public EquipmentEffect CurrentEquipmentEffect
    {
        get
        {
            return mCurrentEquipmentEffect;
        }
    }

    new private void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// ��� �������� ��ȯ�ϸ� ���� ������ ���鿡 �°� �߰�ȿ���� �� ���
    /// </summary>
    public void CalculateEffect()
    {
        EquipmentEffect calcedEffect = new EquipmentEffect();

        foreach (InventorySlot slot in mSlots)
        {
            if (slot.Item == null) { continue; }

            calcedEffect += ((Item_Equipment)slot.Item).Effect;
        }

        mCurrentEquipmentEffect = calcedEffect;

        mDamageLabel.text = mCurrentEquipmentEffect.Damage.ToString();
        mDefenseLabel.text = mCurrentEquipmentEffect.Defense.ToString();
    }

    /// <summary>
    /// ������ Ÿ�Կ� �´� �����۽����� ����
    /// </summary>
    /// <param name="type">���Ϲ��� ������ ������ Ÿ��</param>
    /// <returns></returns>
    public InventorySlot GetEquipmentSlot(ItemType type)
    {
        switch (type)
        {
            case ItemType.Equipment_HELMET:
                {
                    return mSlots[0];
                }
            case ItemType.Equipment_ARMORPLATE:
                {
                    return mSlots[1];
                }
            case ItemType.Equipment_GLOVE:
                {
                    //�尩 ������ �ΰ��̱⶧����, �� ������ �켱���� �����ϵ��� �Ѵ�.
                    if (mSlots[2].Item == null) { return mSlots[2]; }
                    if (mSlots[3].Item == null) { return mSlots[3]; }

                    //�Ѵ� �� ������ �ƴѰ�� mSlot[2]�� �����Ѵ�.
                    return mSlots[2];
                }
            case ItemType.Equipment_PANTS:
                {
                    return mSlots[4];
                }
            case ItemType.Equipment_SHOES:
                {
                    return mSlots[5];
                }
        }

        Debug.Log("����");
        return null;
    }

    private void Update()
    {
        //�ɼ��� �����ִ°�� ��Ȱ��ȭ
        //if (GameMenuManager.IsOptionActive) { return; }

        if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("Equipment")))
        {
            if (mInventoryBase.activeInHierarchy)
            {
                mInventoryBase.SetActive(false);
                IsInventoryActive = false;

                //UtilityManager.TryLockCursor();
            }
            else
            {
                mInventoryBase.SetActive(true);
                IsInventoryActive = true;

                //UtilityManager.UnlockCursor();
            }
        }
    }

}
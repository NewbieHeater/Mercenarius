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

        //mDamageLabel.text = mCurrentEquipmentEffect.Damage.ToString();
       // mDefenseLabel.text = mCurrentEquipmentEffect.Defense.ToString();
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
            case ItemType.Equipment_NORMAL:
                {
                    //�尩 ������ �ΰ��̱⶧����, �� ������ �켱���� �����ϵ��� �Ѵ�.
                    if (mSlots[0].Item == null) { return mSlots[0]; }
                    if (mSlots[1].Item == null) { return mSlots[1]; }
                    if (mSlots[2].Item == null) { return mSlots[2]; }
                    if (mSlots[3].Item == null) { return mSlots[3]; }
                    if (mSlots[4].Item == null) { return mSlots[4]; }
                    if (mSlots[5].Item == null) { return mSlots[5]; }

                    //�Ѵ� �� ������ �ƴѰ�� mSlot[2]�� �����Ѵ�.
                    return mSlots[0];
                }
            case ItemType.Equipment_ARMORPLATE:
                {
                    return mSlots[2];
                }
            case ItemType.Equipment_GLOVE:
                {
                    return mSlots[3];
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

    

}
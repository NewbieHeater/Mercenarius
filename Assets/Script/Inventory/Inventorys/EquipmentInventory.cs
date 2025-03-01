using UnityEngine;
using TMPro;

public class EquipmentInventory : InventoryBase
{
    public static bool IsInventoryActive = false;  // 인벤토리 활성화 되었는가?


    [Header("현재 계산된 수치를 표현할 텍스트 라벨들")]
    [SerializeField] private TextMeshProUGUI mDamageLabel;
    [SerializeField] private TextMeshProUGUI mDefenseLabel;

    private EquipmentEffect mCurrentEquipmentEffect;

    /// <summary>
    /// 현재 장비 아이템으로 인해 받은 추가 효과
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
    /// 장비 아이템을 교환하면 현재 장착한 장비들에 맞게 추가효과를 재 계산
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
    /// 아이템 타입에 맞는 아이템슬롯을 리턴
    /// </summary>
    /// <param name="type">리턴받을 슬롯의 아이템 타입</param>
    /// <returns></returns>
    public InventorySlot GetEquipmentSlot(ItemType type)
    {
        switch (type)
        {
            case ItemType.Equipment_NORMAL:
                {
                    //장갑 슬롯은 두개이기때문에, 빈 슬롯을 우선으로 리턴하도록 한다.
                    if (mSlots[0].Item == null) { return mSlots[0]; }
                    if (mSlots[1].Item == null) { return mSlots[1]; }
                    if (mSlots[2].Item == null) { return mSlots[2]; }
                    if (mSlots[3].Item == null) { return mSlots[3]; }
                    if (mSlots[4].Item == null) { return mSlots[4]; }
                    if (mSlots[5].Item == null) { return mSlots[5]; }

                    //둘다 빈 슬롯이 아닌경우 mSlot[2]를 리턴한다.
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

        Debug.Log("없음");
        return null;
    }

    

}
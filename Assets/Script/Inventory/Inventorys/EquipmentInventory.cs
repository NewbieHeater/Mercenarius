using UnityEngine;
using TMPro;

public class EquipmentInventory : InventoryBase
{
    public static bool IsInventoryActive = false;  // 인벤토리 활성화 되었는가?
    public static EquipmentInventory Instance { get; private set; }

    [Header("현재 계산된 수치를 표현할 텍스트 라벨들")]
    [SerializeField] private TextMeshProUGUI mDamageLabel;
    [SerializeField] private TextMeshProUGUI mDefenseLabel;

    private EquipmentEffect mCurrentEquipmentEffect;

    /// <summary>
    /// 현재 장비 아이템으로 인해 받은 추가 효과
    /// </summary>
    public EquipmentEffect CurrentEquipmentEffect
    {
        get { return mCurrentEquipmentEffect; }
    }

    new private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        base.Awake();
    }

    private void Start()
    {
        // 게임 시작 시, 각 장비 슬롯에 있는 아이템에 대해 Equip()을 호출하여 효과를 적용합니다.
        Character playerCharacter = CharacterManager.Instance.character.GetComponent<Character>();
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.Item != null && slot.Item is Item_Equipment equipmentItem)
            {
                equipmentItem.Equip(playerCharacter);
            }
        }
    }

    /// <summary>
    /// 장비 아이템을 교체하거나 새로 획득했을 때 호출하여, 해당 아이템을 슬롯에 추가하고 Equip()을 호출합니다.
    /// </summary>
    /// <param name="newEquipment">추가할 장비 아이템</param>
    /// <param name="playerCharacter">플레이어 캐릭터</param>
    public void AddAndEquipItem(Item_Equipment newEquipment, Character playerCharacter)
    {
        InventorySlot slot = GetEquipmentSlot(newEquipment.Type);
        if (slot != null)
        {
            // 이미 장비가 있는 슬롯인 경우 교체(기존 장비 효과 해제)
            if (slot.Item != null)
            {
                ((Item_Equipment)slot.Item).Unequip(playerCharacter);
            }
            // 슬롯에 새 장비 아이템 추가
            slot.AddItem(newEquipment, 1);
            //slot.mItemImage.sprite = 
            // 새 장비의 효과 적용
            newEquipment.Equip(playerCharacter);
            // 장비 효과 재계산
            CalculateEffect();
        }
    }
    
    /// <summary>
    /// 장비 아이템을 교체하면 현재 장착한 장비들에 맞게 추가효과를 재계산합니다.
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
        // mDamageLabel.text = mCurrentEquipmentEffect.Damage.ToString();
        // mDefenseLabel.text = mCurrentEquipmentEffect.Defense.ToString();
    }

    /// <summary>
    /// 아이템 타입에 맞는 아이템 슬롯을 리턴합니다.
    /// </summary>
    /// <param name="type">리턴받을 슬롯의 아이템 타입</param>
    /// <returns></returns>
    public InventorySlot GetEquipmentSlot(ItemType type)
    {
        switch (type)
        {
            case ItemType.Equipment_NORMAL:
                {
                    if (mSlots[0].Item == null) return mSlots[0];
                    if (mSlots[1].Item == null) return mSlots[1];
                    if (mSlots[2].Item == null) return mSlots[2];
                    if (mSlots[3].Item == null) return mSlots[3];
                    if (mSlots[4].Item == null) return mSlots[4];
                    if (mSlots[5].Item == null) return mSlots[5];
                    return mSlots[0];
                }
            case ItemType.Equipment_ARMORPLATE:
                return mSlots[2];
            case ItemType.Equipment_GLOVE:
                return mSlots[3];
            case ItemType.Equipment_PANTS:
                return mSlots[4];
            case ItemType.Equipment_SHOES:
                return mSlots[5];
        }

        Debug.Log("해당 슬롯이 없습니다.");
        return null;
    }
}

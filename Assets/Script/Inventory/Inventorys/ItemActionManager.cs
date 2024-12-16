using UnityEngine;
using static System.Net.WebRequestMethods;


/// <summary>
/// 씬 내의 매니저 오브젝트에 할당
/// 아이템(또는 정적 물체)과 상호작용하거나, 인벤토리에서 아이템을 사용하면 특수 이벤트를 발생시킴
/// </summary>
public class ItemActionManager : MonoBehaviour
{
    /// <summary>
    /// 메시지를 주고받는경우 스킬에 대한 메시지 약속
    /// </summary>
    public static string _SkillMessage = "ActiveSkill";

    [SerializeField] private Character mCharacter;

    [Header("Preloaded objects into the scene")]
    [SerializeField] private GameObject[] mObjects;

    [Space(30)]
    [Header("인벤토리")]
    [SerializeField] private EquipmentInventory mEquipmentInventory;
    [SerializeField] private InventoryMain mMainInventory;

    /// <summary>
    /// 아이템 사용 이벤트 호출
    /// 각 아이템마다 실행되는 기능을 수행
    /// </summary>
    /// <param name="item"></param>
    /// <returns>실행이 정상적으로 이루어 졌는가?</returns>
    public bool UseItem(Item item, InventorySlot calledSlot = null)
    {
        Debug.Log("UseItemEvent");

        switch (item.Type)
        {
            //스킬을 사용한경우라면?
            //case itemtype.skill:
            //    {

            //    }

            case ItemType.Consumable:
                {
                    switch (item.ItemID)
                    {
                        case (int)ItemCode.SMALL_HEALTH_POTION:
                            {
                                //GameManager._instance.player.curHealth += 1;
                                //SoundManager.Instance.PlaySound2D("Food Drink " + SoundManager.Range(1, 4, true));
                                break;
                            }
                        case (int)ItemCode.SMALL_MANA_POTION:
                            {
                                //GameManager._instance.player.curHealth -= 1;
                                //SoundManager.Instance.PlaySound2D("Food Drink " + SoundManager.Range(1, 4, true));
                                break;
                            }
                    }

                    break;
                }
            case ItemType.Equipment_NORMAL:
            case ItemType.Equipment_ARMORPLATE:
            case ItemType.Equipment_GLOVE:
            case ItemType.Equipment_PANTS:
            case ItemType.Equipment_SHOES:
                {
                    //case 1: 아이템 사용을 호출한 슬롯이 장비창이라면?
                    //장비를 장착 해제해야한다
                    if (Item.CheckEquipmentType(calledSlot.mSlotMask))
                    {
                        //인벤토리 슬롯에서 아이템을 획득할 수 있는지 확인
                        InventorySlot mainSlot = mMainInventory.IsCanAquireItem(item);

                        if (mainSlot != null)
                        {
                            calledSlot.ClearSlot(); //장비 아이템을 장착 해제
                            //mMainInventory.AcquireItem(item, mainSlot); //현재 장비 아이템을 인벤토리에 획득
                        }
                    }
                    //case 2: 아이템 사용을 호출한 슬롯이 인벤토리라면?
                    //장비를 장착해야한다.
                    else
                    {
                        Debug.Log("give");
                        Debug.Log(item);
                        //장비 인벤토리에서 현재 유형에 맞는 인벤토리 가져오기
                        InventorySlot equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);

                        //이미 장착중인 아이템을 스왑을 위해 임시 저장
                        Item tempItem = equipmentSlot.Item;

                        //호출한 아이템으로 장비 아이템을 변경
                        equipmentSlot.AddItem(item);

                        //호출한 슬롯에 아이템을 추가(착용하던 기존 아이템)하거나 지움
                        if (tempItem != null) { calledSlot.AddItem(tempItem); }
                        else { calledSlot.ClearSlot(); }
                    }

                    //장비 착용 효과음 재생
                    //SoundManager.Instance.PlaySound2D("Equipment " + SoundManager.Range(1, 2));

                    //장비 효과 계산
                    mEquipmentInventory.CalculateEffect();
                    break;
                }
        }

        return true;
    }

    /// <summary>
    /// 씬 내에서 아이템을 줍거나, NONE타입(줍지 않고, 상호작용 전용) 아이템과 상호작용한경우 실행되는 함수
    /// </summary>
    /// <param name="itemID">해당 아이템의 코드</param>
    /// <param name="interactTarget"></param>
    //public void InteractionItem(Item item, GameObject interactTarget)
    //{
    //    Debug.Log("InteractionItemEvent");

    //    if (interactTarget.tag == "NPC")
    //    {
    //        //NPC FSM 가져오기
    //        NPCBase targetNPC = interactTarget.GetComponent<NPCBase>();

    //        //현재 상호작용이 불가능한 대상이라면 리턴
    //        if (!targetNPC.CanInteraction || targetNPC.IsQuotePlaying) { return; }

    //        //상호작용 메시지 보냄
    //        //MessageDispatcher.Instance.DispatchMessage(0, "", targetNPC.EntityName, "Interaction");
    //        return;
    //    }
    //}

    /// <summary>
    /// 아이템을 슬롯에 드롭하는경우 발생하는 이벤트이다.
    /// </summary>
    /// <param name="slot">드롭된 슬롯</param>
    public void SlotOnDropEvent(InventorySlot slot)
    {
        Debug.Log("SlotOnDropEvent");
    }
}

public enum ItemCode
{
    SMALL_HEALTH_POTION,
    SMALL_MANA_POTION,
}


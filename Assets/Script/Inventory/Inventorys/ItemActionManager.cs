using UnityEngine;
using static System.Net.WebRequestMethods;


/// <summary>
/// �� ���� �Ŵ��� ������Ʈ�� �Ҵ�
/// ������(�Ǵ� ���� ��ü)�� ��ȣ�ۿ��ϰų�, �κ��丮���� �������� ����ϸ� Ư�� �̺�Ʈ�� �߻���Ŵ
/// </summary>
public class ItemActionManager : MonoBehaviour
{
    /// <summary>
    /// �޽����� �ְ�޴°�� ��ų�� ���� �޽��� ���
    /// </summary>
    public static string _SkillMessage = "ActiveSkill";

    [SerializeField] private Character mCharacter;

    [Header("Preloaded objects into the scene")]
    [SerializeField] private GameObject[] mObjects;

    [Space(30)]
    [Header("�κ��丮")]
    [SerializeField] private EquipmentInventory mEquipmentInventory;
    [SerializeField] private InventoryMain mMainInventory;

    /// <summary>
    /// ������ ��� �̺�Ʈ ȣ��
    /// �� �����۸��� ����Ǵ� ����� ����
    /// </summary>
    /// <param name="item"></param>
    /// <returns>������ ���������� �̷�� ���°�?</returns>
    public bool UseItem(Item item, InventorySlot calledSlot = null)
    {
        Debug.Log("UseItemEvent");

        switch (item.Type)
        {
            //��ų�� ����Ѱ����?
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
                    //case 1: ������ ����� ȣ���� ������ ���â�̶��?
                    //��� ���� �����ؾ��Ѵ�
                    if (Item.CheckEquipmentType(calledSlot.mSlotMask))
                    {
                        //�κ��丮 ���Կ��� �������� ȹ���� �� �ִ��� Ȯ��
                        InventorySlot mainSlot = mMainInventory.IsCanAquireItem(item);

                        if (mainSlot != null)
                        {
                            calledSlot.ClearSlot(); //��� �������� ���� ����
                            //mMainInventory.AcquireItem(item, mainSlot); //���� ��� �������� �κ��丮�� ȹ��
                        }
                    }
                    //case 2: ������ ����� ȣ���� ������ �κ��丮���?
                    //��� �����ؾ��Ѵ�.
                    else
                    {
                        Debug.Log("give");
                        Debug.Log(item);
                        //��� �κ��丮���� ���� ������ �´� �κ��丮 ��������
                        InventorySlot equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);

                        //�̹� �������� �������� ������ ���� �ӽ� ����
                        Item tempItem = equipmentSlot.Item;

                        //ȣ���� ���������� ��� �������� ����
                        equipmentSlot.AddItem(item);

                        //ȣ���� ���Կ� �������� �߰�(�����ϴ� ���� ������)�ϰų� ����
                        if (tempItem != null) { calledSlot.AddItem(tempItem); }
                        else { calledSlot.ClearSlot(); }
                    }

                    //��� ���� ȿ���� ���
                    //SoundManager.Instance.PlaySound2D("Equipment " + SoundManager.Range(1, 2));

                    //��� ȿ�� ���
                    mEquipmentInventory.CalculateEffect();
                    break;
                }
        }

        return true;
    }

    /// <summary>
    /// �� ������ �������� �ݰų�, NONEŸ��(���� �ʰ�, ��ȣ�ۿ� ����) �����۰� ��ȣ�ۿ��Ѱ�� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="itemID">�ش� �������� �ڵ�</param>
    /// <param name="interactTarget"></param>
    //public void InteractionItem(Item item, GameObject interactTarget)
    //{
    //    Debug.Log("InteractionItemEvent");

    //    if (interactTarget.tag == "NPC")
    //    {
    //        //NPC FSM ��������
    //        NPCBase targetNPC = interactTarget.GetComponent<NPCBase>();

    //        //���� ��ȣ�ۿ��� �Ұ����� ����̶�� ����
    //        if (!targetNPC.CanInteraction || targetNPC.IsQuotePlaying) { return; }

    //        //��ȣ�ۿ� �޽��� ����
    //        //MessageDispatcher.Instance.DispatchMessage(0, "", targetNPC.EntityName, "Interaction");
    //        return;
    //    }
    //}

    /// <summary>
    /// �������� ���Կ� ����ϴ°�� �߻��ϴ� �̺�Ʈ�̴�.
    /// </summary>
    /// <param name="slot">��ӵ� ����</param>
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


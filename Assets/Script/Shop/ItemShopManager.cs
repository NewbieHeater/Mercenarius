using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ItemShopManager : Singleton<ItemShopManager>
{
    private static bool mIsItemShopActive = false;
    public static bool IsItemShopActive
    {
        get
        {
            return mIsItemShopActive;
        }
    }
    //public RectTransform
    [Header("���� ������Ʈ ��Ʈ ������Ʈ")]
    [SerializeField] public GameObject mShopRootGo;

    

    [Header("���� ���� ������")]
    [SerializeField] public GameObject mShopSlotPrefab;


    private List<ItemShopSlot> mCurrentSlots = new List<ItemShopSlot>(); // ���� �ν��Ͻ��� ���Ե�
    private ItemShopSlot slot;
    private void Awake()
    {
        // �ʱ�ȭ�� ���� Ȱ��ȭ���� ����
        ItemShopManager.mIsItemShopActive = false;
    }

    /// <summary>
    /// �������κ��� ȣ��Ǿ� ������ ���� ���̾�α׸� ����
    /// </summary>
    /// <param name="sellItems">�������� �Ǹ��ϴ� �����۵�</param>
    /// <param name="shopLevel">������ ��ô�� ����</param>
    public void OpenShop(ItemShopSlotInfo[] sellItems, int shopLevel, int itemNumber, Vector3 parent, ItemShopSlot slot)
    {
        //ItemShopSlot slot = Instantiate(mShopSlotPrefab, parent, Quaternion.Euler(45, 0, 0), mSlotInstantiateTransform).GetComponent<ItemShopSlot>();
        slot.InitSlot(sellItems[itemNumber], shopLevel);
        mCurrentSlots.Add(slot);
        mShopRootGo.SetActive(true);
        ItemShopManager.Instance.RefreshSlots();
        mIsItemShopActive = true;
    }
    public void ItemShopInteractEnter(ItemShopSlot slot)
    {
        Debug.Log("�۵�������");
        slot.InteractionManageEnter();
    }
    public void ItemShopInteractExit(ItemShopSlot slot)
    {
        Debug.Log("�۵�������");
        slot.InteractionManageExit();
    }

    /// <summary>
    /// ���� ���̾�α׸� ����
    /// </summary>
    public void CloseItemShop()
    {
        //GameManager.Instance.isUIOpen = false;
        foreach (ItemShopSlot slot in mCurrentSlots)
            Destroy(slot.gameObject);

        mCurrentSlots.Clear();
        mShopRootGo.SetActive(false);

        // ��Ȱ��ȭ ���
        mIsItemShopActive = false;

        //UtilityManager.TryLockCursor();
    }

    /// <summary>
    /// ���� ���̾�α׸� ����
    /// </summary>
    public void RefreshSlots()
    {
        // �� ���� ���� ȣ��
        foreach (ItemShopSlot slot in mCurrentSlots)
            slot.RefreshSlot();

        // �� ����
        InventoryMain.Instance.RefreshLabels();
    }

    public void BuyItem(ItemShopSlot slot)
    {
        slot.Buy();
    }
}
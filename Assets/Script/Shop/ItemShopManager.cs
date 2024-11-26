using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("���� ������Ʈ�� ������ �ν��Ͻ� Ʈ������")]
    [SerializeField] public RectTransform mSlotInstantiateTransform;

    [Header("���� ���� ������")]
    [SerializeField] public GameObject mShopSlotPrefab;

    private List<ItemShopSlot> mCurrentSlots = new List<ItemShopSlot>(); // ���� �ν��Ͻ��� ���Ե�

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
    public void OpenItemShop(ItemShopSlotInfo[] sellItems, int shopLevel,int itemNumber, Transform parent)
    {
        Debug.Log(mSlotInstantiateTransform);
        ItemShopSlot slot = Instantiate(mShopSlotPrefab, Vector3.zero, Quaternion.identity, mSlotInstantiateTransform).GetComponent<ItemShopSlot>();
        slot.InitSlot(sellItems[itemNumber], shopLevel);

        mCurrentSlots.Add(slot);
        mShopRootGo.transform.SetParent(parent, false);
        mShopRootGo.transform.LookAt(Camera.main.transform.position, Vector3.up);
        mShopRootGo.SetActive(true);

        // ��� ������ ����
        ItemShopManager.Instance.RefreshSlots();

        // Ȱ��ȭ ���
        mIsItemShopActive = true;

        //UtilityManager.UnlockCursor();
    }

    /// <summary>
    /// ���� ���̾�α׸� ����
    /// </summary>
    public void CloseItemShop()
    {
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
}
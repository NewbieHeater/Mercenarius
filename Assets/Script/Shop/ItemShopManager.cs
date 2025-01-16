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
    [Header("상점 오브젝트 루트 오브젝트")]
    [SerializeField] public GameObject mShopRootGo;

    

    [Header("상점 슬롯 프리팹")]
    [SerializeField] public GameObject mShopSlotPrefab;


    private List<ItemShopSlot> mCurrentSlots = new List<ItemShopSlot>(); // 현재 인스턴스된 슬롯들
    private ItemShopSlot slot;
    private void Awake()
    {
        // 초기화시 전역 활성화상태 해제
        ItemShopManager.mIsItemShopActive = false;
    }

    /// <summary>
    /// 상점으로부터 호출되어 아이템 상점 다이얼로그를 열음
    /// </summary>
    /// <param name="sellItems">상점에서 판매하는 아이템들</param>
    /// <param name="shopLevel">상점의 진척도 레벨</param>
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
        Debug.Log("작동ㅇ시작");
        slot.InteractionManageEnter();
    }
    public void ItemShopInteractExit(ItemShopSlot slot)
    {
        Debug.Log("작동ㅇ종료");
        slot.InteractionManageExit();
    }

    /// <summary>
    /// 상점 다이얼로그를 닫음
    /// </summary>
    public void CloseItemShop()
    {
        //GameManager.Instance.isUIOpen = false;
        foreach (ItemShopSlot slot in mCurrentSlots)
            Destroy(slot.gameObject);

        mCurrentSlots.Clear();
        mShopRootGo.SetActive(false);

        // 비활성화 토글
        mIsItemShopActive = false;

        //UtilityManager.TryLockCursor();
    }

    /// <summary>
    /// 상점 다이얼로그를 갱신
    /// </summary>
    public void RefreshSlots()
    {
        // 각 슬롯 갱신 호출
        foreach (ItemShopSlot slot in mCurrentSlots)
            slot.RefreshSlot();

        // 라벨 갱신
        InventoryMain.Instance.RefreshLabels();
    }

    public void BuyItem(ItemShopSlot slot)
    {
        slot.Buy();
    }
}
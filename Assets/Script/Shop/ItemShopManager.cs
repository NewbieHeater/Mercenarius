using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopManager : Singleton<ItemShopManager>
{
    private static bool mIsItemShopActive = false;
    public static bool IsItemShopActive
    {
        get { return mIsItemShopActive; }
    }

    [Header("상점 오브젝트 루트 오브젝트")]
    [SerializeField] public GameObject mShopRootGo;
    [Header("상점 슬롯 프리팹")]
    [SerializeField] public GameObject mShopSlotPrefab;

    private List<ItemShopSlot> mCurrentSlots = new List<ItemShopSlot>(); // 현재 인스턴스된 슬롯들

    private void Awake()
    {
        // 초기화 시 전역 활성화 상태 해제
        mIsItemShopActive = false;
    }

    /// <summary>
    /// 상점을 열 때 호출하는 함수.
    /// sellItems 배열에서 itemNumber 인덱스의 아이템 정보를 사용합니다.
    /// </summary>
    public void OpenShop(ItemShopSlotInfo[] sellItems, int shopLevel, int itemNumber, Vector3 parent, ItemShopSlot slot)
    {
        if (sellItems == null || sellItems.Length == 0)
        {
            Debug.LogError("sellItems 배열이 비어 있습니다.");
            return;
        }
        // itemNumber가 sellItems 배열 범위를 벗어나면 안전하게 보정 (모듈로 연산)
        int index = itemNumber % sellItems.Length;
        slot.InitSlot(sellItems[index], shopLevel);
        mCurrentSlots.Add(slot);
        //mShopRootGo.SetActive(true);
        RefreshSlots();
        mIsItemShopActive = true;
    }

    public void ItemShopInteractEnter(ItemShopSlot slot)
    {
        Debug.Log("ItemShop: 상호작용 시작");
        slot.InteractionManageEnter();
    }

    public void ItemShopInteractExit(ItemShopSlot slot)
    {
        Debug.Log("ItemShop: 상호작용 종료");
        slot.InteractionManageExit();
    }

    /// <summary>
    /// 상점 다이얼로그를 닫습니다.
    /// </summary>
    public void CloseItemShop()
    {
        foreach (ItemShopSlot slot in mCurrentSlots)
            Destroy(slot.gameObject);

        mCurrentSlots.Clear();
        mShopRootGo.SetActive(false);
        mIsItemShopActive = false;
    }

    /// <summary>
    /// 상점 다이얼로그를 갱신합니다.
    /// </summary>
    public void RefreshSlots()
    {
        foreach (ItemShopSlot slot in mCurrentSlots)
            slot.RefreshSlot();

        InventoryMain.Instance.RefreshLabels();
    }

    public void BuyItem(ItemShopSlot slot)
    {
        slot.Buy();
        Item item = slot.Item;
        if (item is Item_Equipment equipment)
        {
            Debug.Log("ItemShop: 장비 효과 발동");
            Character playerCharacter = CharacterManager.Instance.character.GetComponent<Character>();
            EquipmentInventory.Instance.AddAndEquipItem(equipment, playerCharacter);
        }
    }
}

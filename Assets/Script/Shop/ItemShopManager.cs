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
    public void OpenShop(ItemShopSlotInfo[] sellItems, int shopLevel, int itemNumber, Vector3 parent, ItemShopSlot slot)
    {
        
        slot.InitSlot(sellItems[itemNumber], shopLevel);
        mCurrentSlots.Add(slot);
        //mShopRootGo.SetActive(true);
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
        Item item = slot.Item;
        if (item is Item_Equipment equipment)
        {
            Debug.Log("효과발동");
            // 플레이어 캐릭터를 가져옵니다.
            Character playerCharacter = CharacterManager.Instance.character.GetComponent<Character>();
            // 새 장비를 추가하고 효과 적용
            EquipmentInventory.Instance.AddAndEquipItem(equipment, playerCharacter);
        }
    }
}
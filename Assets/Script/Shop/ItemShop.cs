using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameSave;

public class ItemShop : MonoBehaviour
{
    [field: Header("아이템 상점의 고유번호")]
    [field: SerializeField] public int ShopId { private set; get; } = 0;

    [Header("상점에서 판매할 아이템")]
    [SerializeField] public ItemShopSlotInfo[] mSellItemInfos;

    [HideInInspector] public int ShopLevel = 0; // 상점의 아이템 판매 단계

    private void Awake()
    {
        // 인스턴스로 생성하여 삽입
        List<ItemShopSlotInfo> itemShopSlotInfos = new List<ItemShopSlotInfo>();
        foreach (ItemShopSlotInfo shotSlotInfo in mSellItemInfos)
            itemShopSlotInfos.Add(new ItemShopSlotInfo(shotSlotInfo));

        // 배열 재구성
        mSellItemInfos = itemShopSlotInfos.ToArray();
    }

    /// <summary>
    /// 게임이 로드되는경우 상점 정보를 불러옴
    /// </summary>
    /// <param name="shopInfo">불러올 상점 정보</param>
    public void LoadFromData(ShopInfo shopInfo)
    {
        for (int i = 0; i < mSellItemInfos.Length; ++i)
            mSellItemInfos[i].ItemAmount = shopInfo.sellItemInfos[i].itemAmount;

        ShopLevel = shopInfo.shopLevel;
    }

    /// <summary>
    /// 게임을 저장하는경우 상점의 정보를 내보냄
    /// </summary>
    /// <returns>저장하고자 하는 상점의 정보</returns>
    public ShopInfo GetShopGameData()
    {
        ShopInfo shopInfo = new ShopInfo();

        shopInfo.shopId = this.ShopId;
        shopInfo.shopLevel = this.ShopLevel;
        shopInfo.sellItemInfos = mSellItemInfos
                                    .Select(sellItemInfo =>
                                    new SellItemInfo
                                    {
                                        itemAmount = sellItemInfo.ItemAmount,
                                        sellItemEntityCode = (int)sellItemInfo.SellItem.ItemID
                                    }).ToArray();

        return shopInfo;
    }
}

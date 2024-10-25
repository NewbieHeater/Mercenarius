using UnityEngine;

[System.Serializable]
public class ItemShopSlotInfo
{
    [Header("판매할 아이템")]
    [SerializeField] public Item SellItem; // 판매할 아이템

    [Header("거래 한번당 아이템의 비용")]
    [SerializeField] public int Cost; // 아이템의 비용

    [Header("아이템의 총 개수 (재고)")]
    [SerializeField] public int ItemAmount; // 아이템의 총 개수(재고량)

    [Header("거래 1회당 지급할 아이템 개수")]
    [SerializeField] public int GiveAmountPerTrade; // 거래 한번당 넘겨줄 아이템 개수

    [Header("아이템 판매 가능 단계 (상점의 진척도 단계)")]
    [SerializeField] public int NeedShopLevel; // 요구 단계

    public ItemShopSlotInfo(ItemShopSlotInfo origin)
    {
        this.SellItem = origin.SellItem;
        this.Cost = origin.Cost;
        this.ItemAmount = origin.ItemAmount;
        this.GiveAmountPerTrade = origin.GiveAmountPerTrade;
        this.NeedShopLevel = origin.NeedShopLevel;
    }
}

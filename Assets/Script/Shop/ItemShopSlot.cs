using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemShopSlot : MonoBehaviour
{
    [SerializeField] private InventorySlot mItemSlot;
    [SerializeField] private TextMeshProUGUI mNameLabel, mCostLabel;
    [SerializeField] private Button mBuyButton;

    private ItemShopSlotInfo mSellInfo; // 현재 판매중인 아이템의 정보
    private int mCalledShopLevel; // 현재 상점의 판매 단계

    public void RefreshSlot()
    {
        // 아이템의 요구 단계가 현재 상점의 판매 단계가 보다 높으면?
        if (mSellInfo.NeedShopLevel > mCalledShopLevel)
        {
            mCostLabel.text = "지금은 구매할 수 없습니다.";
            mBuyButton.interactable = false;

            return;
        }

        // 구매버튼 비활성화
        if (InventoryMain.Instance.CurrentCoin < mSellInfo.Cost
            || mSellInfo.ItemAmount <= 0)
            mBuyButton.interactable = false;
        else
            mBuyButton.interactable = true;

        // 텍스트 갱신
        mCostLabel.text = $"{mSellInfo.Cost} ({mSellInfo.ItemAmount}개 남음)";

    }

    public void InitSlot(ItemShopSlotInfo sellItem, int shopLevel)
    {
        // 정보 가져오기
        mCalledShopLevel = shopLevel;
        mSellInfo = sellItem;

        // 슬롯 초기화
        mItemSlot.ClearSlot();

        // 슬롯 설정
        
        InventoryMain.Instance.AcquireItem(mSellInfo.SellItem, mItemSlot, mSellInfo.GiveAmountPerTrade);
        mNameLabel.text = ItemDataManager.Instance.GetName(mSellInfo.SellItem.ItemID);
    }

    public void BTN_BuyItem()
    {
        // 가격 지불
        InventoryMain.Instance.CurrentCoin -= mSellInfo.Cost;
        Debug.Log(InventoryMain.Instance.CurrentCoin);
        // 인벤토리에 아이템 추가(구매)
        InventoryMain.Instance.AcquireItem(mSellInfo.SellItem, mSellInfo.GiveAmountPerTrade < mSellInfo.ItemAmount ? mSellInfo.GiveAmountPerTrade : mSellInfo.ItemAmount);

        // 개수 갱신
        mSellInfo.ItemAmount -= mSellInfo.GiveAmountPerTrade;
        mSellInfo.ItemAmount = Mathf.Clamp(mSellInfo.ItemAmount, 0, int.MaxValue);

        // 모든 슬롯을 갱신
        ItemShopManager.Instance.RefreshSlots();

        // 사운드 재생
        //SoundManager.Instance.PlaySound2D("Item purchase " + SoundManager.Range(1, 3));
    }
}

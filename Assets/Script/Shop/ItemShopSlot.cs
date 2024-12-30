using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemShopSlot : MonoBehaviour
{
    [SerializeField] private InventorySlot mItemSlot;
    [SerializeField] private TextMeshProUGUI mCostLabel;
    [SerializeField] private TextMeshProUGUI mItemShopInteract;
    private bool interactable = false;
    private bool interactOn = false;
    private ItemShopSlotInfo mSellInfo; // 현재 판매중인 아이템의 정보
    private int mCalledShopLevel; // 현재 상점의 판매 단계

    private void Awake()
    {
        mItemShopInteract.enabled = false;
    }
    public void InteractionManage()
    {
        if (interactOn)
        {
            mItemShopInteract.enabled = false;
            interactOn = false;
        }
        else
        {
            mItemShopInteract.enabled = true;
            interactOn = true;
        }
    }
    public void RefreshSlot()
    {
        // 아이템의 요구 단계가 현재 상점의 판매 단계가 보다 높으면?
        if (mSellInfo.NeedShopLevel > mCalledShopLevel)
        {
            mCostLabel.text = "지금은 구매할 수 없습니다.";
            interactable = false;

            return;
        }

        // 구매버튼 비활성화
        if (InventoryMain.Instance.CurrentCoin < mSellInfo.Cost
            || mSellInfo.ItemAmount <= 0)
            interactable = false;
        else
            interactable = true;

        // 텍스트 갱신
        mCostLabel.text = $"{mSellInfo.Cost} 골드";

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

    }

    public void Buy()
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

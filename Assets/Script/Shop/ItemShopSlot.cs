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

    private ItemShopSlotInfo mSellInfo; // ���� �Ǹ����� �������� ����
    private int mCalledShopLevel; // ���� ������ �Ǹ� �ܰ�

    public void RefreshSlot()
    {
        // �������� �䱸 �ܰ谡 ���� ������ �Ǹ� �ܰ谡 ���� ������?
        if (mSellInfo.NeedShopLevel > mCalledShopLevel)
        {
            mCostLabel.text = "������ ������ �� �����ϴ�.";
            mBuyButton.interactable = false;

            return;
        }

        // ���Ź�ư ��Ȱ��ȭ
        if (InventoryMain.Instance.CurrentCoin < mSellInfo.Cost
            || mSellInfo.ItemAmount <= 0)
            mBuyButton.interactable = false;
        else
            mBuyButton.interactable = true;

        // �ؽ�Ʈ ����
        mCostLabel.text = $"{mSellInfo.Cost} ({mSellInfo.ItemAmount}�� ����)";

    }

    public void InitSlot(ItemShopSlotInfo sellItem, int shopLevel)
    {
        // ���� ��������
        mCalledShopLevel = shopLevel;
        mSellInfo = sellItem;

        // ���� �ʱ�ȭ
        mItemSlot.ClearSlot();

        // ���� ����
        
        InventoryMain.Instance.AcquireItem(mSellInfo.SellItem, mItemSlot, mSellInfo.GiveAmountPerTrade);
        mNameLabel.text = ItemDataManager.Instance.GetName(mSellInfo.SellItem.ItemID);
    }

    public void BTN_BuyItem()
    {
        // ���� ����
        InventoryMain.Instance.CurrentCoin -= mSellInfo.Cost;
        Debug.Log(InventoryMain.Instance.CurrentCoin);
        // �κ��丮�� ������ �߰�(����)
        InventoryMain.Instance.AcquireItem(mSellInfo.SellItem, mSellInfo.GiveAmountPerTrade < mSellInfo.ItemAmount ? mSellInfo.GiveAmountPerTrade : mSellInfo.ItemAmount);

        // ���� ����
        mSellInfo.ItemAmount -= mSellInfo.GiveAmountPerTrade;
        mSellInfo.ItemAmount = Mathf.Clamp(mSellInfo.ItemAmount, 0, int.MaxValue);

        // ��� ������ ����
        ItemShopManager.Instance.RefreshSlots();

        // ���� ���
        //SoundManager.Instance.PlaySound2D("Item purchase " + SoundManager.Range(1, 3));
    }
}

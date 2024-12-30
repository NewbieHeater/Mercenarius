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
    private ItemShopSlotInfo mSellInfo; // ���� �Ǹ����� �������� ����
    private int mCalledShopLevel; // ���� ������ �Ǹ� �ܰ�

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
        // �������� �䱸 �ܰ谡 ���� ������ �Ǹ� �ܰ谡 ���� ������?
        if (mSellInfo.NeedShopLevel > mCalledShopLevel)
        {
            mCostLabel.text = "������ ������ �� �����ϴ�.";
            interactable = false;

            return;
        }

        // ���Ź�ư ��Ȱ��ȭ
        if (InventoryMain.Instance.CurrentCoin < mSellInfo.Cost
            || mSellInfo.ItemAmount <= 0)
            interactable = false;
        else
            interactable = true;

        // �ؽ�Ʈ ����
        mCostLabel.text = $"{mSellInfo.Cost} ���";

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

    }

    public void Buy()
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameSave;

public class ItemShop : MonoBehaviour
{
    [field: Header("������ ������ ������ȣ")]
    [field: SerializeField] public int ShopId { private set; get; } = 0;

    [Header("�������� �Ǹ��� ������")]
    [SerializeField] public ItemShopSlotInfo[] mSellItemInfos;

    [HideInInspector] public int ShopLevel = 0; // ������ ������ �Ǹ� �ܰ�

    private void Awake()
    {
        // �ν��Ͻ��� �����Ͽ� ����
        List<ItemShopSlotInfo> itemShopSlotInfos = new List<ItemShopSlotInfo>();
        foreach (ItemShopSlotInfo shotSlotInfo in mSellItemInfos)
            itemShopSlotInfos.Add(new ItemShopSlotInfo(shotSlotInfo));

        // �迭 �籸��
        mSellItemInfos = itemShopSlotInfos.ToArray();
    }

    /// <summary>
    /// ������ �ε�Ǵ°�� ���� ������ �ҷ���
    /// </summary>
    /// <param name="shopInfo">�ҷ��� ���� ����</param>
    public void LoadFromData(ShopInfo shopInfo)
    {
        for (int i = 0; i < mSellItemInfos.Length; ++i)
            mSellItemInfos[i].ItemAmount = shopInfo.sellItemInfos[i].itemAmount;

        ShopLevel = shopInfo.shopLevel;
    }

    /// <summary>
    /// ������ �����ϴ°�� ������ ������ ������
    /// </summary>
    /// <returns>�����ϰ��� �ϴ� ������ ����</returns>
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

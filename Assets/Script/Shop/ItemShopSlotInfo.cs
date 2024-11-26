using UnityEngine;

[System.Serializable]
public class ItemShopSlotInfo
{
    [Header("�Ǹ��� ������")]
    [SerializeField] public Item SellItem; // �Ǹ��� ������

    [Header("�ŷ� �ѹ��� �������� ���")]
    [SerializeField] public int Cost; // �������� ���

    [Header("�������� �� ���� (���)")]
    [SerializeField] public int ItemAmount; // �������� �� ����(���)

    [Header("�ŷ� 1ȸ�� ������ ������ ����")]
    [SerializeField] public int GiveAmountPerTrade; // �ŷ� �ѹ��� �Ѱ��� ������ ����

    [Header("������ �Ǹ� ���� �ܰ� (������ ��ô�� �ܰ�)")]
    [SerializeField] public int NeedShopLevel; // �䱸 �ܰ�

    public ItemShopSlotInfo(ItemShopSlotInfo origin)
    {
        this.SellItem = origin.SellItem;
        this.Cost = origin.Cost;
        this.ItemAmount = origin.ItemAmount;
        this.GiveAmountPerTrade = origin.GiveAmountPerTrade;
        this.NeedShopLevel = origin.NeedShopLevel;
    }
}

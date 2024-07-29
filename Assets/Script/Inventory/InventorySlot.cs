using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �κ��丮 ���� �ϳ��� ���
/// </summary>
public class InventorySlot : MonoBehaviour
{
    private Item mItem; //���� ������ �ν��Ͻ�
    public Item Item
    {
        get
        {
            return mItem;
        }
    }

    [Header("�ش� ���Կ� ��� Ÿ�Ը� ���� �� �ִ��� Ÿ�� ����ũ")]
    [SerializeField] private ItemType mSlotMask;
    [SerializeField]
    private int mItemCount = 0; //ȹ���� �������� ����


    [Header("������ ���Կ� �ִ� UI ������Ʈ")]
    [SerializeField] private Image mItemImage; //�������� �̹���
    [SerializeField] private Image mCooltimeImage; //������ ��Ÿ�� �̹���
    [SerializeField] private Text mTextCount; //�������� ���� �ؽ�Ʈ


    // ������ �̹����� ���� ����
    private void SetColor(float _alpha)
    {
        Color color = mItemImage.color;
        color.a = _alpha;
        mItemImage.color = color;
    }

    /// <summary>
    /// mSlotMask���� ������ ���� ���� ��Ʈ�������Ѵ�.
    /// ���� ����ũ���� ��Ʈ�������� 0�� ���´ٸ� ���� ���Կ� ����ũ�� ��ġ���� �ʴ´ٴ� ��.
    /// 0�� �ƴ� ���� ���� ��Ʈ��ġ(10������ 1, 2, 4, 8)�� ���� ���´�.
    /// </summary>
    public bool IsMask(Item item)
    {
        return ((int)item.Type & (int)mSlotMask) == 0 ? false : true;
    }

    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(Item item, int count = 1)
    {
        mItem = item;
        mItemCount = count;
        mItemImage.sprite = mItem.Image;

        if (mItem.Type <= ItemType.Equipment_SHOES)
        {
            mTextCount.text = "";
        }
        else
        {
            if(mItemCount > 1)
            {
                mTextCount.text = mItemCount.ToString();
            }
            else
            {
                mTextCount.text = "";
            }
        }

        SetColor(1);
    }

    // �ش� ������ ������ ���� ������Ʈ
    public void UpdateSlotCount(int count)
    {
        mItemCount += count;
        if (mItemCount > 1)
        {
            mTextCount.text = mItemCount.ToString();
        }
        else
        {
            mTextCount.text = "";
        }
        
        if (mItemCount <= 0)
            ClearSlot();
    }

    // �ش� ���� �ϳ� ����
    public void ClearSlot()
    {
        mItem = null;
        mItemCount = 0;
        mItemImage.sprite = null;
        SetColor(0);

        mTextCount.text = "";
    }
}

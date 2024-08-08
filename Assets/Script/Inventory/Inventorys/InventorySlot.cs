using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �κ��丮 ���� �ϳ��� ���
/// </summary>
public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (mItem != null)
        {
            //����Ʈ (������ ������ ��� Ȱ��ȭ)
            if (Input.GetKey(KeyCode.LeftShift)) { DragSlot.Instance.IsShiftMode = true; }
            else { DragSlot.Instance.IsShiftMode = false; }

            //���� �������� ���
            DragSlot.Instance.CurrentSlot = this;
            DragSlot.Instance.DragSetImage(mItemImage);
            DragSlot.Instance.transform.position = eventData.position;
        }
    }

    // ���콺 �巡�� �� �� �����Ӹ��� ȣ��Ǵ� �������̵�
    public void OnDrag(PointerEventData eventData)
    {
        if (mItem != null)
            DragSlot.Instance.transform.position = eventData.position;
    }

    // ���콺 �巡�� ���� �������̵�
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.Instance.SetColor(0);
        DragSlot.Instance.CurrentSlot = null;
    }

    // �ش� ���Կ� ���𰡰� ���콺 ��� ���� �� �߻��ϴ� �̺�Ʈ
    public void OnDrop(PointerEventData eventData)
    {
        //����Ʈ ����� ��Ȳ���� �ش� ��ġ�� �������� �ִ°��, ������ ���� �� ���⿡ �����Ѵ�.
        if (DragSlot.Instance.IsShiftMode && mItem != null) { return; }

        //�巡�� ���Կ� ������ �����۰�, �ٲ��� �������� ����ũ�� ��� ����Ǹ� �ٲ۴�.
        if (!IsMask(DragSlot.Instance.CurrentSlot.Item)) { return; }

        //Ÿ�� �巡�� ���Կ� �̹� �������� �ִ°��, �ش� �������� ������ ������ ���Կ��� ����ũ�� üũ�Ѵ�.
        if (mItem != null && !DragSlot.Instance.CurrentSlot.IsMask(mItem)) { return; }

        ChangeSlot();

        //mItemActionManager.SlotOnDropEvent(this); ��� �̺�Ʈ ȣ��
    }
    private void ChangeSlot()
    {
        //if (DragSlot.Instance.CurrentSlot.Item.Type >= ItemType.Etc)
        //{
        //    //����Ʈ ��� ���� ���� �Ͼ �� �ִ� ���
        //    //���ο� ���԰� ���� ������ ������ID�� ������� ������ ��ģ��.
        //    if (mItem != null && mItem.ItemID == DragSlot.Instance.CurrentSlot.Item.ItemID)
        //    {
        //        int changedSlotCount;
        //        if (DragSlot.Instance.IsShiftMode) { changedSlotCount = (int)(DragSlot.Instance.CurrentSlot.mItemCount * 0.5f); }
        //        else { changedSlotCount = DragSlot.Instance.CurrentSlot.mItemCount; }

        //        UpdateSlotCount(changedSlotCount);
        //        DragSlot.Instance.CurrentSlot.UpdateSlotCount(-changedSlotCount);
        //        return;
        //    }

        //    //����Ʈ ����ΰ�� ������ ������ ������.
        //    if (DragSlot.Instance.IsShiftMode)
        //    {
        //        //changedSlotCount ������ŭ ���ϰ� �����̴� (+�� -�� ���̰� 0�̾�� �������� ����, ���ǵ��� �ʴ´�.)
        //        int changedSlotCount = (int)(DragSlot.Instance.CurrentSlot.mItemCount * 0.5f);

        //        //(int)���� ����ȯ���� ���� 0�� �Ǵ� ���� (int)(1 * 0.5f)�̱⿡ �ܼ��� ���ο� �������� �ű��.
        //        if (changedSlotCount == 0)
        //        {
        //            AddItem(DragSlot.Instance.CurrentSlot.Item, 1);
        //            DragSlot.Instance.CurrentSlot.ClearSlot();
        //            return;
        //        }

        //        //�� ��� ��찡 �ƴѰ�� ���ο� ���Կ� ���ο� �������� �����Ѵ�.
        //        AddItem(DragSlot.Instance.CurrentSlot.Item, changedSlotCount);
        //        DragSlot.Instance.CurrentSlot.UpdateSlotCount(-changedSlotCount);
        //        return;
        //    }
        //}

        //���� ���� ��ȯ�ϱ�
        Item tempItem = mItem;
        int tempItemCount = mItemCount;

        AddItem(DragSlot.Instance.CurrentSlot.mItem, DragSlot.Instance.CurrentSlot.mItemCount);

        if (tempItem != null) { DragSlot.Instance.CurrentSlot.AddItem(tempItem, tempItemCount); }
        else { DragSlot.Instance.CurrentSlot.ClearSlot(); }
    }
}

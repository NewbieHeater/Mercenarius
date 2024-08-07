using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 인벤토리 슬롯 하나를 담당
/// </summary>
public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Item mItem; //현재 아이템 인스턴스
    public Item Item
    {
        get
        {
            return mItem;
        }
    }

    [Header("해당 슬롯에 어떠한 타입만 들어올 수 있는지 타입 마스크")]
    [SerializeField] private ItemType mSlotMask;
    [SerializeField]
    private int mItemCount = 0; //획득한 아이템의 개수


    [Header("아이템 슬롯에 있는 UI 오브젝트")]
    [SerializeField] private Image mItemImage; //아이템의 이미지
    [SerializeField] private Image mCooltimeImage; //아이템 쿨타임 이미지
    [SerializeField] private Text mTextCount; //아이템의 개수 텍스트


    // 아이템 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = mItemImage.color;
        color.a = _alpha;
        mItemImage.color = color;
    }

    /// <summary>
    /// mSlotMask에서 설정된 값에 따라 비트연산을한다.
    /// 현재 마스크값이 비트연산으로 0이 나온다면 현재 슬롯에 마스크가 일치하지 않는다는 뜻.
    /// 0이 아닌 수는 현재 비트위치(10진수로 1, 2, 4, 8)로 값이 나온다.
    /// </summary>
    public bool IsMask(Item item)
    {
        return ((int)item.Type & (int)mSlotMask) == 0 ? false : true;
    }

    // 인벤토리에 새로운 아이템 슬롯 추가
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

    // 해당 슬롯의 아이템 개수 업데이트
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

    // 해당 슬롯 하나 삭제
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
            //쉬프트 (반으로 나누기 모드 활성화)
            if (Input.GetKey(KeyCode.LeftShift)) { DragSlot.Instance.IsShiftMode = true; }
            else { DragSlot.Instance.IsShiftMode = false; }

            //현재 슬롯으로 등록
            DragSlot.Instance.CurrentSlot = this;
            DragSlot.Instance.DragSetImage(mItemImage);
            DragSlot.Instance.transform.position = eventData.position;
        }
    }

    // 마우스 드래그 중 매 프레임마다 호출되는 오버라이드
    public void OnDrag(PointerEventData eventData)
    {
        if (mItem != null)
            DragSlot.Instance.transform.position = eventData.position;
    }

    // 마우스 드래그 종료 오버라이드
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.Instance.SetColor(0);
        DragSlot.Instance.CurrentSlot = null;
    }

    // 해당 슬롯에 무언가가 마우스 드롭 됐을 때 발생하는 이벤트
    public void OnDrop(PointerEventData eventData)
    {
        //쉬프트 모드인 상황에서 해당 위치에 아이템이 있는경우, 반으로 나눌 수 없기에 리턴한다.
        if (DragSlot.Instance.IsShiftMode && mItem != null) { return; }

        //드래그 슬롯에 놓여진 아이템과, 바꿔질 아이템의 마스크가 모두 통과되면 바꾼다.
        if (!IsMask(DragSlot.Instance.CurrentSlot.Item)) { return; }

        //타겟 드래그 슬롯에 이미 아이템이 있는경우, 해당 아이템이 직전의 아이템 슬롯에서 마스크를 체크한다.
        if (mItem != null && !DragSlot.Instance.CurrentSlot.IsMask(mItem)) { return; }

        ChangeSlot();

        //mItemActionManager.SlotOnDropEvent(this); 드롭 이벤트 호출
    }
    private void ChangeSlot()
    {
        //if (DragSlot.Instance.CurrentSlot.Item.Type >= ItemType.Etc)
        //{
        //    //쉬프트 모드 관계 없이 일어날 수 있는 경우
        //    //새로운 슬롯과 이전 슬롯의 아이템ID가 같은경우 개수를 합친다.
        //    if (mItem != null && mItem.ItemID == DragSlot.Instance.CurrentSlot.Item.ItemID)
        //    {
        //        int changedSlotCount;
        //        if (DragSlot.Instance.IsShiftMode) { changedSlotCount = (int)(DragSlot.Instance.CurrentSlot.mItemCount * 0.5f); }
        //        else { changedSlotCount = DragSlot.Instance.CurrentSlot.mItemCount; }

        //        UpdateSlotCount(changedSlotCount);
        //        DragSlot.Instance.CurrentSlot.UpdateSlotCount(-changedSlotCount);
        //        return;
        //    }

        //    //쉬프트 모드인경우 개수를 반으로 나눈다.
        //    if (DragSlot.Instance.IsShiftMode)
        //    {
        //        //changedSlotCount 개수만큼 더하고 뺄것이다 (+와 -의 차이가 0이어야 아이템이 복사, 유실되지 않는다.)
        //        int changedSlotCount = (int)(DragSlot.Instance.CurrentSlot.mItemCount * 0.5f);

        //        //(int)로의 형변환으로 인해 0이 되는 경우는 (int)(1 * 0.5f)이기에 단순히 새로운 슬롯으로 옮긴다.
        //        if (changedSlotCount == 0)
        //        {
        //            AddItem(DragSlot.Instance.CurrentSlot.Item, 1);
        //            DragSlot.Instance.CurrentSlot.ClearSlot();
        //            return;
        //        }

        //        //위 모든 경우가 아닌경우 새로운 슬롯에 새로운 아이템을 생성한다.
        //        AddItem(DragSlot.Instance.CurrentSlot.Item, changedSlotCount);
        //        DragSlot.Instance.CurrentSlot.UpdateSlotCount(-changedSlotCount);
        //        return;
        //    }
        //}

        //슬롯 서로 교환하기
        Item tempItem = mItem;
        int tempItemCount = mItemCount;

        AddItem(DragSlot.Instance.CurrentSlot.mItem, DragSlot.Instance.CurrentSlot.mItemCount);

        if (tempItem != null) { DragSlot.Instance.CurrentSlot.AddItem(tempItem, tempItemCount); }
        else { DragSlot.Instance.CurrentSlot.ClearSlot(); }
    }
}

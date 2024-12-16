using System.Collections;
using UnityEngine;

/// <summary>
/// 씬 내의 아이템(또는 정적 물체)에 다가가면 해당 아이템을 줍거나, 상호작용 할 수 있도록 해주는 스크립트
/// 플레이어의 오브젝트에 자식으로 넣은 EmptyObject에 Trigger Collider을 추가하여 사용
/// </summary>
public class ItemRaycast : MonoBehaviour
{
    /// <summary>
    /// 레이캐스트 된 아이템
    /// </summary>
    private RaycastHit mHit;

    /// <summary>
    /// 레이캐스트 거리
    /// </summary>
    [SerializeField] private float mRayDistance;

    private bool mIsPickupActive = false;  //아이템 습득이 가능한가?

    private ItemPickUp mCurrentItem; //활성화시 현재 등록된 아이템

    [Header("레이캐스트를 쏠 카메라")]
    [SerializeField] private Camera mRayCamera; //레이를 쏠 카메라 (메인카메라)

    [SerializeField] private InventoryMain mInventory; //인벤토리 메인
    // [SerializeField] private ItemActionManager mItemActionCustomFunc; //아이템 상호작용 커스텀 함수 매니저 (이 글에서는 설명 X)
    // [SerializeField] private ItemRaycastInfoText mItemRaycastInfoText; //아이템 상호작용 가능시 보여질 텍스트 매니저 (이 글에서는 설명 X)

    private void Update()
    {
        CheckItem();
        //if (Input.GetKeyDown(KeyCode.V))
        //{
            
        //}
        

        if (mIsPickupActive) { TryPickItem(); }
    }

    /// <summary>
    /// 아이템을 주울 수 있는지 확인한다.
    /// </summary>
    private void TryPickItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //주울 수 있는 아이템이라면?
            if (mCurrentItem.Item.Type > ItemType.NONE)
            {
                //현재 인벤토리 아이템 가져오기
                InventorySlot[] allitems = mInventory.mSlots;

                int count = 0;
                for (; count < allitems.Length; ++count)
                {
                    //현재 아이템 칸이 null이라면 주울 수 있는 상태
                    if (allitems[count].Item == null) { break; }

                    //현재 아이템칸이 null이 아니지만, 현재 아이템과 동일하면서 중첩이 가능한 아이템이라면 주울 수 있는 상태
                    if (allitems[count].Item.ItemID == mCurrentItem.Item.ItemID && allitems[count].Item.CanOverlap) { break; }
                }

                //모든 칸이 null이 아니고, 중첩이 불가능하면 주울 수 없음
                if (count == allitems.Length) { return; }

                //아이템 줍는 효과음 재생
                //SoundManager.Instance.PlaySound2D("GrabItem " + SoundManager.Range(1, 3));
            }

            TryPickUp();
            ItemInfoDisappear();
        }
    }

    /// <summary>
    /// 레이캐스트를 이용하여 아이템을 확인한다.
    /// </summary>
    private void CheckItem()
    {
        //if (Physics.Raycast(mRayCamera.transform.position, mRayCamera.transform.forward, out mHit, mRayDistance))
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit mHit, mRayDistance))
        {
            //레이캐스트 결과의 태그가 아이템이라면?
            if (mHit.transform.tag == "Item" || mHit.transform.tag == "NPC")
            {
                //현재 레이캐스트된 아이템
                ItemPickUp rayCastedItem = mHit.transform.GetComponent<ItemPickUp>();

                //레이캐스트 결과가 현재 아이템과 같으면 무시 (중복호출 방지)
                if (mCurrentItem == rayCastedItem) { return; }

                //아이템 얻어오기 및 정보 호출
                mCurrentItem = mHit.transform.GetComponent<ItemPickUp>();
                // mItemRaycastInfoText.EnableText(mHit.transform.position + Vector3.up * rayCastedItem.IndicatorHeight, mCurrentItem.Item); (이 글에서는 설명 X)

                Debug.LogFormat("아이템: {0} 획득 가능", mCurrentItem.Item.name);

                mIsPickupActive = true;

                return;
            }
            //레이캐스트 닿았을 때, 아이템이 아닌경우에는 비활성화
            else
            {
                ItemInfoDisappear();
            }
        }
        //레이캐스트 결과가 없으면 비활성화
        else
        {
            ItemInfoDisappear();
        }
    }

    /// <summary>
    /// 아이템 정보 보여주기를 비활성화 한다.
    /// </summary>
    private void ItemInfoDisappear()
    {
        //픽업 비활성화
        mIsPickupActive = false;

        //텍스트 비활성화
        // mItemRaycastInfoText.DisableText(); (이 글에서는 설명 X)

        //현재 아이템은 null
        mCurrentItem = null;
    }

    /// <summary>
    /// 아이템을 습득한다.
    /// </summary>
    private void TryPickUp()
    {
        if (mIsPickupActive)
        {
            // mItemActionCustomFunc.InteractionItem(mCurrentItem.Item, mCurrentItem.gameObject); (이 글에서는 설명 X)

            if (mCurrentItem.Item.Type != ItemType.NONE)
            {
                mInventory.AcquireItem(mCurrentItem.Item);
                Destroy(mCurrentItem.gameObject);
            }
            else if (mCurrentItem.Item.Type != ItemType.Equipment_NORMAL)
            {
                mInventory.AcquireItem(mCurrentItem.Item);
                Destroy(mCurrentItem.gameObject);
            }

            ItemInfoDisappear();
        }
    }
}
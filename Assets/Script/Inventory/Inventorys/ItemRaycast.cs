using System.Collections;
using UnityEngine;

/// <summary>
/// �� ���� ������(�Ǵ� ���� ��ü)�� �ٰ����� �ش� �������� �ݰų�, ��ȣ�ۿ� �� �� �ֵ��� ���ִ� ��ũ��Ʈ
/// �÷��̾��� ������Ʈ�� �ڽ����� ���� EmptyObject�� Trigger Collider�� �߰��Ͽ� ���
/// </summary>
public class ItemRaycast : MonoBehaviour
{
    /// <summary>
    /// ����ĳ��Ʈ �� ������
    /// </summary>
    private RaycastHit mHit;

    /// <summary>
    /// ����ĳ��Ʈ �Ÿ�
    /// </summary>
    [SerializeField] private float mRayDistance;

    private bool mIsPickupActive = false;  //������ ������ �����Ѱ�?

    private ItemPickUp mCurrentItem; //Ȱ��ȭ�� ���� ��ϵ� ������

    [Header("����ĳ��Ʈ�� �� ī�޶�")]
    [SerializeField] private Camera mRayCamera; //���̸� �� ī�޶� (����ī�޶�)

    [SerializeField] private InventoryMain mInventory; //�κ��丮 ����
    // [SerializeField] private ItemActionManager mItemActionCustomFunc; //������ ��ȣ�ۿ� Ŀ���� �Լ� �Ŵ��� (�� �ۿ����� ���� X)
    // [SerializeField] private ItemRaycastInfoText mItemRaycastInfoText; //������ ��ȣ�ۿ� ���ɽ� ������ �ؽ�Ʈ �Ŵ��� (�� �ۿ����� ���� X)

    private void Update()
    {
        CheckItem();
        //if (Input.GetKeyDown(KeyCode.V))
        //{
            
        //}
        

        if (mIsPickupActive) { TryPickItem(); }
    }

    /// <summary>
    /// �������� �ֿ� �� �ִ��� Ȯ���Ѵ�.
    /// </summary>
    private void TryPickItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //�ֿ� �� �ִ� �������̶��?
            if (mCurrentItem.Item.Type > ItemType.NONE)
            {
                //���� �κ��丮 ������ ��������
                InventorySlot[] allitems = mInventory.mSlots;

                int count = 0;
                for (; count < allitems.Length; ++count)
                {
                    //���� ������ ĭ�� null�̶�� �ֿ� �� �ִ� ����
                    if (allitems[count].Item == null) { break; }

                    //���� ������ĭ�� null�� �ƴ�����, ���� �����۰� �����ϸ鼭 ��ø�� ������ �������̶�� �ֿ� �� �ִ� ����
                    if (allitems[count].Item.ItemID == mCurrentItem.Item.ItemID && allitems[count].Item.CanOverlap) { break; }
                }

                //��� ĭ�� null�� �ƴϰ�, ��ø�� �Ұ����ϸ� �ֿ� �� ����
                if (count == allitems.Length) { return; }

                //������ �ݴ� ȿ���� ���
                //SoundManager.Instance.PlaySound2D("GrabItem " + SoundManager.Range(1, 3));
            }

            TryPickUp();
            ItemInfoDisappear();
        }
    }

    /// <summary>
    /// ����ĳ��Ʈ�� �̿��Ͽ� �������� Ȯ���Ѵ�.
    /// </summary>
    private void CheckItem()
    {
        //if (Physics.Raycast(mRayCamera.transform.position, mRayCamera.transform.forward, out mHit, mRayDistance))
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit mHit, mRayDistance))
        {
            //����ĳ��Ʈ ����� �±װ� �������̶��?
            if (mHit.transform.tag == "Item" || mHit.transform.tag == "NPC")
            {
                //���� ����ĳ��Ʈ�� ������
                ItemPickUp rayCastedItem = mHit.transform.GetComponent<ItemPickUp>();

                //����ĳ��Ʈ ����� ���� �����۰� ������ ���� (�ߺ�ȣ�� ����)
                if (mCurrentItem == rayCastedItem) { return; }

                //������ ������ �� ���� ȣ��
                mCurrentItem = mHit.transform.GetComponent<ItemPickUp>();
                // mItemRaycastInfoText.EnableText(mHit.transform.position + Vector3.up * rayCastedItem.IndicatorHeight, mCurrentItem.Item); (�� �ۿ����� ���� X)

                Debug.LogFormat("������: {0} ȹ�� ����", mCurrentItem.Item.name);

                mIsPickupActive = true;

                return;
            }
            //����ĳ��Ʈ ����� ��, �������� �ƴѰ�쿡�� ��Ȱ��ȭ
            else
            {
                ItemInfoDisappear();
            }
        }
        //����ĳ��Ʈ ����� ������ ��Ȱ��ȭ
        else
        {
            ItemInfoDisappear();
        }
    }

    /// <summary>
    /// ������ ���� �����ֱ⸦ ��Ȱ��ȭ �Ѵ�.
    /// </summary>
    private void ItemInfoDisappear()
    {
        //�Ⱦ� ��Ȱ��ȭ
        mIsPickupActive = false;

        //�ؽ�Ʈ ��Ȱ��ȭ
        // mItemRaycastInfoText.DisableText(); (�� �ۿ����� ���� X)

        //���� �������� null
        mCurrentItem = null;
    }

    /// <summary>
    /// �������� �����Ѵ�.
    /// </summary>
    private void TryPickUp()
    {
        if (mIsPickupActive)
        {
            // mItemActionCustomFunc.InteractionItem(mCurrentItem.Item, mCurrentItem.gameObject); (�� �ۿ����� ���� X)

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
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// ���� �������� ���� ���� �⺻���� �κ��丮
/// </summary>
public class InventoryMain : InventoryBase
{
    public static InventoryMain _instance;
    public static bool IsInventoryActive = false;  // �κ��丮 Ȱ��ȭ �Ǿ��°�?
    public TextMeshProUGUI gold;
    public int CurrentCoin = 1000;
    public static InventoryMain Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(InventoryMain)) as InventoryMain;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
    new void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        gold.text = $"{CurrentCoin}���)";
        TryOpenInventory();
    }

    /// <summary>
    /// Ư�� ������ ���Կ� �������� ��Ͻ�Ų��
    /// </summary>
    /// <param name="item">� ������?</param>
    /// <param name="targetSlot">��� ���Կ�?</param>
    /// <param name="count">������?></param>
    public void AcquireItem(Item item, InventorySlot targetSlot, int count = 1)
    {
        //��ø�� �����ϴٸ�?
        if (item.CanOverlap)
        {
            //����ũ�� ����Ͽ� �ش� ������ ����ũ�� ���Ǵ� ��ġ�ΰ�쿡�� �������� ����ֵ��� �Ѵ�.
            if (targetSlot.Item != null && targetSlot.IsMask(item))
            {
                if (targetSlot.Item.ItemID == item.ItemID)
                {
                    //���� ������ ������ ����(Count)�� �����Ѵ�.
                    targetSlot.UpdateSlotCount(count);
                }
            }
        }
        else
        {
            targetSlot.AddItem(item, count);
        }
    }


    public void AcquireItem(Item item, int count = 1)
    {
        //��ø�� �����ϴٸ�?
        if (item.CanOverlap)
        {
            for (int i = 0; i < mSlots.Length; i++)
            {
                //����ũ�� ����Ͽ� �ش� ������ ����ũ�� ���Ǵ� ��ġ�ΰ�쿡�� �������� ����ֵ��� �Ѵ�.
                if (mSlots[i].Item != null && mSlots[i].IsMask(item))
                {
                    if (mSlots[i].Item.ItemID == item.ItemID)
                    {
                        //���� ������ ������ ����(Count)�� �����Ѵ�.
                        mSlots[i].UpdateSlotCount(count);
                        return;
                    }
                }
            }
        }

        //��� �������� �ƴѰ�� ���ο� ���Կ� ���´�.
        for (int i = 0; i < mSlots.Length; i++)
        {
            if (mSlots[i].Item == null && mSlots[i].IsMask(item))
            {
                mSlots[i].AddItem(item, count);
                mSlots[i].UseItem();
                return;
            }
        }
    }

    /// <summary>
    /// �κ��丮�� IŰ�� ���� ���ų� �ݴ´�.
    /// </summary>
    private void TryOpenInventory()
    {
        //�ɼ��� �����ִ°�� ��Ȱ��ȭ
        //if (GameMenuManager.IsOptionActive) { return; }

        if (Input.GetKeyDown(Managers.KeyInput.GetKeyCode("Inventory")))
        {
            if (!IsInventoryActive)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    /// <summary>
    /// �κ��丮�� ����.
    /// </summary>
    private void OpenInventory()
    {
        mInventoryBase.SetActive(true);
        IsInventoryActive = true;
    }

    /// <summary>
    /// �κ��丮�� �ݴ´�.
    /// </summary>
    public void CloseInventory()
    {
        mInventoryBase.SetActive(false);
        IsInventoryActive = false;
    }

    /// <summary>
    /// �ش� �������� ȹ���� �� �ִ°�?
    /// </summary>
    /// <param name="item">ȹ���ϰ��� �ϴ� ������</param>
    /// <returns>ȹ���� ������ �ش� ������ ��ġ</returns>
    public InventorySlot IsCanAquireItem(Item item)
    {
        foreach (InventorySlot slot in base.mSlots)
        {
            //����ִ� ������ �߰��Ѱ��
            if (slot.Item == null) { return slot; }

            //��ø�� ������ �������� ���� �������� ������ �߰��Ѱ��
            if (item.CanOverlap && slot.Item.Type == item.Type) { return slot; }
        }

        return null;
    }

    public void RefreshLabels()
    {
        //targetSlot.
    }
}
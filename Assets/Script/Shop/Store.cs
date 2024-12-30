using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public int num = 0;
    [SerializeField] private ItemShop mItemShop;
    public ItemShopSlot Slot;
    private void Start()
    {
        Debug.Log(num + "¤·¤·");
        ItemShopManager.Instance.OpenShop(mItemShop.mSellItemInfos, mItemShop.ShopLevel, num, transform.position, Slot);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemShopManager.Instance.ItemShopInteract(Slot);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("Interact")))
        {
            ItemShopManager.Instance.BuyItem(Slot);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemShopManager.Instance.ItemShopInteract(Slot);
            //ItemShopManager.Instance.CloseItemShop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public int num = 0;
    private bool isInTrigger = false;
    [SerializeField] private ItemShop mItemShop;
    public ItemShopSlot Slot;
    private void Start()
    {
        ItemShopManager.Instance.OpenShop(mItemShop.mSellItemInfos, mItemShop.ShopLevel, num, transform.position, Slot);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = true;
            ItemShopManager.Instance.ItemShopInteractEnter(Slot);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInTrigger = false;
        ItemShopManager.Instance.ItemShopInteractExit(Slot);
    }

    private void Update()
    {
        if (isInTrigger && Input.GetKeyDown(Managers.KeyInput.GetKeyCode("Interact")))
        {
            Debug.Log("Interact");
            ItemShopManager.Instance.BuyItem(Slot);
        }
    }
}

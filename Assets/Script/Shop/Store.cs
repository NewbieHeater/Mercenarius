using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public int num = 0;
    [SerializeField] private ItemShop mItemShop;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("dd");
            ItemShopManager.Instance.OpenItemShop(mItemShop.mSellItemInfos, mItemShop.ShopLevel, num, transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemShopManager.Instance.CloseItemShop();
        }
    }
}

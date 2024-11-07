using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPC_Level1 : MonoBehaviour
{
    [SerializeField] private ItemShop mItemShop;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ItemShopManager.Instance.OpenItemShop(mItemShop.mSellItemInfos, mItemShop.ShopLevel);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            ItemShopManager.Instance.CloseItemShop();
        }

    }
}

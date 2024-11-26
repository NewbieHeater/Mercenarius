using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using Quest;

namespace GameSave
{
    
    #region ShopGameDataManager

    [Serializable]
    public struct SellItemInfo // 아이템 슬롯 한개의 판매 정보
    {
        public int sellItemEntityCode; // 판매 아이템의 아이템코드
        public int itemAmount; // 아이템의 남은 총 개수(재고량)
    }

    [Serializable]
    public struct ShopInfo
    {
        public int shopId; // 상점의 고유 ID
        public int shopLevel; // 현재 상점의 판매 단계
        public SellItemInfo[] sellItemInfos; // 상점에서 판매중인 아이템들의 정보
    }


    [Serializable]
    public struct ShopGameData
    {
        public ShopInfo[] shopInfos;
    }

    #endregion
}

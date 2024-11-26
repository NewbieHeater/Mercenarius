using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using Quest;

namespace GameSave
{
    
    #region ShopGameDataManager

    [Serializable]
    public struct SellItemInfo // ������ ���� �Ѱ��� �Ǹ� ����
    {
        public int sellItemEntityCode; // �Ǹ� �������� �������ڵ�
        public int itemAmount; // �������� ���� �� ����(���)
    }

    [Serializable]
    public struct ShopInfo
    {
        public int shopId; // ������ ���� ID
        public int shopLevel; // ���� ������ �Ǹ� �ܰ�
        public SellItemInfo[] sellItemInfos; // �������� �Ǹ����� �����۵��� ����
    }


    [Serializable]
    public struct ShopGameData
    {
        public ShopInfo[] shopInfos;
    }

    #endregion
}

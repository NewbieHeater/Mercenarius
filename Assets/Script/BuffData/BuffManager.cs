using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public List<BuffData> buffDataList;

    private Dictionary<BuffType, BuffData> buffDataDictionary;

    private void Awake()
    {
        buffDataDictionary = new Dictionary<BuffType, BuffData>();
        foreach (var buffData in buffDataList)
        {
            buffDataDictionary[buffData.buffType] = buffData;
        }
    }

    public BuffData GetBuffData(BuffType buffType)
    {
        if (buffDataDictionary.ContainsKey(buffType))
        {
            return buffDataDictionary[buffType];
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Level1 : MonoBehaviour
{
    public Store[] ItemRandomNumber;


    List<int> itemList = new List<int>();

    int min = 0;
    int max = 3;

    void Awake()
    {
        CreateUnDuplicateRandom(min, max);
        
        for (int i = 0; i < itemList.Count; i++)
        {
        
            ItemRandomNumber[i].num = itemList[i];
            
        }

    }

    // 랜덤 생성 (중복 배제)
    void CreateUnDuplicateRandom(int min, int max)
    {
        int currentNumber = Random.Range(min, max);

        for (int i = 0; i < 3;)
        {
            if (itemList.Contains(currentNumber))
            {
                currentNumber = Random.Range(min, max);
            }
            else
            {
                itemList.Add(currentNumber);
                i++;
            }
        }

    }
}

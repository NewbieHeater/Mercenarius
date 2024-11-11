using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject Abcd;
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;
    public int num1;
    public int num2;
    public int num3;

    List<int> itemList = new List<int>();

    int min=0;
    int max=10;

    void Start()
    {
        //CreateDuplicateRandom(min, max);
        CreateUnDuplicateRandom(min, max);

        for (int i = 0; i < itemList.Count; i++)
        {
            Debug.Log(itemList[i]);
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

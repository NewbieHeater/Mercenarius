using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Level1 : MonoBehaviour
{
    private List<Store> ItemRandomNumber = new List<Store>();
    private GameObject[] gameObjects;

    private List<int> itemList = new List<int>();

    public void Init()
    {

        gameObjects = GameObject.FindGameObjectsWithTag("Store");
        ItemRandomNumber.Clear();

        foreach (GameObject go in gameObjects)
        {
            Store store = go.GetComponent<Store>();
            if (store != null)
            {
                ItemRandomNumber.Add(store);
            }
        }

        int count = ItemRandomNumber.Count;
        CreateUnDuplicateRandom(0, count - 1);


        for (int i = 0; i < count; i++)
        {
            Debug.Log(ItemRandomNumber.Count);
            Debug.Log(itemList.Count);
            Debug.Log(count);
            ItemRandomNumber[i].num = itemList[i];
            ItemRandomNumber[i].Init();
        }
    }


    void CreateUnDuplicateRandom(int min, int max)
    {
        List<int> numbers = new List<int>();
        for (int i = min; i <= max; i++)
        {
            numbers.Add(i);
        }

        for (int i = numbers.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        itemList = numbers;
    }
}

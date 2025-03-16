using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Level1 : MonoBehaviour
{
    private List<Store> ItemRandomNumber = new List<Store>();
    private GameObject[] gameObjects;
    private List<int> itemList = new List<int>();

    int min = 0;
    int max = 20;

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
        if (count == 0)
        {
            Debug.LogWarning("Store 오브젝트가 하나도 없습니다.");
            return;
        }


        CreateUnDuplicateRandom(min, count - 1);


        if (itemList.Count < count)
        {
            Debug.LogError("itemList의 개수가 스토어 수보다 작습니다.");
            return;
        }

        for (int i = 0; i < count; i++)
        {
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

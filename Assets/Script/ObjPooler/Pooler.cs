using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pooler : MonoBehaviour
{
    public GameObject[] prefabs; // 인스펙터에서 초기화

    List<GameObject>[] pools; //풀에 넣을 프리팹을 받기위해
    //private Dictionary<string, IObjectPool<GameObject>> ojbectPoolDic = new Dictionary<string, IObjectPool<GameObject>>();
    //private Dictionary<string, GameObject> goDic = new Dictionary<string, GameObject>();
    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
            pools[index] = new List<GameObject>();
        //풀 초기화
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])//foreach로 검사
        {
            if (!item.activeSelf) //비활성화된 풀이 있다면 활성화(죽은게 있다면)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select) //비활성화된 풀이 없으면 새로 생성
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

    public void Clear(int index)
    {
        foreach (GameObject item in pools[index])
            item.SetActive(false);
    }

    public void ClearAll()
    {
        for (int index = 0; index < pools.Length; index++)
            foreach (GameObject item in pools[index])
                item.SetActive(false);
    }
}

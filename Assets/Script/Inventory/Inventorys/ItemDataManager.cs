using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    public static ItemDataManager _instance;
    private string Name;
    private string Description;
    public static ItemDataManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(ItemDataManager)) as ItemDataManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
    public string GetName(int id)
    {
        return Name;
    }

    public string GetDescription(int id)
    {
        return Description;
    }
}

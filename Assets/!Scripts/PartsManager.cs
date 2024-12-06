using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsManager : MonoBehaviour
{
    static PartsManager _instance;

    [SerializeField] List<Parts> partsPrefab;
    List<Parts> parts = new List<Parts>();

    public static PartsManager instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;

        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        Initiate();
    }

    void Initiate()
    {
        foreach (var part in partsPrefab)
        {
            parts.Add(part);
        }
    }
}

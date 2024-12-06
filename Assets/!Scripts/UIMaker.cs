using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMaker : MonoBehaviour
{
    [SerializeField] UIResource[] ui;

    static UIMaker instance;

    public static UIMaker Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void CreateCanvas(int index)
    {
        Instantiate(ui[index].canvas);
    }
}
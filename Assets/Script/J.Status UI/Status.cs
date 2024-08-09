using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Status : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int gold;

    void Start()
    {
        gold = 0;
    }

    void Update()
    {
        text.text = gold;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gold += 1;
        }
    }
}
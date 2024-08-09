using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statusinformation : MonoBehaviour
{
    [SerializeField] private Image barImage;

    public void Awake()
    {
        barImage.fillAmount = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            barImage.fillAmount = barImage.fillAmount - 0.01f; //체력 감소
        }
    }
}
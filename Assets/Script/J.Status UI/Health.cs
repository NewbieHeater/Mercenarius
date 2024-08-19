using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int Damage = 1;

    public static Health instance;
    [SerializeField] private Image barImage;

    private void Awake()
    {
        barImage.fillAmount = 1;

        if (Health.instance == null)
        {
            Health.instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            barImage.fillAmount = barImage.fillAmount - Damage / 100f; //체력바 감소
        }
    }
}

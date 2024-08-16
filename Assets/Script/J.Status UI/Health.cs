using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statusinformation : MonoBehaviour
{
    public int enemyDamage = 1;

    [SerializeField] private Image barImage;

    public void Awake()
    {
        barImage.fillAmount = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            barImage.fillAmount = barImage.fillAmount - enemyDamage / 100f; //체력바 감소
        }
    }
}
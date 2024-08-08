using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statusinformation : MonoBehaviour
{
    float curHealth;  //���� ü��
    float maxHealth; //�ִ� ü��

    public Slider HpBarSlider;

    public void Awake()
    {
        maxHealth = 100;
        curHealth = maxHealth;
    }

    private void Update()
    {
        HpBarSlider.value = (float)curHealth / maxHealth;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            curHealth = curHealth - 1;
        }
    }
}


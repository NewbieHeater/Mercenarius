using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statusinformation : MonoBehaviour
{
    float curHealth;  //현재 체력
    float maxHealth; //최대 체력

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


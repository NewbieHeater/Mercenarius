using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class enemyHealth : EnemyState
{
    public UnityEvent onPlayerDead;
    public float health;
    public float maxHealth;
    //public float health = ;
    //플레이어 공격 리지드바디에 닿았을때
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health--;
        }
        if(health <= 0)
        {
            onPlayerDead.Invoke();
        }
    }
    /*
    public void Init(SpawnData data)
    {
        maxHealth = data.OriginalHealth;
        health = data.OriginalHealth;
    }
    */
    void OnEnable()
    {
        health = maxHealth;
    }
}

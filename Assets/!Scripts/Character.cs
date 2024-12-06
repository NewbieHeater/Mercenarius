using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected CharacterManager cm = CharacterManager.Instance;

    protected int hp;
    protected int maxHp;
    protected int attackDamage;
    protected int speed;
    protected int range;

    bool alive;

    protected virtual void Start()
    {
        alive = true;
    }

    public abstract void Attack();

    public void Move()
    {

    }

    public void GetDamaged(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
            SetDead();
    }

    void SetDead()
    {
        alive = false;
    }

    protected int GetDamage()
    {
        return attackDamage;
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public int GetRange()
    {
        return range;
    }

    public void SetHP(int value)
    {
        maxHp = value;
    }

    public void SetAttackDamage(int value)
    {
        attackDamage = value;
    }

    public void SetSpeed(int value)
    {
        speed = value;
    }

    public void SetRange(int value)
    {
        range = value;
    }
}

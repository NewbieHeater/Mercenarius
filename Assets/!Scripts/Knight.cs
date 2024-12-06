using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Character
{
    protected override void Start()
    {
        hp = 100;
        attackDamage = 10;
        speed = 5;
        range = 2;
        base.Start();
    }

    public override void Attack()
    {
        Debug.Log("Knight Attack!");
    }
}

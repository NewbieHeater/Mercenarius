using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character
{
    protected override void Start()
    {
        hp = 50;
        attackDamage = 10;
        speed = 3;
        range = 10;
        base.Start();
    }

    public override void Attack()
    {
        Debug.Log("Archer Attack!");
    }
}

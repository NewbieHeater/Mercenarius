using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musketeer : Character, IParts
{
    protected override void Start()
    {
        hp = 40;
        attackDamage = 15;
        speed = 5;
        range = 20;
        base.Start();
    }

    public override void Attack()
    {
        Debug.Log($"Musketter Attack! attackDanage: {attackDamage}, range: {range}");
    }

    public void EnPart<T>() where T : Parts
    {
        gameObject.AddComponent<T>();
    }

    public void DePart<T>() where T : Parts
    {
        var part = gameObject.GetComponent<T>();

        if (part)
        {
            part.DePart();
        }
    }
}

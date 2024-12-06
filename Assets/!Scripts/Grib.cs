using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grib : Parts
{
    Character character;
    int damage;

    void Start()
    {
        damage = 5;
        character = GetComponent<Character>();
        character.SetAttackDamage(character.GetAttackDamage() + damage);
    }

    public override void DePart()
    {
        character.SetAttackDamage(character.GetAttackDamage() - damage);
        base.DePart();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : Parts
{
    Character character;
    int range;

    void Start()
    {
        range = 5;
        character = GetComponent<Character>();
        character.SetRange(character.GetRange() + range);
    }

    public override void DePart()
    {
        character.SetRange(character.GetRange() - range);
        base.DePart();
    }
}
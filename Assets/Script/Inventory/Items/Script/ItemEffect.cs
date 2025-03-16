using UnityEngine;
using System;

[System.Serializable]
public abstract class ItemEffect
{

    public abstract void OnEquip(Character character);

    public abstract void OnUnequip(Character character);
}


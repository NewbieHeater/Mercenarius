using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOItem : ScriptableObject
{
    public enum ItemType
    {
        Weapon,
        Shield,
        Armor,
        Accessory,
        Potion,
        Resource,
    }

    public ItemType itemType;

    [System.Serializable]
    public struct STAT
    {
        public string name;
        public int value;
    }

    public List<STAT> stats = new List<STAT>();

    public int maxStack;
    public int price;

    public Sprite icon;
    public string description;
}
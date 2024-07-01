using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour 
{
    public ItemStat[] itemStat;

}

[System.Serializable]
public class ItemStat
{
    public string itemName;
    public int type;
    public int atkDamage;
    public int def;
    public int maxHp;
    public int regen_Hp;
    public int regen_Hp_by_Percent;
    public int skillDamage;
    public int enhanced_Damage;
    public int enhanced_Defense;
    public int gun_Parts;
}
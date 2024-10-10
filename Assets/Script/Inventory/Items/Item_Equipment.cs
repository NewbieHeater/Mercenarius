using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// 장비 효과
/// </summary>
[Serializable]
public struct EquipmentEffect
{
    [Header("추가 공격력")]
    [SerializeField] private float mDamage;
    public float Damage
    {
        get
        {
            return mDamage;
        }
    }

    [Header("추가 체력")]
    [SerializeField] private float mHp;
    public float Hp
    {
        get
        {
            return mHp;
        }
    }

    [Header("추가 방어력")]
    [SerializeField] private float mDefense;
    public float Defense
    {
        get
        {
            return mDefense;
        }
    }

    [Header("추가 이동속도")]
    [SerializeField] private float mSpeed;
    public float Speed
    {
        get
        {
            return mSpeed;
        }
    }

    public static EquipmentEffect operator +(EquipmentEffect param1, EquipmentEffect param2)
    {
        EquipmentEffect calcedEffect;

        calcedEffect.mDamage = param1.mDamage + param2.mDamage;
        calcedEffect.mHp = param1.mHp + param2.mHp;
        calcedEffect.mDefense = param1.mDefense + param2.mDefense;
        calcedEffect.mSpeed = param1.mSpeed + param2.mSpeed;

        return calcedEffect;
    }
}

[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Item(Equipment)")]
public class Item_Equipment : Item
{
    [Space(50)]
    [Header("장비 아이템 효과 (착용시 발동)")]
    [SerializeField] private EquipmentEffect mEffect;

    /// <summary>
    /// 해당 아이템을 장착했을 때 받는 추가 효과
    /// </summary>
    /// <value></value>
    public EquipmentEffect Effect
    {
        get
        {
            return mEffect;
        }
    }
}
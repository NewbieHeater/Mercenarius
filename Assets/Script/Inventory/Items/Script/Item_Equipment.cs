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
    [Header("수치")]
    [SerializeField] private float mValue;
    public float Value
    {
        get
        {
            return mValue;
        }
    }
    [Header("추가 공격")]
    [SerializeField] private float mAttack;
    public float Attack
    {
        get
        {
            return mAttack;
        }
    }
    [Header("수치")]
    [SerializeField] private float mAttackSpeed;
    public float AttackSpeed
    {
        get
        {
            return mAttackSpeed;
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

        calcedEffect.mValue = param1.mValue + param2.mValue;
        calcedEffect.mAttack = param1.mAttack + param2.mAttack;
        calcedEffect.mHp = param1.mHp + param2.mHp;
        calcedEffect.mDefense = param1.mDefense + param2.mDefense;
        calcedEffect.mSpeed = param1.mSpeed + param2.mSpeed;
        calcedEffect.mAttackSpeed = param1.mAttackSpeed + param2.mAttackSpeed;

        return calcedEffect;
    }
}

[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Item(Equipment)")]
public class Item_Equipment : Item // Item은 기존 아이템 기본 클래스
{
    [Header("장비 아이템 효과 (착용시 발동)")]
    public ItemEffect[] effects;

    [Space(50)]
    [Header("기본 장비 효과")]
    [SerializeField] private EquipmentEffect mEffect;
    public EquipmentEffect Effect => mEffect;

    
    public void Equip(Character character)
    {
        foreach (var effect in effects)
        {
            effect.OnEquip(character);
        }
    }

    public void Unequip(Character character)
    {
        foreach (var effect in effects)
        {
            effect.OnUnequip(character);
        }
    }
}

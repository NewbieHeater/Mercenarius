using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// ��� ȿ��
/// </summary>
[Serializable]
public struct EquipmentEffect
{
    [Header("�߰� ���ݷ�")]
    [SerializeField] private float mDamage;
    public float Damage
    {
        get
        {
            return mDamage;
        }
    }

    [Header("�߰� ����")]
    [SerializeField] private float mDefense;
    public float Defense
    {
        get
        {
            return mDefense;
        }
    }

    public static EquipmentEffect operator +(EquipmentEffect param1, EquipmentEffect param2)
    {
        EquipmentEffect calcedEffect;

        calcedEffect.mDamage = param1.mDamage + param2.mDamage;
        calcedEffect.mDefense = param1.mDefense + param2.mDefense;

        return calcedEffect;
    }
}

[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Item(Equipment)")]
public class Item_Equipment : Item
{
    [Space(50)]
    [Header("��� ������ ȿ�� (����� �ߵ�)")]
    [SerializeField] private EquipmentEffect mEffect;

    /// <summary>
    /// �ش� �������� �������� �� �޴� �߰� ȿ��
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
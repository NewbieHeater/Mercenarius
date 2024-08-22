using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BuffStat
{
    [Header("���� ���ݷ�")]
    [SerializeField] private float mDamage;
    public float damage
    {
        get
        {
            return mDamage;
        }
    }

    [Header("���� ����")]
    [SerializeField] private float mDefense;
    public float defense
    {
        get
        {
            return mDefense;
        }
    }

    [Header("���� �̵��ӵ�")]
    [SerializeField] private float mMovementSpeed;
    public float movementSpeed
    {
        get
        {
            return mMovementSpeed;
        }
    }
}

public abstract class BuffController
{
    [SerializeField] private BuffStat mBuffStat;

    protected float duration;
    protected GameObject target;

    public BuffController(float duration, GameObject target)
    {
        this.duration = duration;
        this.target = target;
    }

    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
}

// ���ݷ� ���� ����
public class AttackBuff : BuffController
{
    float attackIncrease;

    public AttackBuff(float duration, GameObject target, float attackIncrease) : base(duration, target)
    {
        this.attackIncrease = attackIncrease;
    }

    public override void ApplyEffect()
    {
        // ���ݷ��� ������Ŵ
        // ���÷� Debug.Log�� ����Ͽ�����, ������ ���ݷ��� �������Ѿ� ��
        Debug.Log("Attack buff applied: +" + attackIncrease + " Attack");
    }

    public override void RemoveEffect()
    {
        // ���÷� Debug.Log�� ����Ͽ�����, ������ ���ݷ��� �����ؾ� ��
        Debug.Log("Attack buff removed: -" + attackIncrease + " Attack");
    }
    //��ųʸ��� ���� �����ϱ�
}

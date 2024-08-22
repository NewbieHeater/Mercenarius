using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BuffStat
{
    [Header("버프 공격력")]
    [SerializeField] private float mDamage;
    public float damage
    {
        get
        {
            return mDamage;
        }
    }

    [Header("버프 방어력")]
    [SerializeField] private float mDefense;
    public float defense
    {
        get
        {
            return mDefense;
        }
    }

    [Header("버프 이동속도")]
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

// 공격력 증가 버프
public class AttackBuff : BuffController
{
    float attackIncrease;

    public AttackBuff(float duration, GameObject target, float attackIncrease) : base(duration, target)
    {
        this.attackIncrease = attackIncrease;
    }

    public override void ApplyEffect()
    {
        // 공격력을 증가시킴
        // 예시로 Debug.Log를 사용하였지만, 실제로 공격력을 증가시켜야 함
        Debug.Log("Attack buff applied: +" + attackIncrease + " Attack");
    }

    public override void RemoveEffect()
    {
        // 예시로 Debug.Log를 사용하였지만, 실제로 공격력을 복원해야 함
        Debug.Log("Attack buff removed: -" + attackIncrease + " Attack");
    }
    //딕셔너리로 버프 저장하기
}

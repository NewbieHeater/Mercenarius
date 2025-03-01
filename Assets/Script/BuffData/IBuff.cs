using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff
{
    string BuffName { get; }
    float Duration { get; }
    void Apply(GameObject target);
    void Remove(GameObject target);
}
public class AttackPowerBuff : IBuff
{
    public string BuffName => "Attack Power Buff";
    public float Duration { get; private set; }
    private float attackIncrease;

    public AttackPowerBuff(float duration, float attackIncrease)
    {
        Duration = duration;
        this.attackIncrease = attackIncrease;
    }

    public void Apply(GameObject target)
    {
        var stats = target.GetComponent<StatDatas>();
        if (stats != null)
        {
            stats.AttackPower += attackIncrease;
        }
    }

    public void Remove(GameObject target)
    {
        var stats = target.GetComponent<StatDatas>();
        if (stats != null)
        {
            stats.AttackPower -= attackIncrease;
        }
    }
}

public class FrenzyBuff : IBuff
{
    public string BuffName => "Frenzy Buff";
    public float Duration { get; private set; }
    private float attackIncrease;
    private float maxHealthDecrease;
    private float speedIncrease;

    public FrenzyBuff(float duration, float attackIncrease, float maxHealthDecrease, float speedIncrease)
    {
        Duration = duration;
        this.attackIncrease = attackIncrease;
        this.maxHealthDecrease = maxHealthDecrease;
        this.speedIncrease = speedIncrease;
    }

    public void Apply(GameObject target)
    {
        var stats = target.GetComponent<StatDatas>();
        if (stats != null)
        {
            stats.AttackPower += attackIncrease;
            stats.MaxHealth -= maxHealthDecrease;
            stats.MoveSpeed += speedIncrease;
        }
    }

    public void Remove(GameObject target)
    {
        var stats = target.GetComponent<StatDatas>();
        if (stats != null)
        {
            stats.AttackPower -= attackIncrease;
            stats.MaxHealth += maxHealthDecrease;
            stats.MoveSpeed -= speedIncrease;
        }
    }
}

public class HolyBuff : IBuff
{
    public string BuffName => "Holy Buff";
    public float Duration { get; private set; }

    public HolyBuff(float duration)
    {
        Duration = duration;
    }

    public void Apply(GameObject target)
    {
        var stats = target.GetComponent<StatDatas>();
        if (stats != null)
        {
            stats.MaxHealth += 10;
            stats.AttackPower += 10;
            stats.HealthRecovery += 10;
            stats.Defense += 10;
        }
    }

    public void Remove(GameObject target)
    {
        var stats = target.GetComponent<StatDatas>();
        if (stats != null)
        {
            stats.MaxHealth -= 10;
            stats.AttackPower -= 10;
            stats.HealthRecovery -= 10;
            stats.Defense -= 10;
        }
    }
}

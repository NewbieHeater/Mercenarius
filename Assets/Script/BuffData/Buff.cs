using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff
{
    BuffType BuffType { get; }
    float Duration { get; }
    float EffectValue { get; } // ȿ�� ���� �߰�
    void ApplyEffect(Character character);
    void RemoveEffect(Character character);
    void StackBuff(float additionalValue); // ��ø ȿ�� �߰� �޼���
}


public class AttackBuff : IBuff
{
    private float duration;
    private float effectValue;

    public BuffType BuffType => BuffType.AttackIncrease;
    public float Duration => duration;
    public float EffectValue => effectValue; // ȿ�� �� ����

    public AttackBuff(float duration, float effectValue)
    {
        this.duration = duration;
        this.effectValue = effectValue;
    }

    public void StackBuff(float additionalValue)
    {
        effectValue += additionalValue; // ��ø ȿ�� �߰�
    }

    public void ApplyEffect(Character character)
    {
        character.AttackBuff += effectValue;
    }

    public void RemoveEffect(Character character)
    {
        character.AttackBuff -= effectValue;
    }
}



public class BleedBuff : IBuff
{
    private float duration;
    private float effectValue;
    private float damagePerSecond;

    public BuffType BuffType => BuffType.Bleed;
    public float Duration => duration;
    public float EffectValue => effectValue; // ȿ�� �� ����

    public BleedBuff(float duration, float damagePerSecond, float effectValue)
    {
        this.duration = duration;
        this.effectValue = effectValue;
        this.damagePerSecond = damagePerSecond;
    }

    public void ApplyEffect(Character character)
    {
        character.StartCoroutine(ApplyBleedEffect(character));
    }

    public void RemoveEffect(Character character)
    {
        // ������ �ڵ� ����ǹǷ� ���� ���� ����
    }

    private IEnumerator ApplyBleedEffect(Character character)
    {
        float t;
        float elapsedTime = 0f;
        t = Time.deltaTime;
        while (elapsedTime < Duration)
        {
            character.HpBuff -= damagePerSecond;
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }
        
        
    }
    public void StackBuff(float additionalValue)
    {

    }
}

public class SlowBuff : IBuff
{
    private float duration;
    private float slowPercentage;

    public BuffType BuffType => BuffType.Slow;
    public float Duration => duration;
    public float EffectValue => slowPercentage; // ������ �ϴ� ����

    public SlowBuff(float duration, float slowPercentage)
    {
        this.duration = duration;
        this.slowPercentage = slowPercentage;
    }

    public void ApplyEffect(Character character)
    {
        character.MovementSpeedBuff *= (1 - slowPercentage); // �ӵ��� ���ҽ�Ŵ
    }

    public void RemoveEffect(Character character)
    {
        character.MovementSpeedBuff /= (1 - slowPercentage); // ���� �ӵ��� ����
    }

    public void StackBuff(float additionalValue)
    {
        // ���⼭�� ������ �ϴ� ������ �߰��ϴ� ��� ���� ������ ��ø�Ǹ� �� ������ �� �� �ֽ��ϴ�.
        slowPercentage += additionalValue;
    }
}

public class HpRegenerateBuff : IBuff
{
    private float duration;
    private float regenerateAmount;
    private Coroutine regenCoroutine;

    public BuffType BuffType => BuffType.HpRegen;
    public float Duration => duration;
    public float EffectValue => regenerateAmount;

    public HpRegenerateBuff(float duration, float regenerateAmount)
    {
        this.duration = duration;
        this.regenerateAmount = regenerateAmount;
    }

    public void ApplyEffect(Character character)
    {
        if (regenCoroutine != null)
        {
            character.StopCoroutine(regenCoroutine); // ���� �ڷ�ƾ�� ������ ����
        }
        regenCoroutine = character.StartCoroutine(RegenerateHP(character));
    }

    public void RemoveEffect(Character character)
    {
        if (regenCoroutine != null)
        {
            character.StopCoroutine(regenCoroutine); // �ڷ�ƾ ����
        }
    }

    private IEnumerator RegenerateHP(Character character)
    {
        float elapsedTime = 0f;
        while (elapsedTime < Duration)
        {
            character.HpBuff += regenerateAmount;
            yield return new WaitForSeconds(1f); // 1�ʸ��� ȸ��
            elapsedTime += 1f;
        }
    }

    public void StackBuff(float additionalValue)
    {
        regenerateAmount += additionalValue; // ���� ȸ���� ����
    }
}



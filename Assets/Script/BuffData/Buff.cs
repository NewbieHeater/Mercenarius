using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff
{
    BuffType BuffType { get; }
    float Duration { get; }
    float EffectValue { get; } // 효과 값을 추가
    void ApplyEffect(Character character);
    void RemoveEffect(Character character);
    void StackBuff(float additionalValue); // 중첩 효과 추가 메서드
}


public class AttackBuff : IBuff
{
    private float duration;
    private float effectValue;

    public BuffType BuffType => BuffType.AttackIncrease;
    public float Duration => duration;
    public float EffectValue => effectValue; // 효과 값 구현

    public AttackBuff(float duration, float effectValue)
    {
        this.duration = duration;
        this.effectValue = effectValue;
    }

    public void StackBuff(float additionalValue)
    {
        effectValue += additionalValue; // 중첩 효과 추가
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
    public float EffectValue => effectValue; // 효과 값 구현

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
        // 출혈은 자동 종료되므로 별도 제거 없음
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
    public float EffectValue => slowPercentage; // 느리게 하는 비율

    public SlowBuff(float duration, float slowPercentage)
    {
        this.duration = duration;
        this.slowPercentage = slowPercentage;
    }

    public void ApplyEffect(Character character)
    {
        character.MovementSpeedBuff *= (1 - slowPercentage); // 속도를 감소시킴
    }

    public void RemoveEffect(Character character)
    {
        character.MovementSpeedBuff /= (1 - slowPercentage); // 원래 속도로 복원
    }

    public void StackBuff(float additionalValue)
    {
        // 여기서는 느리게 하는 비율을 추가하는 대신 같은 버프가 중첩되면 더 느리게 할 수 있습니다.
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
            character.StopCoroutine(regenCoroutine); // 기존 코루틴이 있으면 중지
        }
        regenCoroutine = character.StartCoroutine(RegenerateHP(character));
    }

    public void RemoveEffect(Character character)
    {
        if (regenCoroutine != null)
        {
            character.StopCoroutine(regenCoroutine); // 코루틴 중지
        }
    }

    private IEnumerator RegenerateHP(Character character)
    {
        float elapsedTime = 0f;
        while (elapsedTime < Duration)
        {
            character.HpBuff += regenerateAmount;
            yield return new WaitForSeconds(1f); // 1초마다 회복
            elapsedTime += 1f;
        }
    }

    public void StackBuff(float additionalValue)
    {
        regenerateAmount += additionalValue; // 마나 회복량 증가
    }
}



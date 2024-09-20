using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff
{
    BuffType BuffType { get; }      // 버프의 타입 (공격력 증가, 출혈 등)
    float Duration { get; }         // 버프의 지속 시간
    void ApplyEffect(Character character);   // 버프를 적용할 때 호출
    void RemoveEffect(Character character);  // 버프를 제거할 때 호출
    void StackBuff(float additionalValue);
}

public class Buff : IBuff
{
    private BuffData buffData;
    private float totalEffectValue;

    public Buff(BuffData data)
    {
        buffData = data;
        totalEffectValue = data.effectValue;
    }

    public BuffType BuffType => buffData.buffType;
    public float Duration => buffData.duration;

    public void ApplyEffect(Character character)
    {
        switch (BuffType)
        {
            case BuffType.AttackIncrease:
                character.AttackBuff += totalEffectValue;
                Debug.Log("gat");
                break;
            case BuffType.Bleed:
                character.StartCoroutine(ApplyBleedEffect(character));
                break;
        }
    }

    public void RemoveEffect(Character character)
    {
        switch (BuffType)
        {
            case BuffType.AttackIncrease:
                character.AttackBuff -= totalEffectValue;
                break;
            case BuffType.Bleed:
                // 출혈 효과는 자동 종료되므로 특별히 제거할 필요 없음
                break;
        }
    }

    private IEnumerator ApplyBleedEffect(Character character)
    {
        float elapsedTime = 0f;
        while (elapsedTime < Duration)
        {
            character.HpBuff -= totalEffectValue;
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }
    }

    // 중첩 시 효과를 추가
    public void StackBuff(float additionalValue)
    {
        totalEffectValue += additionalValue;
    }
}

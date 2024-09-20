using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff
{
    BuffType BuffType { get; }      // ������ Ÿ�� (���ݷ� ����, ���� ��)
    float Duration { get; }         // ������ ���� �ð�
    void ApplyEffect(Character character);   // ������ ������ �� ȣ��
    void RemoveEffect(Character character);  // ������ ������ �� ȣ��
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
                // ���� ȿ���� �ڵ� ����ǹǷ� Ư���� ������ �ʿ� ����
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

    // ��ø �� ȿ���� �߰�
    public void StackBuff(float additionalValue)
    {
        totalEffectValue += additionalValue;
    }
}

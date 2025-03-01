using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    AttackBuff,    // ���ݷ� ����
    Bleed          // ���� �����
}

[Serializable]
public class Buff
{
    public BuffType buffType;    // ���� Ÿ��
    public float value;          // ���� ��ġ
    public float duration;       // ���� �ð� (��)
    public float tickInterval;   // ƽ ���� (���� �� �ֱ��� ����)
    public bool isStackable;     // ��ø ���� ����

    public Buff(BuffType type, float value, float duration, bool isStackable = true, float tickInterval = 0)
    {
        this.buffType = type;
        this.value = value;
        this.duration = duration;
        this.isStackable = isStackable;
        this.tickInterval = tickInterval;
    }
}

public class BuffController : MonoBehaviour
{
    private StatData statData; // ������ StatData
    private Dictionary<BuffType, Buff> activeBuffs; // Ȱ��ȭ�� ���� ���

    private void Start()
    {
        activeBuffs = new Dictionary<BuffType, Buff>();
    }

    public void Initialize(StatData statData)
    {
        this.statData = statData;
    }

    // ������ �߰��ϰ� ��ø ó��
    public void AddBuff(Buff newBuff)
    {
        if (activeBuffs.ContainsKey(newBuff.buffType))
        {
            Buff existingBuff = activeBuffs[newBuff.buffType];

            if (newBuff.isStackable)
            {
                // ��ø �����ϸ� �� ���� �� ���ӽð� �ʱ�ȭ
                existingBuff.value += newBuff.value;
                existingBuff.duration = newBuff.duration;
            }
            else
            {
                // ��ø �Ұ����ϸ� ���ӽð��� �ʱ�ȭ
                existingBuff.duration = newBuff.duration;
            }
        }
        else
        {
            // ���ο� ������� �߰��ϰ� ����
            activeBuffs[newBuff.buffType] = newBuff;
            ApplyBuff(newBuff);
            StartCoroutine(RemoveBuffAfterDuration(newBuff));
        }
    }

    // ���� ����
    private void ApplyBuff(Buff buff)
    {
        switch (buff.buffType)
        {
            case BuffType.AttackBuff:
                statData.mBaseAttack += buff.value;
                break;

            case BuffType.Bleed:
                StartCoroutine(ApplyBleedDamage(buff));
                break;
        }
    }

    // �ֱ������� ���� ����� ����
    private IEnumerator ApplyBleedDamage(Buff bleedBuff)
    {
        float remainingTime = bleedBuff.duration;
        while (remainingTime > 0)
        {
            statData.ModifyCurrentHp(bleedBuff.value); // ü�� ����
            remainingTime -= bleedBuff.tickInterval;
            yield return new WaitForSeconds(bleedBuff.tickInterval);
        }
    }

    // ���� ���� �� ����
    private IEnumerator RemoveBuffAfterDuration(Buff buff)
    {
        yield return new WaitForSeconds(buff.duration);
        RemoveBuff(buff);
    }

    // ���� ���� �� ���� ����
    private void RemoveBuff(Buff buff)
    {
        if (activeBuffs.ContainsKey(buff.buffType))
        {
            switch (buff.buffType)
            {
                case BuffType.AttackBuff:
                    statData.mBaseAttack -= buff.value;
                    break;
            }

            activeBuffs.Remove(buff.buffType);
        }
    }
}

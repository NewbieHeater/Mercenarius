using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    AttackBuff,    // 공격력 증가
    Bleed          // 출혈 디버프
}

[Serializable]
public class Buff
{
    public BuffType buffType;    // 버프 타입
    public float value;          // 버프 수치
    public float duration;       // 지속 시간 (초)
    public float tickInterval;   // 틱 간격 (출혈 등 주기적 적용)
    public bool isStackable;     // 중첩 가능 여부

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
    private StatData statData; // 참조할 StatData
    private Dictionary<BuffType, Buff> activeBuffs; // 활성화된 버프 목록

    private void Start()
    {
        activeBuffs = new Dictionary<BuffType, Buff>();
    }

    public void Initialize(StatData statData)
    {
        this.statData = statData;
    }

    // 버프를 추가하고 중첩 처리
    public void AddBuff(Buff newBuff)
    {
        if (activeBuffs.ContainsKey(newBuff.buffType))
        {
            Buff existingBuff = activeBuffs[newBuff.buffType];

            if (newBuff.isStackable)
            {
                // 중첩 가능하면 값 증가 및 지속시간 초기화
                existingBuff.value += newBuff.value;
                existingBuff.duration = newBuff.duration;
            }
            else
            {
                // 중첩 불가능하면 지속시간만 초기화
                existingBuff.duration = newBuff.duration;
            }
        }
        else
        {
            // 새로운 버프라면 추가하고 적용
            activeBuffs[newBuff.buffType] = newBuff;
            ApplyBuff(newBuff);
            StartCoroutine(RemoveBuffAfterDuration(newBuff));
        }
    }

    // 버프 적용
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

    // 주기적으로 출혈 디버프 적용
    private IEnumerator ApplyBleedDamage(Buff bleedBuff)
    {
        float remainingTime = bleedBuff.duration;
        while (remainingTime > 0)
        {
            statData.ModifyCurrentHp(bleedBuff.value); // 체력 감소
            remainingTime -= bleedBuff.tickInterval;
            yield return new WaitForSeconds(bleedBuff.tickInterval);
        }
    }

    // 버프 만료 후 제거
    private IEnumerator RemoveBuffAfterDuration(Buff buff)
    {
        yield return new WaitForSeconds(buff.duration);
        RemoveBuff(buff);
    }

    // 버프 제거 후 원상 복구
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

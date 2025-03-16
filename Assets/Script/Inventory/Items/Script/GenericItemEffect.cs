using UnityEngine;
using System;
using System.Collections.Generic;

public enum EffectType
{
    ArrowOppositeMouse,
    Bleeding,
    PreHitAttackBuff,
    InstantAttackBuffAndHpReduction,
    Hit,
    GiveEffect,
    AddMoreMoney,
    // 추가 효과 타입…
}

[Serializable]
public class GenericItemEffect : ItemEffect
{
    [Header("효과 타입 및 파라미터")]
    public EffectType effectType;
    [Tooltip("효과 값 (예: 출혈 피해량, 공격력 증가량 등)")]
    public float value;
    [Tooltip("효과 지속 시간이나 추가 파라미터 (필요한 경우)")]
    public float duration;
    [Header("장비 이미지 변경 (PreHitAttackBuff 효과용)")]
    [Tooltip("버프 적용 시 장비 슬롯에 표시할 이미지")]
    public Sprite buffActiveSprite;
    [Tooltip("버프 해제 시 장비 슬롯에 표시할 이미지")]
    public Sprite buffInactiveSprite;

    private Character character;
    private InventorySlot equippedSlot;
    List<EnemyGolemController> targets = new List<EnemyGolemController>();

    
    public override void OnEquip(Character character)
    {
        this.character = character;
        switch (effectType)
        {
            case EffectType.ArrowOppositeMouse:
                character.OnAttack += ExecuteArrowEffect;
                break;
            case EffectType.Bleeding:
                // 아이템 획득(장착) 시, 출혈 효과를 적용하기 위해 OnAttack 이벤트에 등록합니다.
                character.OnAttack += ApplyBleedingEffect;
                break;
            case EffectType.PreHitAttackBuff:
                character.statData.mBaseAttack += value;
                character.OnHit += RemovePreHitBuff;
                break;
            case EffectType.InstantAttackBuffAndHpReduction:
                character.OnAttack += RegenerateOnAttack;
                break;
            case EffectType.Hit:
                character.OnHit += HitRegenerate;
                break;
            case EffectType.GiveEffect:
                //character.OnAttack += ExecuteArrowEffect;
                break;
            case EffectType.AddMoreMoney:
                InventoryMain.Instance.multiplyer = 1.2f;
                break;
            default:
                break;
        }
    }
    
    public override void OnUnequip(Character character)
    {
        switch (effectType)
        {
            case EffectType.ArrowOppositeMouse:
                character.OnAttack -= ExecuteArrowEffect;
                break;
            case EffectType.Bleeding:
                character.OnAttack -= ApplyBleedingEffect;
                break;
            case EffectType.PreHitAttackBuff:
                character.statData.mBaseAttack -= value;
                character.OnHit -= RemovePreHitBuff;
                break;
            case EffectType.InstantAttackBuffAndHpReduction:
                character.OnAttack -= RegenerateOnAttack;
                break;
            case EffectType.Hit:
                character.OnHit -= HitRegenerate;
                break;
            case EffectType.GiveEffect:
                //character.OnAttack -= ExecuteArrowEffect;
                break;
            case EffectType.AddMoreMoney:
                InventoryMain.Instance.multiplyer = 1f;
                break;
            default:
                break;
        }
    }
    public void RegenerateOnAttack(List<EnemyGolemController> targets)
    {
        foreach (EnemyGolemController col in targets)
        {
            character.TakeDamage(value);
        }
    }
    private void ExecuteArrowEffect(List<EnemyGolemController> targets)
    {
        Vector3 attackDir = character.GetAttackDirection();
        Vector3 oppositeDir = -attackDir;
        Quaternion spawnRotation = Quaternion.LookRotation(oppositeDir.normalized);
        Vector3 spawnPosition = character.transform.position;
        spawnPosition.y = 1f;
        ObjectPooler.SpawnFromPool("Arrow", spawnPosition, spawnRotation);
        Debug.Log("Generic: 화살이 반대 방향으로 발사됨: " + oppositeDir);
    }

    private void ApplyBleedingEffect(List<EnemyGolemController> targets)
    {
        foreach (EnemyGolemController col in targets)
        {
            
            if (col != null)
            {
                col.ApplyBleedingEffect(value);
                Debug.Log("Generic: 출혈 효과 적용: " + col.name);
                Debug.Log(value);
            }
        }
    }

    private void RemovePreHitBuff(float damage)
    {
        Debug.Log("Generic: 피격으로 인한 공격력 버프 해제");
        character.statData.mBaseAttack -= value;
        if (equippedSlot != null && buffInactiveSprite != null)
            equippedSlot.UpdateEquipmentImage(buffInactiveSprite);
        character.OnHit -= RemovePreHitBuff;
    }

    private void HitRegenerate(float damage)
    {
        Debug.Log("Generic: 피격 효과 발동, 자기 피해: " + value);
        character.TakeDamage(value);
    }
}

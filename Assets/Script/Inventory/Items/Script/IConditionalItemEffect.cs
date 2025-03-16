using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConditionalItemEffect
{
    /// <summary>
    /// 아이템 장착 시 호출 (플레이어에게 효과 적용)
    /// </summary>
    void OnEquip(Character character);
    /// <summary>
    /// 아이템 해제 시 호출 (플레이어에서 효과 제거)
    /// </summary>
    void OnUnequip(Character character);
}
public interface IAttackListener
{
    /// <summary>
    /// 플레이어가 공격할 때 호출됩니다.
    /// </summary>
    void OnAttack();
}
public interface IHitListener
{
    /// <summary>
    /// 플레이어가 피해를 입을 때 호출됩니다.
    /// </summary>
    /// <param name="damage">받은 피해량</param>
    void OnHit(float damage);
}

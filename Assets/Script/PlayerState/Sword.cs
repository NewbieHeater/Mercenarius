using System;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Character
{
    public float AttackRadiusA = 1.5f;
    public float AttackRadiusB = 2f;
    public float AttackRadiusC = 3f;
    

    List<EnemyGolemController> targets = new List<EnemyGolemController>();
    protected override void OnEnable()
    {
        dashCoolDown = 0f;
        base.OnEnable();
    }

    private void Update()
    {
        if (dashCoolDown > 0)
            dashCoolDown -= Time.deltaTime;
        else
            dashCoolDown = -1;
        sm.DoOperateUpdate();
    }

    private void Effect()
    {
        if (TryGetGroundPosition(out Vector3 mouse))
        {
            if (mouse.x < transform.position.x)
            {
                effect[0].Play();
                effect[1].Play();
            }
            else
            {
                effect[2].Play();
                effect[3].Play();
            }
        }
    }

    public override void BasicAttack()
    {
        if (attackComboValue == 0)
        {
            // 반구 공격: 반환된 적들에게 데미지 적용
            targets = GetEnemiesInHemisphere(transform.position, AttackRadiusA);
            DamageEnemies(targets, statData.curAttack * 1.0f, 2);
            SoundManager.Instance.PlaySound2D("DualBlades_atk_" + (attackComboValue + 1));
            Effect();
            
        }
        else if (attackComboValue == 1)
        {
            // 반구 공격: 다른 범위 (AttackRadiusB)
            targets = GetEnemiesInHemisphere(transform.position, AttackRadiusB);
            DamageEnemies(targets, statData.curAttack * 1.5f, 1);
            SoundManager.Instance.PlaySound2D("DualBlades_atk_" + (attackComboValue + 1));
            Effect();
        }
        else if (attackComboValue == 2)
        {
            // 구형 공격: 반환된 적들에게 데미지 적용
            targets = GetEnemiesInSphere(transform.position, AttackRadiusC);
            DamageEnemies(targets, statData.curAttack * 0.8f, 6);
            SoundManager.Instance.PlaySound2D("DualBlades_atk_" + (attackComboValue + 1));
            Effect();
        }
        
    }

    public override void SkillAttack1() { }
    public override void SkillAttack2() { }
    public override void SharedSkill()
    {
        if (SelectedSharedSkill != null)
        {
            SelectedSharedSkill.Execute(this);
            Debug.LogWarning("공용 스킬");
        }
        else
        {
            Debug.LogWarning("공용 스킬이 선택되지 않았습니다.");
        }
    }

    public override void ResetCombo()
    {
        attackComboValue = 0;
        nextAttack = false;
        attackCombo = false;
    }
}

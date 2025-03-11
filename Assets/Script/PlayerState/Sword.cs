using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.VFX;

public class Sword : Character
{
    public float AttackRadiusA = 2.5f;
    public float AttackRadiusB = 3f;
    public float AttackRadiusC = 4f;
    

    protected override void OnEnable()
    {
        dashCoolDown = 0f;
        //Managers.Input.OnMouseButtonDown += OnMouseButtonDown;
        base.OnEnable();
    }
    public void OnMouseButtonDown(int button)
    {
        if (button == 0)
        {
            //agent.SetDestination(MousePosition());
        }
    }
    void OnKeyboard()
    {
        
    }
    private void Update()
    {
        if (dashCoolDown > 0)
        {
            dashCoolDown -= Time.deltaTime;
        }
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
            PerformOptimizedHemisphereAttack(transform.position, AttackRadiusA, statData.curAttack * 1.0f, 2);
            SoundManager.Instance.PlaySound2D("DualBlades_atk_" + (attackComboValue + 1));
            Effect();
        }
        else if (attackComboValue == 1)
        {
            // 두 번째 공격: 반원 공격 1회
            PerformOptimizedHemisphereAttack(transform.position, AttackRadiusB, statData.curAttack * 1.5f, 1);
            SoundManager.Instance.PlaySound2D("DualBlades_atk_" + (attackComboValue + 1));
            Effect();
        }
        else if (attackComboValue == 2)
        {
            // 세 번째 공격: 원형 공격 5회
            PerformOptimizedSphericalAttack(transform.position, AttackRadiusC, statData.curAttack * 0.8f, 6);
            SoundManager.Instance.PlaySound2D("DualBlades_atk_" + (attackComboValue + 1));
            Effect();
        }
        
    }
    public override void SkillAttack1()
    {

    }
    public override void SkillAttack2()
    {

    }
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
        animator.SetInteger("AttackCombo", attackComboValue);
        attackCombo = false;
    }
}
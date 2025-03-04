using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.VFX;

public class Sword : Character
{
    int attackComboValue = 0;
    
    protected override void OnEnable()
    {
        
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
        
        sm.DoOperateUpdate();
    }
    
    public override void BasicAttack()
    {
        
        if (attackCombo == true)
            attackComboValue++;
        //character.animator.SetTrigger("NextCombo");
        
        animator.SetInteger("AttackCombo", attackComboValue);
        
        if (attackComboValue >= 3)
        {
            attackComboValue = 0;
        }
        FlipSpriteByMousePosition();
        if(!TryGetGroundPosition(out Vector3 LookPosition))
        attackTransform.transform.LookAt(LookPosition);
        attackCombo = false;
        
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
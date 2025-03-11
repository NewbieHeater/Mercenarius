using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.VFX;

public class Spear : Character
{

    [Header("Attack Range Visualization")]
    private Vector3 boxAttackFirst = new Vector3(1, 2,2);
    private Vector3 boxAttackSecond = new Vector3(1, 2, 2);
    private Vector3 boxAttackThird = new Vector3(2, 2, 3);    


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
        // 공격 콤보에 따른 패턴 실행
        if (attackComboValue == 0)
        {
            PerformOptimizedBoxAttackInFront(transform.position, boxAttackFirst, statData.curAttack * 1.0f, 2);
            SoundManager.Instance.PlaySound2D("Spear_atk_" + (attackComboValue + 1));
        }
        else if (attackComboValue == 1)
        {
            // 두 번째 공격: 반원 공격 1회
            PerformOptimizedBoxAttackInFront(transform.position, boxAttackSecond, statData.curAttack * 1.5f, 1);
            SoundManager.Instance.PlaySound2D("Spear_atk_" + (attackComboValue + 1));
        }
        else if (attackComboValue == 2)
        {
            // 세 번째 공격: 원형 공격 5회
            PerformOptimizedBoxAttackInFront(transform.position, boxAttackThird, statData.curAttack * 0.8f, 6);
            SoundManager.Instance.PlaySound2D("Spear_atk_" + (attackComboValue + 1));
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
        Debug.Log("asd");
        attackComboValue = 0;
        attackCombo = false;
    }
}
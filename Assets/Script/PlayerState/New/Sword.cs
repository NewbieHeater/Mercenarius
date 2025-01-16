using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : Character
{
    protected override void Start()
    {
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        base.Start();
    }
    void OnMouseClicked(Define.MouseEvent evt)
    {

    }

    void OnKeyboard()
    {
        if (KeyManager.Instance.GetKeyDown("SkillQuickSlot1"))
        {
            Managers.UI.ShowPopupUI<UI_Button>();
        }
    }
    private void Update()
    {
        
        sm.DoOperateUpdate();
    }

    protected override void BasicAttack()
    {
        agent.isStopped = true;
        agent.SetDestination(transform.position);
        animator.SetTrigger("isAttack");
        animator.SetBool("Attack", true);
        SetAttackDirection();
    }
    protected override void SkillAttack1()
    {

    }
    protected override void SkillAttack2()
    {

    }
}
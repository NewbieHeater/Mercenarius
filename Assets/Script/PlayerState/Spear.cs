using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.VFX;

public class Spear : Character
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
    /// <summary>
    /// ĳ����(������) �������� ���콺 ������ �ݿ� ���� ���� target�� �ִ��� �Ǻ��մϴ�.
    /// </summary>
    /// <param name="attacker">������ Transform</param>
    /// <param name="target">������ ��� Transform</param>
    /// <param name="attackRange">���� �ݰ�</param>
    /// <returns>����� �ݿ� ���� ���� ������ true, �ƴϸ� false</returns>
    public bool IsTargetInMouseSemicircle(Transform attacker, Transform target, float attackRange)
    {
        // ĳ���Ͱ� ���콺�� �� �� ��ġ�� �����´ٰ� ���� (TryGetGroundPosition �޼��� ���)
        Vector3 mouseWorldPos;
        Character characterComponent = attacker.GetComponent<Character>();

        // ���콺 ��ġ�� ���������� �޾ƿ���, �� ������ ���� �������� ����մϴ�.
        // �����ϸ�, fallback���� �������� ����(��: transform.forward)�� ����մϴ�.
        Vector3 attackDirection;
        if (characterComponent != null && characterComponent.TryGetGroundPosition(out mouseWorldPos))
        {
            attackDirection = (mouseWorldPos - attacker.position).normalized;
        }
        else
        {
            attackDirection = attacker.forward; // fallback
        }

        // ������ ��� ����
        Vector3 toTarget = target.position - attacker.position;
        // �Ÿ� ����
        if (toTarget.magnitude > attackRange)
            return false;

        // ���� ����� ��� ������ ���� ���
        float angle = Vector3.Angle(attackDirection, toTarget);

        // �ݿ� ����: ���� ������ �߽����� ��90�� ���� �ִٸ�
        return angle <= 90f;
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
        if (!TryGetGroundPosition(out Vector3 LookPosition))
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
            Debug.LogWarning("���� ��ų");
        }
        else
        {
            Debug.LogWarning("���� ��ų�� ���õ��� �ʾҽ��ϴ�.");
        }
    }

    public override void ResetCombo()
    {
        attackComboValue = 0;
        animator.SetInteger("AttackCombo", attackComboValue);
        attackCombo = false;
    }
}
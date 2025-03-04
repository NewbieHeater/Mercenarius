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
    /// 캐릭터(공격자) 기준으로 마우스 방향의 반원 영역 내에 target이 있는지 판별합니다.
    /// </summary>
    /// <param name="attacker">공격자 Transform</param>
    /// <param name="target">판정할 대상 Transform</param>
    /// <param name="attackRange">공격 반경</param>
    /// <returns>대상이 반원 영역 내에 있으면 true, 아니면 false</returns>
    public bool IsTargetInMouseSemicircle(Transform attacker, Transform target, float attackRange)
    {
        // 캐릭터가 마우스의 땅 위 위치를 가져온다고 가정 (TryGetGroundPosition 메서드 사용)
        Vector3 mouseWorldPos;
        Character characterComponent = attacker.GetComponent<Character>();

        // 마우스 위치를 성공적으로 받아오면, 그 방향을 공격 방향으로 사용합니다.
        // 실패하면, fallback으로 공격자의 앞쪽(예: transform.forward)을 사용합니다.
        Vector3 attackDirection;
        if (characterComponent != null && characterComponent.TryGetGroundPosition(out mouseWorldPos))
        {
            attackDirection = (mouseWorldPos - attacker.position).normalized;
        }
        else
        {
            attackDirection = attacker.forward; // fallback
        }

        // 대상과의 상대 벡터
        Vector3 toTarget = target.position - attacker.position;
        // 거리 판정
        if (toTarget.magnitude > attackRange)
            return false;

        // 공격 방향과 대상 사이의 각도 계산
        float angle = Vector3.Angle(attackDirection, toTarget);

        // 반원 영역: 공격 방향을 중심으로 ±90도 내에 있다면
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
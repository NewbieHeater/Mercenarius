using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Spear : Character
{
    [Header("Attack Range Visualization")]
    // 각 공격 단계에서 박스(혹은 반경) 크기를 정의 (여기서는 half extents로 사용)
    [SerializeField] private Vector3 boxAttackFirst = new Vector3(1, 2, 2);
    [SerializeField] private Vector3 boxAttackSecond = new Vector3(1, 2, 2);
    [SerializeField] private Vector3 boxAttackThird = new Vector3(2, 2, 3);
    // 구형 공격 반경 (AttackRadiusC 대신 사용)
    [SerializeField] private float sphereAttackRadius = 4f;

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

    // 기존 효과 함수 (예시)
    private void PlayEffect()
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
        // 먼저 공격 방향과 회전값 계산
        Vector3 attackDirection = GetAttackDirection();
        Quaternion orientation = Quaternion.LookRotation(attackDirection);

        if (attackComboValue == 0)
        {
            // 첫 번째 공격: 반구/박스 공격
            // 박스의 중심은 공격 방향으로 boxAttackFirst.z 만큼 오프셋
            Vector3 center = transform.position + attackDirection * boxAttackFirst.z;
            // GetEnemiesInBox는 center, halfExtents, orientation을 인자로 받아 리스트 반환
            List<EnemyGolemController> targets = GetEnemiesInBox(center, boxAttackFirst, orientation);
            // 반환된 적들에게 데미지 적용 (hitCount 2회)
            DamageEnemies(targets, statData.curAttack * 1.0f, 2);
            SoundManager.Instance.PlaySound2D("Spear_atk_" + (attackComboValue + 1));

        }
        else if (attackComboValue == 1)
        {
            // 두 번째 공격: 반구/박스 공격 (다른 크기)
            Vector3 center = transform.position + attackDirection * boxAttackSecond.z;
            List<EnemyGolemController> targets = GetEnemiesInBox(center, boxAttackSecond, orientation);
            DamageEnemies(targets, statData.curAttack * 1.5f, 1);
            SoundManager.Instance.PlaySound2D("Spear_atk_" + (attackComboValue + 1));

        }
        else if (attackComboValue == 2)
        {
            // 세 번째 공격: 구형 공격
            Vector3 center = transform.position; // 플레이어를 중심으로
            List<EnemyGolemController> targets = GetEnemiesInSphere(center, sphereAttackRadius);
            DamageEnemies(targets, statData.curAttack * 0.8f, 6);
            SoundManager.Instance.PlaySound2D("Spear_atk_" + (attackComboValue + 1));

        }
        base.BasicAttack();
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
        attackCombo = false;
    }
}

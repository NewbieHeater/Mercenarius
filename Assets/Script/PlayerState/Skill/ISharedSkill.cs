using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public interface ISharedSkill
{
    void Execute(Character sender);
}

public class DaggerThrow : ISharedSkill
{
    public void Execute(Character sender)
    {
        Vector3 playerPosition = sender.transform.position;
        Vector3 mouseWorldPosition;
        if (!sender.TryGetGroundPosition(out mouseWorldPosition))
            return;
        playerPosition.y = 1;
        mouseWorldPosition.y = 1;
        Vector3 directionCenter = (mouseWorldPosition - playerPosition).normalized;
        Vector3 directionRight = Quaternion.AngleAxis(20f, Vector3.up) * directionCenter;

        // 왼쪽 30도 회전: Y축을 기준으로 -20도 회전
        Vector3 directionLeft = Quaternion.AngleAxis(-20f, Vector3.up) * directionCenter;
        ObjectPooler.SpawnFromPool("SharedSkillDagger", playerPosition, Quaternion.LookRotation(directionLeft));
        ObjectPooler.SpawnFromPool("SharedSkillDagger", playerPosition, Quaternion.LookRotation(directionCenter));
        ObjectPooler.SpawnFromPool("SharedSkillDagger", playerPosition, Quaternion.LookRotation(directionRight));
        Debug.Log("Fireball 스킬 실행");
        // 추가 이펙트, 데미지 계산 등 로직...
    }
}

public class IceBlastSkill : ISharedSkill
{
    public void Execute(Character sender)
    {
        // IceBlast 스킬 로직 구현 예시
        sender.animator.SetTrigger("IceBlast");
        Debug.Log("IceBlast 스킬 실행");
        // 추가 이펙트, 데미지 계산 등 로직...
    }
}

public class LightningStrikeSkill : ISharedSkill
{
    public void Execute(Character sender)
    {
        // LightningStrike 스킬 로직 구현 예시
        sender.animator.SetTrigger("LightningStrike");
        Debug.Log("LightningStrike 스킬 실행");
        // 추가 이펙트, 데미지 계산 등 로직...
    }
}

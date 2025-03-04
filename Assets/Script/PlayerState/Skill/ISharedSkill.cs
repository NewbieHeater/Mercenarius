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

        // ���� 30�� ȸ��: Y���� �������� -20�� ȸ��
        Vector3 directionLeft = Quaternion.AngleAxis(-20f, Vector3.up) * directionCenter;
        ObjectPooler.SpawnFromPool("SharedSkillDagger", playerPosition, Quaternion.LookRotation(directionLeft));
        ObjectPooler.SpawnFromPool("SharedSkillDagger", playerPosition, Quaternion.LookRotation(directionCenter));
        ObjectPooler.SpawnFromPool("SharedSkillDagger", playerPosition, Quaternion.LookRotation(directionRight));
        Debug.Log("Fireball ��ų ����");
        // �߰� ����Ʈ, ������ ��� �� ����...
    }
}

public class IceBlastSkill : ISharedSkill
{
    public void Execute(Character sender)
    {
        // IceBlast ��ų ���� ���� ����
        sender.animator.SetTrigger("IceBlast");
        Debug.Log("IceBlast ��ų ����");
        // �߰� ����Ʈ, ������ ��� �� ����...
    }
}

public class LightningStrikeSkill : ISharedSkill
{
    public void Execute(Character sender)
    {
        // LightningStrike ��ų ���� ���� ����
        sender.animator.SetTrigger("LightningStrike");
        Debug.Log("LightningStrike ��ų ����");
        // �߰� ����Ʈ, ������ ��� �� ����...
    }
}

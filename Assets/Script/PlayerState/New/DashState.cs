using System;
using UnityEngine;

public class DashState : IState<Character>
{
    private Character character;
    private float dashPower = 0;
    private float dashSpeed = 0;
    private float dashTime = 0;
    private float time = 0;
    private Vector3 dashDest;
    private Vector3 curPosition;

    private int wallLayerMask;

    public void OperateEnter(Character sender)
    {
        character = sender;

        if (character.agent != null)
        {
            character.agent.isStopped = true;
            character.agent.enabled = false;
        }
        
        wallLayerMask = 1 << LayerMask.NameToLayer("Wall");

        dashDest = SetDashDestination();
        curPosition = character.transform.position;
        character.FlipSpriteByMousePosition();
        time = 0; // Ÿ�̸� �ʱ�ȭ
        character.animator.SetBool("Dash", true);
    }

    public void OperateExit(Character sender)
    {
        //character.transform.position = dashDest;
        character.agent.enabled = true;
        character.animator.SetBool("Dash", false);
    }

    public void OperateUpdate(Character sender)
    {
        if (time >= dashTime)
        {
            character.sm.SetState(character.dicState["Idle"]);
            return;
        }

        // ��� ����
        float t = time / dashTime;
        character.transform.position = Vector3.Lerp(curPosition, dashDest, t);

        time += Time.fixedDeltaTime;
    }

    private Vector3 SetDashDestination()
    {
        Vector3 mousePosition = character.MousePosition();
        Debug.Log("Mouse Position: " + mousePosition);
        Vector3 dashDestDir = (mousePosition - character.transform.position).normalized;
        RaycastHit dashHit;
        float margin = 0.25f;  // ���� ���� ����(margin)�� ����

        if (Physics.Raycast(character.transform.position, dashDestDir, out dashHit, character.statData.curDashPower, wallLayerMask))
        {
            float distanceToWall = dashHit.distance;
            // �������� �Ÿ����� margin�� �� ���� dashPower�� ��� (�ּҰ��� ����)
            dashPower = Mathf.Max(distanceToWall - margin, 0.1f);
            dashSpeed = character.statData.curDashSpeed;
            // dashTime�� dashPower�� dashSpeed�� ������ ��� (�Ÿ��� ª���� �ð��� ª����)
            dashTime = dashPower / (dashSpeed * 1.3f);
        }
        else
        {
            // ���� ������ �⺻ �뽬 �Ÿ��� �ӵ� ���
            dashPower = character.statData.curDashPower;
            dashSpeed = character.statData.curDashSpeed;
            dashTime = dashPower / dashSpeed;
        }
        Debug.Log($"Dash Power: {dashPower}, Dash Time: {dashTime}, Dash Speed: {dashSpeed}");
        return character.transform.position + dashDestDir * dashPower;
    }
}

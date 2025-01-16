using UnityEngine;

public class DashState : IState<Character>
{
    private Character character;
    private float dashPower = 0;
    private float dashSpeed = 0;
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
        }
        character.agent.SetDestination(character.transform.position);

        wallLayerMask = 1 << LayerMask.NameToLayer("Wall");

        dashDest = SetDashDestination();
        curPosition = character.transform.position;
        character.FlipSpriteByMousePosition();
        time = 0; // Ÿ�̸� �ʱ�ȭ
        character.animator.SetBool("Dash", true);
    }

    public void OperateExit(Character sender)
    {
        character.animator.SetBool("Dash", false);
    }

    public void OperateUpdate(Character sender)
    {
        if (time >= dashSpeed)
        {
            character.sm.SetState(character.dicState["Idle"]);
            return;
        }

        // ��� ����
        character.transform.position = Vector3.Lerp(curPosition, dashDest, time);

        // �ð� ����� ���� �̵� ���� ���
        time += Time.deltaTime * dashSpeed;
    }

    private Vector3 SetDashDestination()
    {
        Vector3 mousePosition = character.MousePosition();
        Vector3 dashDestDir = (mousePosition - character.transform.position).normalized;
        RaycastHit dashHit;

        if (Physics.Raycast(character.transform.position, dashDestDir, out dashHit, character.statData.curDashPower, wallLayerMask))
        {
            dashPower = dashHit.distance - 0.35f;    // �� �Ÿ���ŭ ��� �Ÿ� ����
            dashSpeed = character.statData.curDashSpeed;
        }
        else
        {
            dashPower = character.statData.curDashPower;
            dashSpeed = character.statData.curDashSpeed;
        }

        // ��� ������ ���
        return character.transform.position + dashDestDir * dashPower;
    }
}

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
        time = 0; // 타이머 초기화
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

        // 대시 진행
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
        float margin = 0.25f;  // 벽과 일정 간격(margin)을 유지

        if (Physics.Raycast(character.transform.position, dashDestDir, out dashHit, character.statData.curDashPower, wallLayerMask))
        {
            float distanceToWall = dashHit.distance;
            // 벽까지의 거리에서 margin을 뺀 값을 dashPower로 사용 (최소값을 보장)
            dashPower = Mathf.Max(distanceToWall - margin, 0.1f);
            dashSpeed = character.statData.curDashSpeed;
            // dashTime은 dashPower와 dashSpeed의 비율로 계산 (거리가 짧으면 시간도 짧아짐)
            dashTime = dashPower / (dashSpeed * 1.3f);
        }
        else
        {
            // 벽이 없으면 기본 대쉬 거리와 속도 사용
            dashPower = character.statData.curDashPower;
            dashSpeed = character.statData.curDashSpeed;
            dashTime = dashPower / dashSpeed;
        }
        Debug.Log($"Dash Power: {dashPower}, Dash Time: {dashTime}, Dash Speed: {dashSpeed}");
        return character.transform.position + dashDestDir * dashPower;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static PlayerController;

public class PlayerDashState : MonoBehaviour, IState<PlayerController>
{
    private PlayerController _playerController;
    private NavMeshAgent agent;
    private Animator anim;

    private Vector3 dashDest;
    private Vector3 curPosition;
    private RaycastHit dashHit;
    
    private float time = 0;
    
    public void OperateEnter(PlayerController sender)
    {
        _playerController = sender;
        
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!anim) anim = GetComponentInChildren<Animator>();

        StartCoroutine(CoolDown(_playerController.coolDownDash, _playerController.imgCool));

        _playerController.isAttack = false;
        _playerController.isInvincible = true;
        agent.enabled = false;
        time = 0;

        anim.SetBool("Idle", false);
        anim.SetBool("Dash", true);
        anim.SetBool("Run", false);
        anim.SetBool("Attack", false);
        anim.ResetTrigger("isAttack");

        SetDashDestination();
        
        if (dashDest.x < curPosition.x)
        {
            _playerController.isFacingRight = false;
        }
        else
        {
            _playerController.isFacingRight = true;
        }
    }
    
    public void OperateUpdate(PlayerController sender)
    {
        if(time >= (1f / _playerController.dashSpeed))
        {
            anim.SetBool("Dash", false);
            return;
        }
            
        transform.position = Vector3.Lerp(curPosition, dashDest, time * _playerController.dashSpeed);
        time += Time.deltaTime;
    }

    public void OperateExit(PlayerController sender)
    {
        agent.enabled = true;
        _playerController.isInvincible = false;
        _playerController.dashPower = _playerController.dashPowerOrigin;
        _playerController.dashSpeed = _playerController.dashSpeedOrigin;
    }

    IEnumerator CoolDown(float cool, Image coolDownSkill)
    {
        float tick = 1f / cool;
        float t = 0;
        //스킬 사용후 아이콘에 회색 채우기
        coolDownSkill.fillAmount = 1;

        // 10초에 걸쳐 1 -> 0 으로 변경하는 값을
        // imgCool.fillAmout 에 넣어주는 코드
        while (coolDownSkill.fillAmount > 0)
        {
            coolDownSkill.fillAmount = Mathf.Lerp(1, 0, t);
            t += (Time.deltaTime * tick);

            yield return null;
        }
    }

    private void SetDashDestination()
    {
        Vector3 mousePosition = _playerController.CheckGround(Input.mousePosition);
        Vector3 dashDestDir = (mousePosition - transform.position).normalized;

        if (Physics.Raycast(transform.position, dashDestDir, out dashHit, _playerController.dashPower, 1 << LayerMask.NameToLayer("Wall")))
        {
            _playerController.dashPower = dashHit.distance - 0.3f;    //벽 거리만큼 대쉬 거리 줄임
            _playerController.dashSpeed = dashHit.distance + _playerController.dashSpeed;
        }
        else
        {
            _playerController.dashPower = _playerController.dashPowerOrigin;
            _playerController.dashSpeed = _playerController.dashSpeedOrigin;
        }

        dashDest = transform.position + dashDestDir * _playerController.dashPower;
        curPosition = transform.position;
    }
}

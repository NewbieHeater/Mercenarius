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
        time = 0;
        anim.SetBool("Dash", true);
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        _playerController.isInvincible = true;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            //���콺 Ŭ�� ��ġ
            Vector3 dashDestPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            //���콺 Ŭ�� ��ġ - ���� ��ġ (�������)
            Vector3 dashDestDir = (dashDestPos - transform.position).normalized;
            //�÷��̾�� ���콺�� Ray�߻�
            if (Physics.Raycast(transform.position, dashDestDir, out dashHit, _playerController.dashPower))
                if (dashHit.collider.CompareTag("Wall"))                      //Ray�� ���� ������
                {
                    _playerController.dashPower = dashHit.distance - 0.3f;    //�� �Ÿ���ŭ �뽬 �Ÿ� ����
                    _playerController.dashSpeed = dashHit.distance + _playerController.dashSpeed;
                }

            //������ǥ ��ġ
            dashDest = transform.position + dashDestDir * _playerController.dashPower;
            curPosition = transform.position;
        }
        else
        {
            anim.SetBool("Dash", false);
        }

        if (dashDest.x < curPosition.x)
        {
            _playerController.isFacingRight = false;
        }
        else
        {
            _playerController.isFacingRight = true;
        }
    }

    IEnumerator CoolDown(float cool, Image coolDownSkill)
    {
        float tick = 1f / cool;
        float t = 0;
        //��ų ����� �����ܿ� ȸ�� ä���
        coolDownSkill.fillAmount = 1;

        // 10�ʿ� ���� 1 -> 0 ���� �����ϴ� ����
        // imgCool.fillAmout �� �־��ִ� �ڵ�
        while (coolDownSkill.fillAmount > 0)
        {
            coolDownSkill.fillAmount = Mathf.Lerp(1, 0, t);
            t += (Time.deltaTime * tick);

            yield return null;
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
        _playerController.isInvincible = false;
        _playerController.dashPower = _playerController.dashPowerOrigin;
    }
}
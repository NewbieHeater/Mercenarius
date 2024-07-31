using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerRollState : MonoBehaviour, IState<PlayerController>
{
    private PlayerController _playerController;
    private NavMeshAgent agent;
    private Animator anim;
    private RaycastHit dashHit;

    private Vector3 dashDest;
    private Vector3 curPosition;
    private float time = 0;

    public void OperateEnter(PlayerController sender)
    {
        _playerController = sender;
        _playerController.isAttack = false;
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!anim) anim = GetComponentInChildren<Animator>();

        StartCoroutine(CoolDown(_playerController.coolDownDash, _playerController.imgCool));

        time = 0;
        anim.SetBool("Dash", true);
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            //���콺 Ŭ�� ��ġ
            Vector3 dashDestPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            //���콺 Ŭ�� ��ġ - ���� ��ġ (�������)
            Vector3 dashDestDir = (dashDestPos - transform.position).normalized;
            //�÷��̾�� ���콺�� Ray�߻�
            if (Physics.Raycast(transform.position, dashDestDir, out dashHit, _playerController.dashPower))
                if (dashHit.collider.CompareTag("Wall") || dashHit.collider.CompareTag("Void"))                      //Ray�� ���� ������
                    _playerController.dashPower = dashHit.distance - 0.3f;    //�� �Ÿ���ŭ �뽬 �Ÿ� ����


            //������ǥ ��ġ
            dashDest = transform.position + dashDestDir * _playerController.dashPower;
            curPosition = transform.position;
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
        if (time >= (1f / _playerController.dashSpeed))
        {
            anim.SetBool("Dash", false);
            return;
        }
        transform.position = Vector3.Lerp(curPosition, dashDest, time * _playerController.dashSpeed);
        time += Time.deltaTime;
    }

    public void OperateExit(PlayerController sender)
    {
        
    }
}

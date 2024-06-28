using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerController;

public class PlayerRollState : MonoBehaviour, IState<PlayerController>
{
    private PlayerController _playerController;

    public void OperateEnter(PlayerController sender)
    {
        _playerController = sender;
        StartCoroutine(CoolDown(_playerController.Soskill.Cooltime, _playerController.imgCool));

        _playerController.anim.SetTrigger("Dashing");
        _playerController.agent.isStopped = true;
        _playerController.agent.velocity = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //���콺 Ŭ�� ��ġ
            Vector3 dashDestPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            //���콺 Ŭ�� ��ġ - ���� ��ġ (�������)
            Vector3 dashDestDir = (dashDestPos - transform.position).normalized;
            //�÷��̾�� ���콺�� Ray�߻�
            if (Physics.Raycast(transform.position, dashDestDir, out _playerController.DashHit, _playerController.dashPower))
                if (_playerController.DashHit.collider.tag.Equals("Wall") || _playerController.DashHit.collider.tag.Equals("Void"))                      //Ray�� ���� ������
                    _playerController.dashPower = _playerController.DashHit.distance - 0.3f;    //�� �Ÿ���ŭ �뽬 �Ÿ� ����


            //������ǥ ��ġ
            Vector3 dashDest = transform.position + dashDestDir * _playerController.dashPower;
            Vector3 curPosition = transform.position;
            StartCoroutine(Dash(dashDest, curPosition));

        }
    }
    IEnumerator Dash(Vector3 Dest, Vector3 pos)
    {
        Debug.Log(Dest.x);
        //�ȱ�� ����ϰ� �¿�������ֱ�
        _playerController.spriteRender.flipX = Dest.x < pos.x;
        float t = 0;
        while (t < (1f / _playerController.dashSpeed))
        {
            //�̵�
            transform.position = Vector3.Lerp(pos, Dest, t * _playerController.dashSpeed);
            t += Time.deltaTime;
            yield return null;
        }
        _playerController.DashEnable = false;
        _playerController.dashPower = _playerController.dashPowerOrigin;
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

    }

    public void OperateExit(PlayerController sender)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerController;

public class PlayerDashState : MonoBehaviour, IState<PlayerController>
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
            //마우스 클릭 위치
            Vector3 dashDestPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            //마우스 클릭 위치 - 현재 위치 (각도계산)
            Vector3 dashDestDir = (dashDestPos - transform.position).normalized;

            if (Physics.Raycast(transform.position, dashDestDir, out _playerController.DashHit, _playerController.dashPower))
                if (_playerController.DashHit.collider.tag.Equals("Wall"))
                    _playerController.dashPower = _playerController.DashHit.distance - 0.3f;

            //최종목표 위치
            Vector3 dashDest = transform.position + dashDestDir * _playerController.dashPower;
            Vector3 curPosition = transform.position;
            StartCoroutine(Dash(dashDest, curPosition));

        }
    }
    IEnumerator Dash(Vector3 Dest, Vector3 pos)
    {
        _playerController.spriteRender.flipX = Dest.x < pos.x;
        float t = 0;
        while (t < (1f/_playerController.dashSpeed))
        {

            transform.position = Vector3.Lerp(pos, Dest, t * _playerController.dashSpeed);
            t += Time.deltaTime;
            Debug.Log(t);
            yield return null;
        }
        Debug.Log("대쉬끗");
        _playerController.Dash = true;
        _playerController.dashPower = _playerController.dashPowerOrigin;
    }

    IEnumerator CoolDown(float cool, Image coolDownSkill)
    {
        float tick = 1f / cool;
        float t = 0;

        coolDownSkill.fillAmount = 1;

        // 10초에 걸쳐 1 -> 0 으로 변경하는 값을
        // imgCool.fillAmout 에 넣어주는 코드
        while (coolDownSkill.fillAmount > 0)
        {
            coolDownSkill.fillAmount = Mathf.Lerp(1, 0, t);
            t += (Time.deltaTime * tick);

            yield return null;
        }
        Debug.Log("쿨다운끝");
    }
    public void OperateUpdate(PlayerController sender)
    {

    }

    public void OperateExit(PlayerController sender)
    {
        
    }
}

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
            //마우스 클릭 위치
            Vector3 dashDestPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            //마우스 클릭 위치 - 현재 위치 (각도계산)
            Vector3 dashDestDir = (dashDestPos - transform.position).normalized;
            //플레이어에서 마우스로 Ray발사
            if (Physics.Raycast(transform.position, dashDestDir, out _playerController.DashHit, _playerController.dashPower))
                if (_playerController.DashHit.collider.tag.Equals("Wall") || _playerController.DashHit.collider.tag.Equals("Void"))                      //Ray가 벽에 닿으면
                    _playerController.dashPower = _playerController.DashHit.distance - 0.3f;    //벽 거리만큼 대쉬 거리 줄임


            //최종목표 위치
            Vector3 dashDest = transform.position + dashDestDir * _playerController.dashPower;
            Vector3 curPosition = transform.position;
            StartCoroutine(Dash(dashDest, curPosition));

        }
    }
    IEnumerator Dash(Vector3 Dest, Vector3 pos)
    {
        Debug.Log(Dest.x);
        //걷기와 비슷하게 좌우반전해주기
        _playerController.spriteRender.flipX = Dest.x < pos.x;
        float t = 0;
        while (t < (1f / _playerController.dashSpeed))
        {
            //이동
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
    public void OperateUpdate(PlayerController sender)
    {

    }

    public void OperateExit(PlayerController sender)
    {

    }
}

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
            //마우스 클릭 위치
            Vector3 dashDestPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            //마우스 클릭 위치 - 현재 위치 (각도계산)
            Vector3 dashDestDir = (dashDestPos - transform.position).normalized;
            //플레이어에서 마우스로 Ray발사
            if (Physics.Raycast(transform.position, dashDestDir, out dashHit, _playerController.dashPower))
                if (dashHit.collider.CompareTag("Wall") || dashHit.collider.CompareTag("Void"))                      //Ray가 벽에 닿으면
                    _playerController.dashPower = dashHit.distance - 0.3f;    //벽 거리만큼 대쉬 거리 줄임


            //최종목표 위치
            dashDest = transform.position + dashDestDir * _playerController.dashPower;
            curPosition = transform.position;
        }
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

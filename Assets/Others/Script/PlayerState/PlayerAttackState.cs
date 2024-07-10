using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : MonoBehaviour, IState<PlayerController>
{
    private PlayerController _playerController;

    public void OperateEnter(PlayerController sender)
    {
        _playerController = sender;
        _playerController.anim.SetBool("Attack", true);
        StartCoroutine(StartAttack());
        // 레이를 쏘아 목표 방향을 설정
        _playerController.agent.isStopped = true;
        _playerController.agent.velocity = Vector3.zero;

    }
    IEnumerator StartAttack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            // 플레이어 오브젝트의 회전 각도 계산
            //, Mathf.Infinity
            Vector3 LookRotation = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Quaternion lookTarget = Quaternion.LookRotation(LookRotation - transform.position);
            // 플레이어 위치의 오른쪽으로 farDistance 거리만큼 떨어진 위치 계산
            Vector3 sidePos1 = transform.position;
            _playerController.spriteRender.flipX = hit.point.x < transform.position.x;
            _playerController.weaponHitBox.transform.LookAt(LookRotation);
            _playerController.weaponHitBox.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            //_playerController.anim.SetBool("Attack", false);
            _playerController.weaponHitBox.SetActive(false);
            yield return null;
        }
    }

    public void OperateUpdate(PlayerController sender)
    {
        if(AnimationEnd.Instance.attackAnimationEnded == true)
        {
            _playerController.anim.SetBool("Attack", false);
        }
        //_playerController.anim
    }

    public void OperateExit(PlayerController sender)
    {
        _playerController.anim.SetBool("Attack", false);
    }
}

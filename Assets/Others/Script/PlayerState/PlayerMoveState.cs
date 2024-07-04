using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerMoveState : MonoBehaviour, IState<PlayerController>
{
    private PlayerController _playerController;

    public void OperateEnter(PlayerController sender)
    {
        _playerController = sender;
        Move();
    }

    public void OperateUpdate(PlayerController sender)
    {
        //Debug.Log("움직임");
        if (Input.GetMouseButtonDown(0))
        {
            //달리기가 안끝났을 때 클릭시 방향전환
            _playerController.anim.SetBool("Running", true);
            Move();
        }
        else if (_playerController.agent.remainingDistance < 0.1f)
        {
            //애니메이션 종료
            _playerController.anim.SetBool("Running", false);

            //이동목표의 과녁 비활성화
            _playerController.spot.gameObject.SetActive(false);
        }
    }

    public void OperateExit(PlayerController sender)
    {
        //State를 나갈 때 애니매이션 달리기 상태 해제
        //_playerController.anim.SetBool("Running", false);
        //_playerController.agent.isStopped = true;
        //_playerController.agent.velocity = Vector3.zero;
    }

    //이동로직
    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            //이동목표지점이 현재 지점보다 오른쪽이면 캐릭터 그림 방향 전환
            _playerController.spriteRender.flipX = hit.point.x < transform.position.x;
            _playerController.agent.isStopped = false;
            //이동
            _playerController.agent.SetDestination(hit.point);
            //달리기 상태 활성
            _playerController.anim.SetBool("Running", true);
            //과녁 활성화
            _playerController.spot.gameObject.SetActive(true);
            _playerController.spot.position = hit.point;
        }
    }
}

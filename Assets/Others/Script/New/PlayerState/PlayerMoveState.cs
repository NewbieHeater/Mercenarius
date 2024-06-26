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
        Debug.Log("������");
        //Debug.Log(_playerController.agent.remainingDistance);
        _playerController = sender;
        //ī�޶󿡼� ���콺�� �����߽�����
        Move();
    }

    public void OperateUpdate(PlayerController sender)
    {
        //Debug.Log("������");
        if (Input.GetMouseButtonDown(0))
        {
            Move();
        }
        //else if (_playerController.agent.remainingDistance < 0.1f) ;
        else if (_playerController.agent.remainingDistance < 0.1f)
        {
            //�ִϸ��̼� ����
            _playerController.anim.SetBool("Running", false);

            //�̵���ǥ�� ���� ��Ȱ��ȭ
            _playerController.spot.gameObject.SetActive(false);
        }
    }

    public void OperateExit(PlayerController sender)
    {
        _playerController.anim.SetBool("Running", false);
    }

    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _playerController.spriteRender.flipX = hit.point.x < transform.position.x;
            _playerController.agent.isStopped = false;
            //�̵�
            _playerController.agent.SetDestination(hit.point);
            _playerController.anim.SetBool("Running", true);
            //���� Ȱ��ȭ
            _playerController.spot.gameObject.SetActive(true);
            _playerController.spot.position = hit.point;
        }
    }
}

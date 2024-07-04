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
        //Debug.Log("������");
        if (Input.GetMouseButtonDown(0))
        {
            //�޸��Ⱑ �ȳ����� �� Ŭ���� ������ȯ
            _playerController.anim.SetBool("Running", true);
            Move();
        }
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
        //State�� ���� �� �ִϸ��̼� �޸��� ���� ����
        //_playerController.anim.SetBool("Running", false);
        //_playerController.agent.isStopped = true;
        //_playerController.agent.velocity = Vector3.zero;
    }

    //�̵�����
    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            //�̵���ǥ������ ���� �������� �������̸� ĳ���� �׸� ���� ��ȯ
            _playerController.spriteRender.flipX = hit.point.x < transform.position.x;
            _playerController.agent.isStopped = false;
            //�̵�
            _playerController.agent.SetDestination(hit.point);
            //�޸��� ���� Ȱ��
            _playerController.anim.SetBool("Running", true);
            //���� Ȱ��ȭ
            _playerController.spot.gameObject.SetActive(true);
            _playerController.spot.position = hit.point;
        }
    }
}

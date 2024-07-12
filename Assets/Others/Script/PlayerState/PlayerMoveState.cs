using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static PlayerController;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerMoveState : MonoBehaviour, IState<PlayerController>
{
    private PlayerController _playerController;
    public Vector3 curPosition;
    public Vector3 prevPosition;
    
    public void OperateEnter(PlayerController sender)
    {
        _playerController = sender;
        prevPosition = transform.position;
        Move();
    }

    
    public void OperateUpdate(PlayerController sender)
    {
        curPosition = transform.position;
        if (prevPosition.x < curPosition.x)
        {
            _playerController.isFacingRight = true;
        }
        else if (prevPosition.x == curPosition.x)
        {
            if (_playerController.isFacingRight == true)
            {
                _playerController.spriteRender.flipX = false;
            }
            else
            {
                _playerController.spriteRender.flipX = true;
            }
        }
        else
        {
            _playerController.isFacingRight = false;
        }
        prevPosition = curPosition;

        //_playerController.spriteRender.flipX = _playerController.agent. < transform.position.x;
        //Debug.Log("������");
        if (Input.GetMouseButtonDown(0))
        {
            
            //�޸��Ⱑ �ȳ����� �� Ŭ���� ������ȯ
            _playerController.anim.SetBool("Run", true);
            Move();
        }
        else if (_playerController.agent.remainingDistance < 0.1f)
        {
            //�ִϸ��̼� ����
            _playerController.anim.SetBool("Run", false);

            //�̵���ǥ�� ���� ��Ȱ��ȭ
            _playerController.spot.gameObject.SetActive(false);
        }
    }

    public void OperateExit(PlayerController sender)
    {
        //StopCoroutine(ChangeSpriteX());
        _playerController.anim.SetBool("Run", false);
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
            //_playerController.spriteRender.flipX = hit.point.x < transform.position.x;
            _playerController.agent.isStopped = false;
            //�̵�
            _playerController.agent.SetDestination(hit.point);
            //�޸��� ���� Ȱ��
            _playerController.anim.SetBool("Run", true);
            //���� Ȱ��ȭ
            _playerController.spot.gameObject.SetActive(true);
            _playerController.spot.position = hit.point;
        }
        //Debug.Log(transform.position);
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveState : MonoBehaviour, IState<PlayerController>
{
    private PlayerController _playerController;
    private SpriteRenderer spriteRender;
    private NavMeshAgent agent;
    private Animator anim;
    private GameObject spot;

    private Vector3 curPosition;
    private Vector3 prevPosition;
    
    public void OperateEnter(PlayerController sender)
    {
        _playerController = sender;

        if (!spriteRender) spriteRender = GetComponentInChildren<SpriteRenderer>();
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!anim) anim = GetComponentInChildren<Animator>();
        if (!spot) spot = GameObject.Find("Spot");

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
                spriteRender.flipX = false;
            }
            else
            {
                spriteRender.flipX = true;
            }
        }
        else
        {
            _playerController.isFacingRight = false;
        }
        prevPosition = curPosition;

        if (Input.GetMouseButtonDown(0))
        {
            //�޸��Ⱑ �ȳ����� �� Ŭ���� ������ȯ
            anim.SetBool("Run", true);
            Move();
        }
        else if (agent.remainingDistance < 0.1f)
        {
            //�ִϸ��̼� ����
            anim.SetBool("Run", false);

            //�̵���ǥ�� ���� ��Ȱ��ȭ
            spot.gameObject.SetActive(false);
        }
    }

    public void OperateExit(PlayerController sender)
    {
        anim.SetBool("Run", false);
    }

    //�̵�����
    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            //�̵���ǥ������ ���� �������� �������̸� ĳ���� �׸� ���� ��ȯ
            //_playerController.spriteRender.flipX = hit.point.x < transform.position.x;
            agent.isStopped = false;                            //�ϴ�����  
            agent.SetDestination(hit.point);                    //�̵���ǥ����
            anim.SetBool("Run", true);                          //�޸��� ���� Ȱ��

            spot.gameObject.SetActive(true);  //���� Ȱ��ȭ
            spot.transform.position = hit.point;
        }
        //Debug.Log(transform.position);
    }
}

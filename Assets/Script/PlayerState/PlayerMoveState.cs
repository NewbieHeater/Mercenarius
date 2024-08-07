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
        FlipSprite();

        if (Input.GetMouseButtonDown(0))
        {
            //달리기가 안끝났을 때 클릭시 방향전환
            anim.SetBool("Run", true);
            Move();
        }
        else if (agent.remainingDistance < 0.1f)
        {
            //애니메이션 종료
            anim.SetBool("Run", false);

            //이동목표의 과녁 비활성화
            spot.gameObject.SetActive(false);
        }
    }

    public void OperateExit(PlayerController sender)
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        anim.SetBool("Run", false);
    }

    private void Move()
    {
        Vector3 mousePosition = _playerController.CheckGround(Input.mousePosition);
        agent.isStopped = false;                            //일단정지  
        agent.SetDestination(mousePosition);                    //이동목표설정
        anim.SetBool("Run", true);                          //달리기 상태 활성
        spot.gameObject.SetActive(true);  //과녁 활성화
        spot.transform.position = mousePosition;
    }

    private void FlipSprite()
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
    }
}

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
        anim.SetBool("Run", false);
    }

    //이동로직
    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            //이동목표지점이 현재 지점보다 오른쪽이면 캐릭터 그림 방향 전환
            //_playerController.spriteRender.flipX = hit.point.x < transform.position.x;
            agent.isStopped = false;                            //일단정지  
            agent.SetDestination(hit.point);                    //이동목표설정
            anim.SetBool("Run", true);                          //달리기 상태 활성

            spot.gameObject.SetActive(true);  //과녁 활성화
            spot.transform.position = hit.point;
        }
        //Debug.Log(transform.position);
    }
}

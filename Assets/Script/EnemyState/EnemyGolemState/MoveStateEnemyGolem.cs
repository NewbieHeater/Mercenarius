using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class MoveStateEnemyGolem : MonoBehaviour, IState<EnemyGolemController>
{
    private EnemyGolemController _golemController;
    private NavMeshAgent agent;
    private Animator anim;
    private SpriteRenderer sprite;

    public void OperateEnter(EnemyGolemController sender)
    {
        _golemController = sender;

        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!anim) anim = GetComponentInChildren<Animator>();
        if (!sprite) sprite = GetComponentInChildren<SpriteRenderer>();

        anim.SetBool("Move", true);
        agent.enabled = true; //움직이기
        agent.speed = _golemController.CurSpeed;
    }


    public void OperateUpdate(EnemyGolemController sender)
    {
        if (transform.position != _golemController.target.transform.position)
        {
            agent.SetDestination(_golemController.target.transform.position);
            sprite.flipX = _golemController.target.transform.position.x < transform.position.x;
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    public void OperateExit(EnemyGolemController sender)
    {
        agent.speed = 0;
        anim.SetBool("Move", false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveStateBannerman : MonoBehaviour, IState<EnemyBannermanController>
{
    private EnemyBannermanController _bannermanController;
    private NavMeshAgent agent;
    private Animator anim;
    private SpriteRenderer sprite;
    Vector3 runAwayPoint;
    public void OperateEnter(EnemyBannermanController sender)
    {
        _bannermanController = sender;
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!anim) anim = GetComponentInChildren<Animator>();
        if (!sprite) sprite = GetComponentInChildren<SpriteRenderer>();

        //anim.SetBool("Move", true);
        agent.enabled = true;
        agent.speed = _bannermanController.CurSpeed;
    }

    public void OperateUpdate(EnemyBannermanController sender)
    {
        runAwayPoint = transform.position + (transform.position - _bannermanController.target.transform.position).normalized;
        if (transform.position != _bannermanController.target.transform.position)
        {
            agent.SetDestination(runAwayPoint);
            sprite.flipX = runAwayPoint.x < transform.position.x;
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    public void OperateExit(EnemyBannermanController sender)
    {
        agent.speed = 0;
    }
}

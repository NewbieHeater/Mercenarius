using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RunAwayStateEnemyRanger : MonoBehaviour, IState<EnemyRangerController>
{
    private EnemyRangerController _enemyRangerController;

    private NavMeshAgent agent;
    private Animator anim;
    private SpriteRenderer sprite;
    Vector3 runAwayPoint;
    float curSpeed = 4f;
    public void OperateEnter(EnemyRangerController sender)
    {
        _enemyRangerController = sender;
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!anim) anim = GetComponentInChildren<Animator>();
        if (!sprite) sprite = GetComponentInChildren<SpriteRenderer>();

        //anim.SetBool("Move", true);
        agent.enabled = true;
        agent.speed = curSpeed;
        
    }

    public void OperateUpdate(EnemyRangerController sender)
    {
        runAwayPoint = transform.position + (transform.position - _enemyRangerController.target.transform.position).normalized;
        if (transform.position != _enemyRangerController.target.transform.position)
        {
            agent.SetDestination(runAwayPoint);
            sprite.flipX = runAwayPoint.x < transform.position.x;
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    public void OperateExit(EnemyRangerController sender)
    {
        agent.speed = 0;
        //anim.SetBool("Move", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class MoveStateEnemyGolem : MonoBehaviour, IState<EnemyGolemController>
{
    private EnemyGolemController _golemController;
    private NavMeshAgent nav;
    private Animator anim;
    private Rigidbody enemyRb;
    private SpriteRenderer sprite;

    public void OperateEnter(EnemyGolemController sender)
    {
        _golemController = sender;

        if (!nav) nav = GetComponent<NavMeshAgent>();
        if (!anim) anim = GetComponentInChildren<Animator>();
        if (!enemyRb) enemyRb = GetComponent<Rigidbody>();
        if (!sprite) sprite = GetComponentInChildren<SpriteRenderer>();

        anim.SetBool("Move", true);
        nav.enabled = true; //움직이기
        nav.speed = _golemController.CurSpeed;
    }


    public void OperateUpdate(EnemyGolemController sender)
    {
        if (enemyRb.transform.position != _golemController.target.transform.position)
        {
            nav.SetDestination(_golemController.target.transform.position);
            sprite.flipX = _golemController.target.transform.position.x < enemyRb.position.x;
        }
        else
        {
            nav.SetDestination(transform.position);
        }
    }

    public void OperateExit(EnemyGolemController sender)
    {
        nav.speed = 0;
        anim.SetBool("Move", false);
    }
}
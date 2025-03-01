using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackStateEnemyGolem : MonoBehaviour, IState<EnemyGolemController>
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

        sprite.flipX = _golemController.target.transform.position.x < transform.position.x;

        _golemController.MoveAble = false;
        agent.avoidancePriority = 96;
        StartCoroutine(Fire());
    }
    IEnumerator Fire()
    {
        sprite.flipX = _golemController.target.transform.position.x < transform.position.x;
        _golemController.AttackPoint.LookAt(_golemController.target.transform);

        yield return new WaitForSeconds(_golemController.beforCastDelay);
        anim.SetBool("Attack", true);
        //_monsterController.MoveAble = true;
        yield return new WaitForSeconds(_golemController.attackSpeed);
        StartCoroutine(Fire());
    }
    public void OperateUpdate(EnemyGolemController sender)
    {
        //Debug.Log("근접공격중");
    }

    public void OperateExit(EnemyGolemController sender)
    {
        StopAllCoroutines();
        agent.avoidancePriority = 98;
    }
}
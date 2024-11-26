using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AttackStateEnemyRanger : MonoBehaviour, IState<EnemyRangerController>
{
    private EnemyRangerController _enemyRangerController;
    private NavMeshAgent agent;
    private Animator anim;
    private Transform Rotation;
    private Rigidbody enemyRb;
    private SpriteRenderer sprite;


    float curSpeed;
    public void OperateEnter(EnemyRangerController sender)
    {
        _enemyRangerController = sender;
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!anim) anim = GetComponentInChildren<Animator>();
        if (!sprite) sprite = GetComponentInChildren<SpriteRenderer>();

        sprite.flipX = _enemyRangerController.target.transform.position.x < transform.position.x;

        _enemyRangerController.MoveAble = false;
        agent.avoidancePriority = 96;
        StartCoroutine(Fire());
    }

    public void OperateUpdate(EnemyRangerController sender)
    {
        
    }

    public void OperateExit(EnemyRangerController sender)
    {
        StopAllCoroutines();
        agent.avoidancePriority = 98;
    }
    float beforCastDelay = 3;
    float attackSpeed = 2;
    IEnumerator Fire()
    {
        _enemyRangerController.AttackPoint.LookAt(_enemyRangerController.target.transform);
        yield return new WaitForSeconds(attackSpeed);
        //_monsterController.anim.SetTrigger("Attack");
        GameObject bulletA =
                ObjectPooler.SpawnFromPool("bulletA", new Vector3(_enemyRangerController.AttackPoint.transform.position.x, _enemyRangerController.AttackPoint.transform.position.y, _enemyRangerController.AttackPoint.transform.position.z));

        //Debug.Log("원거리발사");
        StartCoroutine(Fire());
    }
}

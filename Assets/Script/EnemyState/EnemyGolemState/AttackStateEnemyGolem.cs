using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackStateEnemyGolem : MonoBehaviour, IState<EnemyGolemController>
{
    private EnemyGolemController _golemController;
    private NavMeshAgent nav;
    private Animator anim;
    private Transform Rotation;
    private Rigidbody enemyRb;
    private SpriteRenderer sprite;

    public void OperateEnter(EnemyGolemController sender)
    {
        _golemController = sender;
        
        if (!nav) nav = GetComponent<NavMeshAgent>();
        if (!anim) anim = GetComponentInChildren<Animator>();
        if (!enemyRb) enemyRb = GetComponent<Rigidbody>();
        if (!sprite) sprite = GetComponentInChildren<SpriteRenderer>();

        sprite.flipX = _golemController.target.transform.position.x < enemyRb.position.x;

        _golemController.MoveAble = false;
        nav.avoidancePriority = 96;
        //_monsterController.navObs.enabled = true;
        //_monsterController.navObs.carving = true;
        //Debug.Log("근접 공격");
        StartCoroutine(Fire());
    }
    IEnumerator Fire()
    {
        sprite.flipX = _golemController.target.transform.position.x < enemyRb.position.x;
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
        nav.avoidancePriority = 98;
    }
}
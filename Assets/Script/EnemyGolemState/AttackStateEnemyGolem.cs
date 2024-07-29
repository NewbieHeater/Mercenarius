using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateEnemyGolem : MonoBehaviour, IState<EnemyGolemController>
{
    private EnemyGolemController _monsterController;

    public Transform Rotation;
    public void OperateEnter(EnemyGolemController sender)
    {
        _monsterController = sender;
        _monsterController.MoveAble = false;
        _monsterController.nav.avoidancePriority = 96;
        //_monsterController.navObs.enabled = true;
        //_monsterController.navObs.carving = true;
        //Debug.Log("근접 공격");
        StartCoroutine(Fire());
    }
    IEnumerator Fire()
    {

        _monsterController.AttackPoint.LookAt(_monsterController.target.transform);

        yield return new WaitForSeconds(_monsterController.beforCastDelay);
        _monsterController.anim.SetBool("Attack", true);
        //_monsterController.MoveAble = true;
        yield return new WaitForSeconds(_monsterController.attackSpeed);
        StartCoroutine(Fire());
    }
    public void OperateUpdate(EnemyGolemController sender)
    {
        //Debug.Log("근접공격중");
    }

    public void OperateExit(EnemyGolemController sender)
    {
        //_monsterController.navObs.carving = false;
        //_monsterController.navObs.enabled = false;
        StopAllCoroutines();
        
        _monsterController.nav.avoidancePriority = 98;
        //Debug.Log("근접 공격 해제");
    }
}
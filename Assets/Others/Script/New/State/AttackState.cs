using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, IState<MonsterController>
{
    private MonsterController _monsterController;
    
    public Transform Rotation;
    public void OperateEnter(MonsterController sender)
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
        _monsterController.anim.SetTrigger("Attack");
        //_monsterController.MoveAble = true;
        Debug.Log("발사");
        yield return new WaitForSeconds(_monsterController.attackSpeed);
        StartCoroutine(Fire());
    }
    public void OperateUpdate(MonsterController sender)
    {
        //Debug.Log("근접공격중");
    }

    public void OperateExit(MonsterController sender)
    {
        //_monsterController.navObs.carving = false;
        //_monsterController.navObs.enabled = false;
        StopAllCoroutines();
        _monsterController.anim.SetBool("Attack1", true);
        _monsterController.nav.avoidancePriority = 98;
        //Debug.Log("근접 공격 해제");
    }
}
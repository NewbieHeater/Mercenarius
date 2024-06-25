using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : MonoBehaviour, IState<MonsterController>
{
    private MonsterController _monsterController;
    
    public void OperateEnter(MonsterController sender)
    {
        _monsterController = sender;

        
        
        _monsterController.nav.enabled = true; //움직이기
        
        _monsterController.nav.speed = _monsterController.CurSpeed;
        
    }
   

    public void OperateUpdate(MonsterController sender)
    {
        if (_monsterController.enemyRb.transform.position != _monsterController.target.transform.position)
        {
            _monsterController.nav.SetDestination(_monsterController.target.transform.position);
            _monsterController.sprite.flipX = _monsterController.target.position.x < _monsterController.enemyRb.position.x;
        }
        else
        {
            _monsterController.nav.SetDestination(transform.position);
        }

        //_monsterController.sprite.flipX = _monsterController.target.position.x < _monsterController.enemyRb.position.x;

    }

    public void OperateExit(MonsterController sender)
    {
        _monsterController.nav.speed = 0;
        
       // _monsterController.nav.enabled = false;
        
    }
}
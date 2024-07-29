using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadStateEnemyGolem : MonoBehaviour, IState<EnemyGolemController>
{
    private EnemyGolemController _monsterController;

    public void OperateEnter(EnemyGolemController sender)
    {
        _monsterController = sender;
        transform.parent.gameObject.SetActive(false);
    }


    public void OperateUpdate(EnemyGolemController sender)
    {

    }

    public void OperateExit(EnemyGolemController sender)
    {

    }
}

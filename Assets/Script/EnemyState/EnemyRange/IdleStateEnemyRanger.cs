using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleStateEnemyRanger : MonoBehaviour, IState<EnemyRangerController>
{
    private EnemyRangerController _enemyRangerController;

    public void OperateEnter(EnemyRangerController sender)
    {
        _enemyRangerController = sender;
        
    }

    public void OperateUpdate(EnemyRangerController sender)
    {

    }

    public void OperateExit(EnemyRangerController sender)
    {

    }
}

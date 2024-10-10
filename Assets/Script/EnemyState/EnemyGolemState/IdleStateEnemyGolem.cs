using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateEnemyGolem : MonoBehaviour, IState<EnemyGolemController>
{
    private EnemyGolemController _golemController;

    public void OperateEnter(EnemyGolemController sender)
    {
        _golemController = sender;

    }

    public void OperateUpdate(EnemyGolemController sender)
    {
        
    }

    public void OperateExit(EnemyGolemController sender)
    {
        
    }
}

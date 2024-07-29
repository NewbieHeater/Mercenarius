using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateEnemyGolem : MonoBehaviour, IState<EnemyGolemController>
{
    private EnemyGolemController _playerController;

    public void OperateEnter(EnemyGolemController sender)
    {
        _playerController = sender;

    }

    public void OperateUpdate(EnemyGolemController sender)
    {
        
    }

    public void OperateExit(EnemyGolemController sender)
    {
        
    }
}

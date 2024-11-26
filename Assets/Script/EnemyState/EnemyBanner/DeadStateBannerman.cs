using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeadStateBannerman : MonoBehaviour, IState<EnemyBannermanController>
{
    private EnemyBannermanController _bannermanController;

    public void OperateEnter(EnemyBannermanController sender)
    {
        _bannermanController = sender;

        
    }

    public void OperateUpdate(EnemyBannermanController sender)
    {

    }

    public void OperateExit(EnemyBannermanController sender)
    {

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, IState<MonsterController>
{
    private MonsterController _monsterController;

    public void OperateEnter(MonsterController sender)
    {
        _monsterController = sender;
    }

    public void OperateUpdate(MonsterController sender)
    {

    }

    public void OperateExit(MonsterController sender)
    {

    }
}

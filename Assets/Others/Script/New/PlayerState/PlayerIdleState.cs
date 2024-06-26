using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : MonoBehaviour, IState<PlayerController>
{
    private PlayerController _playerController;

    public void OperateEnter(PlayerController sender)
    {
        _playerController = sender;
    }

    public void OperateUpdate(PlayerController sender)
    {

    }

    public void OperateExit(PlayerController sender)
    {

    }
}

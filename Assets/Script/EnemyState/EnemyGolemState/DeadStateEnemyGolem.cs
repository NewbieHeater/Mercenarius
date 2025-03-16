using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadStateEnemyGolem : MonoBehaviour, IState<EnemyGolemController>
{
    private EnemyGolemController _monsterController;

    public void OperateEnter(EnemyGolemController sender)
    {
        Debug.Log("WHY");
        _monsterController = sender;
        InventoryMain.Instance.CurrentCoin += 100;
        this.transform.gameObject.SetActive(false);
    }


    public void OperateUpdate(EnemyGolemController sender)
    {

    }

    public void OperateExit(EnemyGolemController sender)
    {

    }
}

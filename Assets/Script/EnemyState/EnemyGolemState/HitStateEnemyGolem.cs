using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UIElements;

public class HitStateEnemyGolem : MonoBehaviour, IState<EnemyGolemController>
{
    private EnemyGolemController _golemController;
    private NavMeshAgent nav;
    private Animator anim;

    private float time;
    private Vector3 KnockBackPos;

    public void OperateEnter(EnemyGolemController sender)
    {
        _golemController = sender;

        if (!nav) nav = GetComponent<NavMeshAgent>();
        if (!anim) anim = GetComponentInChildren<Animator>();

        //초기화
        time = 0;
        nav.enabled = false;
        anim.SetBool("Hit", true);
        _golemController.curHealth -= 10;

        _golemController.Hpbar.fillAmount = _golemController.curHealth / _golemController.maxHealth;                                      //체력바
        KnockBackPos = transform.position + (-_golemController.target.transform.position + transform.position).normalized * knockbackSpeed;   //넉백위치
    }
    float knockbackSpeed = 0.3f;

    public void OperateUpdate(EnemyGolemController sender)
    {
        transform.position = Vector3.Lerp(transform.position, KnockBackPos, 3 * time);
        time += Time.deltaTime;
    }

    public void OperateExit(EnemyGolemController sender)
    {
        _golemController.MoveAble = true;
        nav.enabled = true;
    }
}

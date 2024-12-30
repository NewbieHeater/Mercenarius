using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : Character
{
    protected override void Start()
    {
        base.Start();
    }
    
    private void Update()
    {
        if(animator.GetBool("Move"))
            FlipSprite();
        //��ǥ���� ���޽� Move�ִϸ��̼� ����
        if (agent.enabled)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                animator.SetBool("Move", false);
            }
        }

        if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("Dash")))
        {
            Dash();
        }
        else if(hit && !animator.GetBool("Dash"))
        {
            Hit();
        }
        else if ((Input.GetMouseButton(0) || Input.GetMouseButtonDown(0)) && !animator.GetBool("Attack") && !animator.GetBool("Dash"))
        {
            Debug.Log("wkr");
            Move();
        }
        else if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("BasicAttack")) && !animator.GetBool("Dash"))
        {
            Attack();
        }
    }

    #region ����
    public override void Attack()
    {
        FlipSpriteByMousePosition();
        agent.SetDestination(transform.position);
        SetAttackDirection();
        animator.SetTrigger("isAttack");
        animator.SetBool("Attack", true);
    }
    #endregion
    
}
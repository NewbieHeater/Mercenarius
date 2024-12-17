using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : Character
{
    
    public TrailRenderer trailRenderer;
    protected override void Start()
    {
        base.Start();
    }
    
    private void Update()
    {
        FlipSprite();
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
        else if (Input.GetMouseButtonDown(0) && !animator.GetBool("Attack") && !animator.GetBool("Dash"))
        {
            Move();
        }
        else if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("BasicAttack")) && !animator.GetBool("Dash"))
        {
            Attack();
        }
        
    }

    #region АјАн
    public override void Attack()
    {
        if(MousePosition().x < transform.position.x)
        {
            spriteRender.flipX = true;
        }
        else
        {
            spriteRender.flipX = false;
        }
        agent.SetDestination(transform.position);
        SetAttackDirection();
        animator.SetTrigger("isAttack");
        animator.SetBool("Attack", true);
    }
    #endregion
    
}
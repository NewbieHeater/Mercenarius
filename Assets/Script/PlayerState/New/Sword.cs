using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Character
{
    
    public TrailRenderer trailRenderer;
    protected override void Start()
    {
        hp = 50;
        attackDamage = 10;
        speed = 3;
        range = 10;
        curDashSpeed = 5;
        curDashPower = 5;
        base.Start();
    }

    private void Update()
    {
        FlipSprite();
        if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("BasicAttack")))
        {
            Attack();
        }
        else if (Input.GetMouseButtonDown(0) && !animator.GetBool("Attack") && !animator.GetBool("Dash"))
        {
            Move();
        }
        else if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("Dash")))
        {
            Dash();
        }
    }

    #region АјАн
    public override void Attack()
    {
        agent.SetDestination(transform.position);
        SetAttackDirection();
        animator.SetTrigger("isAttack");
        animator.SetBool("Attack", true);
    }
    
    #endregion
}
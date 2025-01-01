using UnityEngine;

public class AttackState : IState<Character>
{
    public void OperateEnter(Character sender)
    {
        if (sender.agent != null)
        {
            sender.agent.isStopped = true;
        }
        sender.animator.SetTrigger("isAttack");
        sender.animator.SetBool("Attack", true);
        sender.SetAttackDirection();
        sender.agent.SetDestination(sender.transform.position);
    }

    public void OperateExit(Character sender)
    {
        
    }

    public void OperateUpdate(Character sender)
    {

    }
}
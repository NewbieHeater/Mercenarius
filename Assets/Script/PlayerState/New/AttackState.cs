using UnityEngine;

public class AttackState : IState<Character>
{
    public void OperateEnter(Character sender)
    {
        
    }

    public void OperateExit(Character sender)
    {
        sender.animator.SetBool("Attack", false);
    }

    public void OperateUpdate(Character sender)
    {
        if (Managers.KeyInput.GetKeyDown("BasicAttack"))
        {
            sender.animator.SetTrigger("isAttack");
        }

        if (Managers.KeyInput.GetKeyDown("Dash"))
        {
            sender.sm.SetState(sender.dicState["Dash"]);
        }
        else if (sender.animator.GetBool("Attack") == true)
            return;
        else
            sender.sm.SetState(sender.dicState["Idle"]);
    }
}
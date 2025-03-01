using UnityEngine;

public class MoveState : IState<Character>
{
    public void OperateEnter(Character sender)
    {
        if (sender.agent != null)
        {
            sender.agent.isStopped = false;
        }
        sender.animator.SetBool("Move", true);
        sender.agent.SetDestination(sender.MousePosition());
    }

    public void OperateExit(Character sender)
    {
        sender.animator.SetBool("Move", false);
    }

    public void OperateUpdate(Character sender)
    {
        //Managers.Input.MouseAction = (mouseEvent) =>
        //{

        //};
        
        if(Input.GetMouseButton(0))
            sender.agent.SetDestination(sender.MousePosition());
        sender.FlipSprite();
        if (Managers.KeyInput.GetKeyDown("BasicAttack"))
        {
            sender.sm.SetState(sender.dicState["Attack"]);
        }
        else if (Managers.KeyInput.GetKeyDown("Dash"))
        {
            sender.sm.SetState(sender.dicState["Dash"]);
        }
        else if(sender.agent.remainingDistance < 0.1f)
        {
            sender.sm.SetState(sender.dicState["Idle"]);
        }
    }
}
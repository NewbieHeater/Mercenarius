using UnityEngine;
using UnityEngine.TextCore.Text;

public class IdleState : IState<Character>
{
    private Character character;
    public void OperateEnter(Character sender)
    {
        character = sender;
    }

    public void OperateExit(Character sender)
    {

    }

    public void OperateUpdate(Character sender)
    {
        if (KeyManager.Instance.GetKeyDown("BasicAttack"))
        {
            sender.sm.SetState(sender.dicState["Attack"]);
        }
        else if (KeyManager.Instance.GetKeyDown("Dash"))
        {
            sender.sm.SetState(sender.dicState["Dash"]);
        }
        else if(Input.GetMouseButton(0) && sender.CheckMousePosition())
        {
            sender.sm.SetState(sender.dicState["Move"]);
        }
        //else
            //sender.sm.SetState(sender.dicState["Idle"]);
    }
}
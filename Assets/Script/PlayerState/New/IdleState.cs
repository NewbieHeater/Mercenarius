using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class IdleState : IState<Character>
{
    private Character character;
    public void OperateEnter(Character sender)
    {
        character = sender;
        character.animator.CrossFade("Idle", 0, -1, 0f);
    }

    public void OperateExit(Character sender)
    {
        //Managers.Input.BindActionKeyDown("Attack", OnKeyboard);
    }

    public void OperateUpdate(Character sender)
    {
        if (Managers.KeyInput.GetKeyDown("BasicAttack"))
        {
            sender.sm.SetState(sender.dicState["Attack"]);
        }
        else if (Managers.KeyInput.GetKeyDown("SkillQuickSlot2"))//B
        {
            sender.sm.SetState(sender.dicState["SharedSkill"]);
        }
        else if (Managers.KeyInput.GetKeyDown("Dash") && sender.IsMouseOverGround() && character.dashCoolDown == -1)
        {
            sender.sm.SetState(sender.dicState["Dash"]);
        }
        else if(Input.GetMouseButton(0) && sender.IsMouseOverGround() && !EventSystem.current.IsPointerOverGameObject())
        {
            sender.sm.SetState(sender.dicState["Move"]);
        }
        
        //else
        //sender.sm.SetState(sender.dicState["Idle"]);
    }


}
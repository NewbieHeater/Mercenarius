using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackState : IState<Character>
{
    Character character;
    
    public void OperateEnter(Character sender)
    {
        character = sender;
        if (character.agent != null)
        {
            character.agent.isStopped = true;
            //character.agent.enabled = false;
        }
        character.animator.SetBool("Attack", true);
        character.BasicAttack();
    }

    public void OperateExit(Character sender)
    {
        character.animator.SetBool("Attack", false);
        character.ResetCombo();
    }

    public void OperateUpdate(Character sender)
    {
        if (Managers.KeyInput.GetKeyDown("BasicAttack"))
            character.BasicAttack();
        

        if (Managers.KeyInput.GetKeyDown("Dash") && character.IsMouseOverGround())
        {
            character.sm.SetState(character.dicState["Dash"]);
        }
        else if (character.animator.GetBool("Attack") == true)
            return;
        else
            character.sm.SetState(character.dicState["Idle"]);
    }
}
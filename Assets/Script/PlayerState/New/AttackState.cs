using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackState : IState<Character>
{
    Character character;
    private bool allowAttack;
    public void OperateEnter(Character sender)
    {
        character = sender;
        if (character.agent != null)
        {
            character.agent.isStopped = true;
            //character.agent.enabled = false;
        }
        allowAttack = false;
        character.attackComboValue = 0;
        character.animator.Play("Attack" + character.attackComboValue);
        Debug.Log("attackIn");
    }

    public void OperateExit(Character sender)
    {
        Debug.Log("attackOut");
        character.ResetCombo();
    }

    public void OperateUpdate(Character sender)
    {
        if (Managers.KeyInput.GetKeyDown("BasicAttack"))
        {
            if (character.attackCombo)
            {
                character.attackComboValue++;
                allowAttack = true;
            }
                

            Debug.Log("attackValue");
            
            if (character.attackComboValue >= 3)
            {
                character.attackComboValue = 0;
            }
            character.attackCombo = false;
            
        }
        if (character.nextAttack && allowAttack)
        {
            Debug.Log("attack");
            character.animator.Play("Attack" + character.attackComboValue);
            character.nextAttack = false;
            allowAttack = false;
        }
        
        if (Managers.KeyInput.GetKeyDown("Dash") && character.IsMouseOverGround()&& character.dashCoolDown == -1)
        {
            character.sm.SetState(character.dicState["Dash"]);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            allowAttack = false;
        }
    }
}
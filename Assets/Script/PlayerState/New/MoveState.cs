using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEngine.EventSystems;

public class MoveState : IState<Character>
{
    Character character;
    public void OperateEnter(Character sender)
    {
        character = sender;
        if (character.agent != null)
        {
            character.agent.isStopped = false;
        }
        character.animator.SetBool("Move", true);
        if (character.TryGetGroundPosition(out Vector3 groundPos))
            character.agent.SetDestination(groundPos);
        else
            character.agent.SetDestination(character.transform.position);
    }

    public void OperateExit(Character sender)
    {
        character.animator.SetBool("Move", false);
    }

    public void OperateUpdate(Character sender)
    {
        if(NavMesh.SamplePosition(character.transform.position, out NavMeshHit hit, 0.1f, NavMesh.AllAreas))
        {
            int areaMask = hit.mask;
            if (areaMask == 1)
            {
                character.agent.speed = character.statData.curMovementSpeed;
            }
            else
            {
                character.agent.speed = character.statData.curMovementSpeed * 0.6f;
            }
        }


        
        if (Managers.KeyInput.GetKeyDown("BasicAttack"))
        {
            character.sm.SetState(character.dicState["Attack"]);
        }
        else if (Managers.KeyInput.GetKeyDown("SkillQuickSlot2"))//B
        {
            character.sm.SetState(character.dicState["SharedSkill"]);
        }
        else if (Managers.KeyInput.GetKeyDown("Dash"))
        {
            character.sm.SetState(character.dicState["Dash"]);
        }
        else if(character.agent.remainingDistance < 0.1f)
        {
            character.sm.SetState(character.dicState["Idle"]);
        }

        if (Input.GetMouseButton(0) && character.TryGetGroundPosition(out Vector3 destination) && !EventSystem.current.IsPointerOverGameObject())
        {
            character.agent.SetDestination(destination);
            character.FlipSprite();
        }
            
    }
}
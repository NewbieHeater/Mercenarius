using UnityEngine;
using UnityEngine.TextCore.Text;

public class SharedSkillState : IState<Character>
{
    Character character;
    private float duration = 0.5f; // ��ų ���� �ð�
    private float timer = 0f;

    public void OperateEnter(Character sender)
    {
        character = sender;
        if (character.agent != null)
        {
            character.agent.isStopped = true;
            //character.agent.enabled = false;
        }
        timer = 0f;
        // ��ų �ִϸ��̼� Ʈ���� �� �ʱ�ȭ
        //character.animator.SetTrigger("SharedSkill");
        character.SharedSkill();
        Debug.Log("SharedSkillState ����");
    }

    public void OperateUpdate(Character sender)
    {
        timer += Time.deltaTime;
        // ��ų ���� ���߿� ���� ������ ���� ���� ����.
        // ��: �ݺ� ����Ʈ ó�� ��...

        if (timer >= duration)
        {
            // ��ų �Ϸ� �� Idle ���·� ����
            character.sm.SetState(character.dicState["Idle"]);
        }
    }

    public void OperateExit(Character sender)
    {
        Debug.Log("SharedSkillState ����");
        // ���� �� ��ó��
    }
}

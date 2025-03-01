using UnityEngine;
using UnityEngine.TextCore.Text;

public class SharedSkillState : IState<Character>
{
    Character character;
    private float duration = 2f; // 스킬 지속 시간
    private float timer = 0f;

    public void OperateEnter(Character sender)
    {
        character = sender;
        timer = 0f;
        // 스킬 애니메이션 트리거 등 초기화
        //character.animator.SetTrigger("SharedSkill");
        character.SharedSkill();
        Debug.Log("SharedSkillState 진입");
    }

    public void OperateUpdate(Character sender)
    {
        timer += Time.deltaTime;
        // 스킬 실행 도중에 실행 로직을 넣을 수도 있음.
        // 예: 반복 이펙트 처리 등...

        if (timer >= duration)
        {
            // 스킬 완료 후 Idle 상태로 복귀
            character.sm.SetState(character.dicState["Idle"]);
        }
    }

    public void OperateExit(Character sender)
    {
        Debug.Log("SharedSkillState 종료");
        // 종료 시 후처리
    }
}

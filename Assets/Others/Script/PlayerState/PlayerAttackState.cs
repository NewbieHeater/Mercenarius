using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : MonoBehaviour, IState<PlayerController>
{
    private PlayerController _playerController;

    public void OperateEnter(PlayerController sender)
    {
        _playerController = sender;
        _playerController.anim.SetTrigger("Attacking");
        // 레이를 쏘아 목표 방향을 설정
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // 플레이어 오브젝트의 회전 각도 계산
            
            Vector3 LookRotation = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Quaternion lookTarget = Quaternion.LookRotation(LookRotation - transform.position);
            // 플레이어 위치의 오른쪽으로 farDistance 거리만큼 떨어진 위치 계산
            Vector3 sidePos1 = transform.position;
            // 오른쪽의 프리팹을 생성하고 이동시키는 코루틴 실행
            //StartCoroutine(MovePrefab(prefab, sidePos1, sidePos1 + (hit.point - transform.position).normalized * SOSkill.SkillDistance));

            // movementDuration초 후에 실행되는 함수 호출
            //StartCoroutine(DelayedPrefabCreation(hit, transform.position));
            GameObject instance = Instantiate(_playerController.BasicAttackPrefab[0], sidePos1, lookTarget);
        }

    }

    public void OperateUpdate(PlayerController sender)
    {

    }

    public void OperateExit(PlayerController sender)
    {

    }
}

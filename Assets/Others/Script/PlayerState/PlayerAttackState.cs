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
        // ���̸� ��� ��ǥ ������ ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // �÷��̾� ������Ʈ�� ȸ�� ���� ���
            
            Vector3 LookRotation = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Quaternion lookTarget = Quaternion.LookRotation(LookRotation - transform.position);
            // �÷��̾� ��ġ�� ���������� farDistance �Ÿ���ŭ ������ ��ġ ���
            Vector3 sidePos1 = transform.position;
            // �������� �������� �����ϰ� �̵���Ű�� �ڷ�ƾ ����
            //StartCoroutine(MovePrefab(prefab, sidePos1, sidePos1 + (hit.point - transform.position).normalized * SOSkill.SkillDistance));

            // movementDuration�� �Ŀ� ����Ǵ� �Լ� ȣ��
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

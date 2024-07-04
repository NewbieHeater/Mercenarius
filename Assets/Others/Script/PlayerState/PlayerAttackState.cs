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
        StartCoroutine(StartAttack());
        // ���̸� ��� ��ǥ ������ ����


    }
    IEnumerator StartAttack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            // �÷��̾� ������Ʈ�� ȸ�� ���� ���
            //, Mathf.Infinity
            Vector3 LookRotation = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Quaternion lookTarget = Quaternion.LookRotation(LookRotation - transform.position);
            // �÷��̾� ��ġ�� ���������� farDistance �Ÿ���ŭ ������ ��ġ ���
            Vector3 sidePos1 = transform.position;
            _playerController.spriteRender.flipX = hit.point.x < transform.position.x;
            _playerController.weaponHitBox.transform.LookAt(LookRotation);
            _playerController.weaponHitBox.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _playerController.weaponHitBox.SetActive(false);
            yield return null;
        }
    }

    public void OperateUpdate(PlayerController sender)
    {
        //_playerController.anim
    }

    public void OperateExit(PlayerController sender)
    {

    }
}

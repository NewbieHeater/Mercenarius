using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttackState : MonoBehaviour, IState<PlayerController>
{
    private PlayerController _playerController;
    private NavMeshAgent agent;
    private Animator anim;
    
    public void OperateEnter(PlayerController sender)
    {
        _playerController = sender;

        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!anim) anim = GetComponentInChildren<Animator>();

        _playerController.isAttack = true;
        anim.SetBool("Attack", true);

    }

    public void OperateUpdate(PlayerController sender)
    {
        if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("BasicAttack")))
        {
            StartAttack();
            anim.SetTrigger("isAttack");
        }
    }

    public void OperateExit(PlayerController sender)
    {
        anim.SetBool("Attack", false);
    }

    public void StartAttack()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, 1 << LayerMask.NameToLayer("Ground")))
        {
            // �÷��̾� ������Ʈ�� ȸ�� ���� ���
            Vector3 LookRotation = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            //Quaternion lookTarget = Quaternion.LookRotation(LookRotation - transform.position);
            _playerController.weaponHitBox.transform.LookAt(LookRotation);
            if (hit.point.x < transform.position.x)
            {
                _playerController.isFacingRight = false;
            }
            else
            {
                _playerController.isFacingRight = true;
            }

        }
    }
}

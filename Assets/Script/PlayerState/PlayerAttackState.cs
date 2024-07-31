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
        _playerController.isAttack = true;
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!anim) anim = GetComponentInChildren<Animator>();
        anim.SetBool("Attack", true);

        StartAttack();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        //_playerController.comboCount = 0;
        _playerController.comboCount++;
        _playerController.CheckAttackReInput(1.5f);
        StartAttack();

        anim.SetInteger("AttackCombo", _playerController.comboCount);
    }
    public void StartAttack()
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
            if(hit.point.x < transform.position.x)
            {
                _playerController.isFacingRight = false;
            }
            else
            {
                _playerController.isFacingRight = true;
            }
            _playerController.weaponHitBox.transform.LookAt(LookRotation);
        }
    }

    public void OperateUpdate(PlayerController sender)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
    }

    public void OperateExit(PlayerController sender)
    {
        anim.SetBool("Attack", false);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseMove : MonoBehaviour
{
    public SOSkill Soskill;
    private float orginSpeed = 8;//���߿� �÷��̾ �������� ��Ȳ ����ؼ� ���� �ӵ��� ����ӵ� ����
    public float curSpeed;
    public float dashPower = 5;
    NavMeshAgent agent;     //�׺�Ž�
    Animator anim;
    public Transform spot;  //���콺 Ŭ���� Ŭ�� ��ġ ǥ�ø� ���� �����簡������
    [SerializeField]
    private Rigidbody rg = null;
    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.speed = orginSpeed;
        curSpeed = orginSpeed;
        agent.updateRotation = false;   //ȸ������
    }
    //���߿� fsm���� ��ü
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.isStopped = false;
                agent.SetDestination(hit.point);    //�̵�
                
                spot.gameObject.SetActive(true);    //���� Ȱ��ȭ
                spot.position = hit.point;
            }
        }
        
        else if (agent.remainingDistance < 0.1f)          //��ǥ���� ��ó���� ���� ��Ȱ��ȭ
        {
            //agent.isStopped = true;
            //agent.velocity = Vector3.zero;
            //������ �ִϸ��̼� ������ ��
            spot.gameObject.SetActive(false);
        }

    }
    public void ActivateSkill(SOSkill skill)
    {
        anim.Play(skill.animationName);
        print(string.Format("������ ��ų {0} �� {1} �� ���ظ� �־����ϴ�.", skill.name, skill.damage));
    }
}

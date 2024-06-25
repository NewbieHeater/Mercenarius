using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseMove : MonoBehaviour
{
    public SOSkill Soskill;
    private float orginSpeed = 8;//나중에 플레이어가 느려지는 상황 대비해서 원래 속도와 현재속도 구별
    public float curSpeed;
    public float dashPower = 5;
    NavMeshAgent agent;     //네비매쉬
    Animator anim;
    public Transform spot;  //마우스 클릭시 클릭 위치 표시를 위해 과녁모양가져오기
    [SerializeField]
    private Rigidbody rg = null;
    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.speed = orginSpeed;
        curSpeed = orginSpeed;
        agent.updateRotation = false;   //회적막기
    }
    //나중에 fsm으로 교체
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.isStopped = false;
                agent.SetDestination(hit.point);    //이동
                
                spot.gameObject.SetActive(true);    //과녁 활성화
                spot.position = hit.point;
            }
        }
        
        else if (agent.remainingDistance < 0.1f)          //목표지점 근처가면 과녁 비활성화
        {
            //agent.isStopped = true;
            //agent.velocity = Vector3.zero;
            //정지시 애니메이션 넣으면 됨
            spot.gameObject.SetActive(false);
        }

    }
    public void ActivateSkill(SOSkill skill)
    {
        anim.Play(skill.animationName);
        print(string.Format("적에게 스킬 {0} 로 {1} 의 피해를 주었습니다.", skill.name, skill.damage));
    }
}

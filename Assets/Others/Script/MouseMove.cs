using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MouseMove : MonoBehaviour
{
    public SOSkill Soskill;
    private float coolDownDash=0;
    private float orginSpeed = 5;//나중에 플레이어가 느려지는 상황 대비해서 원래 속도와 현재속도 구별
    public float curSpeed;
    private float dashPowerOrigin = 5f;
    public float dashPower = 5f;

    RaycastHit DashHit;
    NavMeshAgent agent;     //네비매쉬
    Animator anim;
    public Transform spot;  //마우스 클릭시 클릭 위치 표시를 위해 과녁모양가져오기
    [SerializeField]
    private Rigidbody rg = null;

    // 스킬 이미지
    public Image imgIcon;
    // Cooldown 이미지
    public Image imgCool;
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
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (imgCool.fillAmount > 0)
                return;
            StartCoroutine(CoolDown(Soskill.Cooltime));
            
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //마우스 클릭 위치
                Vector3 dashDestPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                //마우스 클릭 위치 - 현재 위치 (각도계산)
                Vector3 dashDestDir = (dashDestPos - transform.position).normalized;

                if (Physics.Raycast(transform.position, dashDestDir, out DashHit, dashPower))
                    if (DashHit.collider.tag.Equals("Wall"))
                        dashPower = DashHit.distance - 0.7f;
                        
                //최종목표 위치
                Vector3 dashDest = transform.position + dashDestDir * dashPower;
                Vector3 curPosition = transform.position;
                StartCoroutine(dash(dashDest, curPosition));

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
    IEnumerator dash(Vector3 Dest, Vector3 pos)
    {
        float t = 0;
        while (t < 1f)
        {
            
            transform.position = Vector3.Lerp(pos, Dest, t*6);
            t += Time.deltaTime;
            Debug.Log(t);
            yield return null;
        }
        dashPower = dashPowerOrigin;
    }
    IEnumerator CoolDown(float cool) 
    {
        float tick = 1f / cool;
        float t = 0;

        imgCool.fillAmount = 1;

        // 10초에 걸쳐 1 -> 0 으로 변경하는 값을
        // imgCool.fillAmout 에 넣어주는 코드
        while (imgCool.fillAmount > 0)
        {
            imgCool.fillAmount = Mathf.Lerp(1, 0, t);
            t += (Time.deltaTime * tick);

            yield return null;
        }
    }

}

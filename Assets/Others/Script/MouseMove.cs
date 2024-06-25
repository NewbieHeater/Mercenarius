using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MouseMove : MonoBehaviour
{
    public SOSkill Soskill;
    private float coolDownDash=0;
    private float orginSpeed = 5;//���߿� �÷��̾ �������� ��Ȳ ����ؼ� ���� �ӵ��� ����ӵ� ����
    public float curSpeed;
    private float dashPowerOrigin = 5f;
    public float dashPower = 5f;

    RaycastHit DashHit;
    NavMeshAgent agent;     //�׺�Ž�
    Animator anim;
    public Transform spot;  //���콺 Ŭ���� Ŭ�� ��ġ ǥ�ø� ���� �����簡������
    [SerializeField]
    private Rigidbody rg = null;

    // ��ų �̹���
    public Image imgIcon;
    // Cooldown �̹���
    public Image imgCool;
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
                //���콺 Ŭ�� ��ġ
                Vector3 dashDestPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                //���콺 Ŭ�� ��ġ - ���� ��ġ (�������)
                Vector3 dashDestDir = (dashDestPos - transform.position).normalized;

                if (Physics.Raycast(transform.position, dashDestDir, out DashHit, dashPower))
                    if (DashHit.collider.tag.Equals("Wall"))
                        dashPower = DashHit.distance - 0.7f;
                        
                //������ǥ ��ġ
                Vector3 dashDest = transform.position + dashDestDir * dashPower;
                Vector3 curPosition = transform.position;
                StartCoroutine(dash(dashDest, curPosition));

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

        // 10�ʿ� ���� 1 -> 0 ���� �����ϴ� ����
        // imgCool.fillAmout �� �־��ִ� �ڵ�
        while (imgCool.fillAmount > 0)
        {
            imgCool.fillAmount = Mathf.Lerp(1, 0, t);
            t += (Time.deltaTime * tick);

            yield return null;
        }
    }

}

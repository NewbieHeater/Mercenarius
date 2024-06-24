using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : EnemyState
{
    /*
    public float speed;//�� �̵��ӵ�
    public float originalSpeed;
    
    public RuntimeAnimatorController[] animCon;

    public Animator anim;
    public NavMeshAgent nav;
    [SerializeField]
    private SpriteRenderer spriter;//�� ������(�� �ڽ�)�� ��������Ʈ

    void Awake()
    {
        spriter = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        nav = GetComponentInChildren<NavMeshAgent>();
        nav.updateRotation = false;
        enemyRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        while (isLive)
        {
            if (isHunting)
            {
                nav.speed = originalSpeed;
                nav.enabled = true;
                if (nav.destination != target.transform.position)
                {
                    nav.SetDestination(target.transform.position);
                }
                else
                {
                    nav.SetDestination(transform.position);
                }
            }
            else if (!isHunting && isHit)
            {
                nav.enabled = false;
            }
            else if (!isHunting && isAttacking)
            {
                //nav.enabled = false;
                nav.speed = 0;
            }
            else
            {
                nav.enabled = false;
            }
            yield return new WaitForSeconds(0.25f);
        }
        
    }


    void LateUpdate()
    {
        if (!isLive)
            return;
        spriter.flipX = target.position.x < enemyRb.position.x;
    }

    

    public void Init(SpawnData data)
    {
        
        anim.runtimeAnimatorController = animCon[data.spriteType];
        originalSpeed = data.OriginalSpeed;
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);  // �� ��ü�� �ѹ���
        CancelInvoke();    // Monobehaviour�� Invoke�� �ִٸ�
    }
*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BackUp : MonoBehaviour
{/*
    public float speed;//�� �̵��ӵ�
    public float originalSpeed;
    public float health;
    public float maxHealth;
    NavMeshAgent nav;
    public RuntimeAnimatorController[] animCon;
    [SerializeField]
    private bool isHunting = false, isAttacking = false, isHit = false;//�ִϸ��̼� ������ ���� ���氡��
    bool isLive;//�׾����� ��Ҵ��� �Ǻ� ���� ����Ƽ �̺�Ʈ�� �ٲ� ����
    Rigidbody enemyRb;//�� �������� ������ٵ�
    public Animator anim;
    [SerializeField]
    private Rigidbody target;//�÷��̾� ĳ������ ������ �ٵ�
    [SerializeField]
    private SpriteRenderer spriter;//�� ������(�� �ڽ�)�� ��������Ʈ

    void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        spriter = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        nav = GetComponentInChildren<NavMeshAgent>();
        nav.updateRotation = false;
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;
        float dist = Vector3.Distance(target.transform.position, transform.position);

        if (nav.destination != target.transform.position)
        {
            nav.SetDestination(target.transform.position);
        }
        else
        {
            nav.SetDestination(transform.position);
        }

        if (!isHit)
        {
            //transform.localScale = new Vector3(directionX, transform.localScale.y, directionZ);            
            if (dist >= 1.5f)
            {
                isHunting = true;
                nav.enabled = true;
            }
            else if (dist < 1.5f)
            {
                //isAttacking = true; //�̶� ���ݸ���� ������ ��
                isHunting = false;
                nav.enabled = false;
            }
            if (isHunting)
            {
                nav.speed = originalSpeed;
            }
        }

        else if (isHit)
        {
            nav.speed = 0f;
            nav.enabled = false;
        }
        else if (isAttacking)
        {

        }
    }

    void LateUpdate()
    {
        if (!isLive)
            return;
        spriter.flipX = target.position.x < enemyRb.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {

        anim.runtimeAnimatorController = animCon[data.spriteType];
        originalSpeed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);  // �� ��ü�� �ѹ���
        CancelInvoke();    // Monobehaviour�� Invoke�� �ִٸ�
    }
    */
}

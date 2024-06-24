using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BackUp : MonoBehaviour
{/*
    public float speed;//적 이동속도
    public float originalSpeed;
    public float health;
    public float maxHealth;
    NavMeshAgent nav;
    public RuntimeAnimatorController[] animCon;
    [SerializeField]
    private bool isHunting = false, isAttacking = false, isHit = false;//애니매이션 설정용 추후 변경가능
    bool isLive;//죽었는지 살았는지 판별 추후 유니티 이벤트로 바꿀 예정
    Rigidbody enemyRb;//적 프리팹의 리지드바디
    public Animator anim;
    [SerializeField]
    private Rigidbody target;//플레이어 캐릭터의 리지드 바디
    [SerializeField]
    private SpriteRenderer spriter;//적 프리팹(의 자식)의 스프라이트

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
                //isAttacking = true; //이때 공격모션을 넣으면 됨
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
        ObjectPooler.ReturnToPool(gameObject);  // 한 객체에 한번만
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }
    */
}

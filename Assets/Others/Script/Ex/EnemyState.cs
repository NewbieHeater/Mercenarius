using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyState : MonoBehaviour
{
    [SerializeField]
    protected bool isHunting = true, isAttacking = false, isHit = false;//애니매이션 설정용 추후 변경가능
    protected bool isLive;//죽었는지 살았는지 판별 추후 유니티 이벤트로 바꿀 예정
    protected Rigidbody enemyRb;//적 프리팹의 리지드바디

    [SerializeField]
    public Rigidbody target;//플레이어 캐릭터의 리지드 바디


    void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLive)
            return;
        float dist = Vector3.Distance(target.transform.position, transform.position);
        Debug.Log(dist);
        if (!isHit)
        {
            if (dist >= 3f)
            {
                isHunting = true;
                isAttacking = false;
            }
            else if (dist < 3f)
            {
                isAttacking = true; //이때 공격모션을 넣으면 됨
                isHunting = false;
            }
        }
        else if (isHit)
        {
            isHunting = false;
            isAttacking = false;
        }
    }
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody>();
        isLive = true;
    }
    public void OnDead()
    {
        gameObject.SetActive(false);
    }

    
}

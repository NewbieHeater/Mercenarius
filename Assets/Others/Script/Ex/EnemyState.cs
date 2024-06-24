using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyState : MonoBehaviour
{
    [SerializeField]
    protected bool isHunting = true, isAttacking = false, isHit = false;//�ִϸ��̼� ������ ���� ���氡��
    protected bool isLive;//�׾����� ��Ҵ��� �Ǻ� ���� ����Ƽ �̺�Ʈ�� �ٲ� ����
    protected Rigidbody enemyRb;//�� �������� ������ٵ�

    [SerializeField]
    public Rigidbody target;//�÷��̾� ĳ������ ������ �ٵ�


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
                isAttacking = true; //�̶� ���ݸ���� ������ ��
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

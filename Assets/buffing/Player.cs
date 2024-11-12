using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IUnit
{
    private BuffManager buffManager;
    private StatDatas statDatas;

    private void Awake()
    {
        buffManager = GetComponent<BuffManager>();
        statDatas = GetComponent<StatDatas>();  
    }
    
    public void Attack()
    {
        
        // ���� ����
        Debug.Log("Player is attacking!");
    }

    public void Move()
    {
        // �̵� ����
        Debug.Log("Player is moving!");
    }

    public void Idle()
    {
        // ��� ����
        Debug.Log("Player is idle!");
    }

    private void Update()
    {
        Debug.Log(statDatas.AttackPower);
        Debug.Log(statDatas.Defense);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // ���÷� ���ݷ� ���� ����
            IBuff attackBuff = new AttackPowerBuff(10f, 20f); // 10�ʰ� ���ݷ� 20 ����
            buffManager.AddBuff(attackBuff, gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // ���÷� �ż� ���� ����
            IBuff holyBuff = new HolyBuff(15f); // 15�ʰ� �ż� ����
            buffManager.AddBuff(holyBuff, gameObject);
        }
    }
}

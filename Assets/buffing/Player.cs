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
        
        // 공격 로직
        Debug.Log("Player is attacking!");
    }

    public void Move()
    {
        // 이동 로직
        Debug.Log("Player is moving!");
    }

    public void Idle()
    {
        // 대기 상태
        Debug.Log("Player is idle!");
    }

    private void Update()
    {
        Debug.Log(statDatas.AttackPower);
        Debug.Log(statDatas.Defense);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 예시로 공격력 버프 적용
            IBuff attackBuff = new AttackPowerBuff(10f, 20f); // 10초간 공격력 20 증가
            buffManager.AddBuff(attackBuff, gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 예시로 신성 버프 적용
            IBuff holyBuff = new HolyBuff(15f); // 15초간 신성 버프
            buffManager.AddBuff(holyBuff, gameObject);
        }
    }
}

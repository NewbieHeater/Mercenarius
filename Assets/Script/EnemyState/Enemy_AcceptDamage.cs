using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AcceptDamage : MonoBehaviour
{
    public EnemyGolemController gollem;
    // Start is called before the first frame update
    void Start()
    {
        gollem = GetComponent<EnemyGolemController>();
    }


    public void Accept_Damage_Oneshot(float damage) // 무적시간과 피격거리조정등 미세조정 들어갈 것임.
    {
        gollem.stat.ModifyCurrentHp(damage);
        Check_HP_Zero();
    }
    public void Accept_Damage_Bombshot(float damage)
    {
        gollem.stat.ModifyCurrentHp(damage);
        Check_HP_Zero();
    }
    public void Accept_Damage_Rapidshot(float damage)
    {
        gollem.stat.ModifyCurrentHp(damage);
        Check_HP_Zero();
    }

    private void Check_HP_Zero()
    {
        if (gollem.stat.curHp <= 0)
        {
            // 사망이벤트
            //Destroy(this.gameObject);

        }
    }
}
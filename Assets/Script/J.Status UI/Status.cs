using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public Text HpText; //체력
    public Text GoldText; //골드
    public Text KillText; //킬수
    public Text ManaText; // 마나
    
    private int hp = 100;
    private int gold = 0;
    private int kill = 0;
    private int mana = 100;

    public int enemyDamage = 1; //적 공격 (임시)
    public int SkillMana = 1; //스킬 마나 (임시)

    public static Status instance;

    private void Awake()
    {
        if (Status.instance == null)
        {
            Status.instance = this;
        }
    }
    void Update()
    {
        //텍스트로 표시
        HpText.text = "HP: " + hp.ToString();
        GoldText.text = "Gold: " + gold.ToString();
        KillText.text = "Kill: " + kill.ToString();
        ManaText.text = "Mana: " + mana.ToString();

        //스페이스바 누르면 변화
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Status.instance.TakeDamage(enemyDamage); //체력 감소

            gold += 1; //골드 증가
            kill += 1; //킬 수 증가

            Status.instance.UseMana(SkillMana); //마나 감소
        }
    }
    
    public void TakeDamage(int damage) //적 공격에 따른 체력감소, 음수 방지
    {
        hp -= damage; 
        if (hp < 0)
        {
            hp = 0;
        }
    }

    public void UseMana(int ManaUsage) //스킬에 따른 마나 감소
    {
        mana -= ManaUsage;
        if (mana < 0)
        {
            mana = 0;
            //스킬 사용 방지
        }
    }
}
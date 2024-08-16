using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public Text HpText; //ü��
    public Text GoldText; //���
    public Text KillText; //ų��
    public Text ManaText; // ����
    
    private int hp = 100;
    private int gold = 0;
    private int kill = 0;
    private int mana = 100;

    public int enemyDamage = 1; //�� ���� (�ӽ�)
    public int SkillMana = 1; //��ų ���� (�ӽ�)

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
        //�ؽ�Ʈ�� ǥ��
        HpText.text = "HP: " + hp.ToString();
        GoldText.text = "Gold: " + gold.ToString();
        KillText.text = "Kill: " + kill.ToString();
        ManaText.text = "Mana: " + mana.ToString();

        //�����̽��� ������ ��ȭ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Status.instance.TakeDamage(enemyDamage); //ü�� ����

            gold += 1; //��� ����
            kill += 1; //ų �� ����

            Status.instance.UseMana(SkillMana); //���� ����
        }
    }
    
    public void TakeDamage(int damage) //�� ���ݿ� ���� ü�°���, ���� ����
    {
        hp -= damage; 
        if (hp < 0)
        {
            hp = 0;
        }
    }

    public void UseMana(int ManaUsage) //��ų�� ���� ���� ����
    {
        mana -= ManaUsage;
        if (mana < 0)
        {
            mana = 0;
            //��ų ��� ����
        }
    }
}
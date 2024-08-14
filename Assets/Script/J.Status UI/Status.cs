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
            hp -= 1;
            gold += 1;
            kill += 1;
            mana -= 1;
        }
    }
}
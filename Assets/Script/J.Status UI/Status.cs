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
            hp -= 1;
            gold += 1;
            kill += 1;
            mana -= 1;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInfo : MonoBehaviour 
{
    //전투방은 C01, C02등 C로 시작  Combat
    //상점이나 특수이벤트방은 S01, S02등 S로 시작  Safe
    public int point = 1;
    public string[] type = { "EnemySquare", "EnemyGolem" };
    public int num = 1;

    void Awake()
    {
        
      
    }
    

}

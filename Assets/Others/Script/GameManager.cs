using Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTIme;
    public float maxGameTIme = 2 * 10f;

    public MouseMove player;
    public ObjectPooler pool;
    public SpawnInfo spawnInfo;
    //public EnemyVariables variables;

    void Awake()
    {
        instance = this;
        
    }

    

    void Update()
    {
        gameTIme += Time.deltaTime;
        //타이머 변수에 실제로 시간 넣기
        if (gameTIme > maxGameTIme) 
        {
            gameTIme = maxGameTIme; 
        }
    }
    //그저 다른 스크립트를 참조하기 편하려고 넣은 게임매니저
}

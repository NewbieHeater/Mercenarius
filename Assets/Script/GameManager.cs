using Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public float gameTIme;
    public float maxGameTIme = 2 * 10f;
    public PlayerController player;
    public ObjectPooler pool;
    //public EnemyVariables variables;
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
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

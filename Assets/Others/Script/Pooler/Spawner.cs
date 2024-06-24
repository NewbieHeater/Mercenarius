using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    
    //맵의 스폰 포인트입력
    int RoomCode;//적의 종류를 정함
    float timer; //time.deltatime을 넣기위한 변수
    int typeNum = 0;
    int spawnPointNum;
    

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    public void Spawn()
    {
        //case "EnemySquare":
        Debug.Log("스폰시작");
        
        for (int i = 0; i < spawnData.Length; i++)
        {
            Debug.Log(spawnData.Length);
            for (int j = 0; j < spawnData[typeNum].num; j++)
            {
                GameObject enemy =
                    ObjectPooler.SpawnFromPool(spawnData[typeNum].spriteType, new Vector3(spawnPoint[spawnPointNum].transform.position.x, spawnPoint[spawnPointNum].transform.position.y, spawnPoint[spawnPointNum].transform.position.z));
            }
            typeNum++;
        }
        
    }
}

[System.Serializable]
public class SpawnData
{
    public string spriteType;
    public int type;
    public int num;

}



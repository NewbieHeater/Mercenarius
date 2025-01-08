using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] topRooms;
    public GameObject[] rightRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;
    public GameObject[] widthRooms;
    public GameObject[] lengthRooms;

    public GameObject closedRoom;   // 중복 방지

    public List<GameObject> rooms;

    public float BossWaitTime;
    public float TrialWaitTime;
    public float StoreWaitTime;

    private bool spawnedBoss;
    private bool spawnedTrial;
    private bool spawnedStore;

    public GameObject boss;
    public GameObject trial;
    public GameObject store;

    private int i, j, k;

    private void Update()
    {
        // 상점방 생성
        if (StoreWaitTime <= 0 && spawnedStore == false)
        {
            for (i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Instantiate(store, rooms[i].transform.position, Quaternion.identity);
                    //Destroy(rooms[i]);
                    spawnedStore = true;
                }
            }
            Debug.Log("i");
            Debug.Log(i);
            if (i < 11)
            {
                // 현재 활성화된 씬 이름 가져오기
                string currentSceneName = SceneManager.GetActiveScene().name;

                // 현재 씬 다시 로드
                SceneManager.LoadScene(currentSceneName);
            }
        }
        else
        {
            if (StoreWaitTime >= 0)
            {
                StoreWaitTime -= Time.deltaTime;
            }
        }

        // 시련방 생성
        if (TrialWaitTime <= 0 && spawnedTrial == false)
        {
            for (j = 0; j < rooms.Count; j++)
            {
                if (j == rooms.Count - 1)
                {
                    Instantiate(trial, rooms[j].transform.position, Quaternion.identity);
                    //Destroy(rooms[j]);
                    spawnedTrial = true;
                }
            }
            Debug.Log("j");
            Debug.Log(j);
            if (j <= i)
            {
                // 현재 활성화된 씬 이름 가져오기
                string currentSceneName = SceneManager.GetActiveScene().name;

                // 현재 씬 다시 로드
                SceneManager.LoadScene(currentSceneName);
            }
        }
        else
        {
            if (TrialWaitTime >= 0)
            {
                TrialWaitTime -= Time.deltaTime;
            }
        }

        // 보스방 생성
        if (BossWaitTime <= 0 && spawnedBoss == false)
        {
            for(k = 0; k < rooms.Count;k++)
            {
                if(k == rooms.Count - 1)
                {
                    Instantiate(boss, rooms[k].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                    Destroy(rooms[i-1]);
                    Destroy(rooms[j-1]);
                    Destroy(rooms[k]);
                }
            }
            Debug.Log("k");
            Debug.Log(k);
            if (k <= j)
            {
                // 현재 활성화된 씬 이름 가져오기
                string currentSceneName = SceneManager.GetActiveScene().name;

                // 현재 씬 다시 로드
                SceneManager.LoadScene(currentSceneName);
            }
        }
        else
        {
            if (BossWaitTime >= 0)
            {
                BossWaitTime -= Time.deltaTime;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

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

    private int b, c, d;
    
    private void Update()
    {
        // 상점방 생성
        if (StoreWaitTime <= 0 && spawnedStore == false)
        {
            for (b = 0; b < rooms.Count; b++)
            {
                if (b == rooms.Count - 1)
                {
                    Instantiate(store, rooms[b].transform.position, Quaternion.identity);
                    spawnedStore = true;
                }
            }
            if (b < 11)
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
            for (c = 0; c < rooms.Count; c++)
            {
                if (c == rooms.Count - 1)
                {
                    Instantiate(trial, rooms[c].transform.position, Quaternion.identity);
                    spawnedTrial = true;
                }
            }
            if (c <= b)
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
            for(d = 0; d < rooms.Count;d++)
            {
                if(d == rooms.Count - 1)
                {
                    Instantiate(boss, rooms[d].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                    Destroy(rooms[b - 1]);
                    Destroy(rooms[c - 1]);
                    Destroy(rooms[d]);
                }
            }
            if (d <= c)
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

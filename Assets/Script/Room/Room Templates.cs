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

    public GameObject closedRoom;   // �ߺ� ����

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
        // ������ ����
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
                // ���� Ȱ��ȭ�� �� �̸� ��������
                string currentSceneName = SceneManager.GetActiveScene().name;

                // ���� �� �ٽ� �ε�
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

        // �÷ù� ����
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
                // ���� Ȱ��ȭ�� �� �̸� ��������
                string currentSceneName = SceneManager.GetActiveScene().name;

                // ���� �� �ٽ� �ε�
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

        // ������ ����
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
                // ���� Ȱ��ȭ�� �� �̸� ��������
                string currentSceneName = SceneManager.GetActiveScene().name;

                // ���� �� �ٽ� �ε�
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

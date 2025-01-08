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

    private int i, j, k;

    private void Update()
    {
        // ������ ����
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

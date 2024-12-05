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

    public GameObject[] lengthRooms;
    public GameObject[] widthRooms;

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

    private void Update()
    {
        if(BossWaitTime <= 0 && spawnedBoss == false)
        {
            for(int i = 0; i<rooms.Count;i++)
            {
                if(i==rooms.Count-1)
                {
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    Destroy(rooms[i]);
                    spawnedBoss = true;
                }
            }
        }
        else
        {
            if (BossWaitTime >= 0)
            {
                BossWaitTime -= Time.deltaTime;
            }
        }

        if (TrialWaitTime <= 0 && spawnedTrial == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Instantiate(trial, rooms[i].transform.position, Quaternion.identity);
                    Destroy(rooms[i]);
                    spawnedTrial = true;
                }
            }
            if (rooms.Count < 18)
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

        if (StoreWaitTime <= 0 && spawnedStore == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Instantiate(store, rooms[i].transform.position, Quaternion.identity);
                    Destroy(rooms[i]);
                    spawnedStore = true;
                }
            }
            if (rooms.Count < 17)
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
    }
}

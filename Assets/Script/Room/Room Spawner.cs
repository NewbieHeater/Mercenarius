using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> �Ʒ��� �Ա� �� �ʿ�
    // 2 --> ���� �Ա� �� �ʿ�
    // 3 --> ���� �Ա� �� �ʿ�
    // 4 --> ������ �Ա� �� �ʿ�

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    public float waitTime = 4f;
    private void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn",0.1f);
    }

    private void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)      // �Ʒ��� �Ա� �� �ʿ�
            {
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2) // ���� �Ա� �� �ʿ�
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3) // ���� �Ա� �� �ʿ�
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4) // ������ �Ա� �� �ʿ�
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Road Spawn"))
        {
            if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
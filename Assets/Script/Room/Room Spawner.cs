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
    // 5 ~ 8 --> �� ª�� ���� ����

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
            if (openingDirection == 5)      // �Ʒ��� �Ա� �� �ʿ�
            {
                rand = Random.Range(0, templates.lengthRooms.Length);
                Instantiate(templates.lengthRooms[rand], transform.position, templates.lengthRooms[rand].transform.rotation);
            }
            else if (openingDirection == 6) // ���� �Ա� �� �ʿ�
            {
                rand = Random.Range(0, templates.widthRooms.Length);
                Instantiate(templates.widthRooms[rand], transform.position, templates.widthRooms[rand].transform.rotation);
            }
            else if (openingDirection == 7) // ���� �Ա� �� �ʿ�
            {
                rand = Random.Range(0, templates.lengthRooms.Length);
                Instantiate(templates.lengthRooms[rand], transform.position, templates.lengthRooms[rand].transform.rotation);
            }
            else if (openingDirection == 8) // ������ �Ա� �� �ʿ�
            {
                rand = Random.Range(0, templates.widthRooms.Length);
                Instantiate(templates.widthRooms[rand], transform.position, templates.widthRooms[rand].transform.rotation);
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

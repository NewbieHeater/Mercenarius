using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> 아래쪽 입구 방 필요
    // 2 --> 왼쪽 입구 방 필요
    // 3 --> 위쪽 입구 방 필요
    // 4 --> 오른쪽 입구 방 필요
    // 5 ~ 8 --> 맵 짧게 생성 방지

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
            if (openingDirection == 1)      // 아래쪽 입구 방 필요
            {
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2) // 왼쪽 입구 방 필요
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3) // 위쪽 입구 방 필요
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4) // 오른쪽 입구 방 필요
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            if (openingDirection == 5)      // 아래쪽 입구 방 필요
            {
                rand = Random.Range(0, templates.lengthRooms.Length);
                Instantiate(templates.lengthRooms[rand], transform.position, templates.lengthRooms[rand].transform.rotation);
            }
            else if (openingDirection == 6) // 왼쪽 입구 방 필요
            {
                rand = Random.Range(0, templates.widthRooms.Length);
                Instantiate(templates.widthRooms[rand], transform.position, templates.widthRooms[rand].transform.rotation);
            }
            else if (openingDirection == 7) // 위쪽 입구 방 필요
            {
                rand = Random.Range(0, templates.lengthRooms.Length);
                Instantiate(templates.lengthRooms[rand], transform.position, templates.lengthRooms[rand].transform.rotation);
            }
            else if (openingDirection == 8) // 오른쪽 입구 방 필요
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

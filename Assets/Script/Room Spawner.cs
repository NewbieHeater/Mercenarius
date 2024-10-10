using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> 세로 복도 필요
    // 2 --> 가로 복도 필요
    // 3 --> 세로 복도 필요
    // 4 --> 가로 복도 필요
    // 5 --> 아래쪽 통로 방 필요
    // 6 --> 왼쪽 통로 방 필요
    // 7 --> 위쪽 통로 방 필요
    // 8 --> 오른쪽 통로 방 필요

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;
    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn",0.1f);
    }

    private void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)      // 세로 복도 필요
            {
                rand = Random.Range(0, templates.verticalRoad.Length);
                Instantiate(templates.verticalRoad[rand], transform.position, templates.verticalRoad[rand].transform.rotation);
            }
            else if (openingDirection == 2) // 가로 복도 필요
            {
                rand = Random.Range(0, templates.horizontalRoad.Length);
                Instantiate(templates.horizontalRoad[rand], transform.position, templates.horizontalRoad[rand].transform.rotation);
            }
            else if (openingDirection == 3) // 세로 복도 필요
            {
                rand = Random.Range(0, templates.verticalRoad.Length);
                Instantiate(templates.verticalRoad[rand], transform.position, templates.verticalRoad[rand].transform.rotation);
            }
            else if (openingDirection == 4) // 가로 복도 필요
            {
                rand = Random.Range(0, templates.horizontalRoad.Length);
                Instantiate(templates.horizontalRoad[rand], transform.position, templates.horizontalRoad[rand].transform.rotation);
            }
            else if (openingDirection == 5) // 아래쪽 통로 방 필요
            {
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 6) // 왼쪽 통로 방 필요
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 7) // 위쪽 통로 방 필요
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 8) // 오른쪽 통로 방 필요
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Road Spawn") && other.GetComponent<RoomSpawner>().spawned == true)
        {
            Destroy(gameObject);
        }
    }
}

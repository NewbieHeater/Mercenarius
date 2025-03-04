using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navi : MonoBehaviour
{
    public NavMeshSurface nms;
    public float RoomWaitTime;
    private bool isCheck;

    private void Update()
    {
        if (RoomWaitTime <= 0 && isCheck==false)
        {
            GenerateNavmesh();
            isCheck = true;
        }
        else
        {
            if (RoomWaitTime >= 0)
            {
                RoomWaitTime -= Time.deltaTime;
            }
        }
    }

    private void GenerateNavmesh()
    {
        nms.BuildNavMesh();
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    public NavMeshSurface nms;
    public NavMeshSurface nms_pit;
    public void Init()
    {
    }

    private void Awake()
    {
        GenerateNavmesh();
    }

    private void GenerateNavmesh()
    {
        nms.BuildNavMesh();
        nms_pit.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GenerateNavmesh();
        }
    }
}
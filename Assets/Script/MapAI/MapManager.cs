using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    public NavMeshSurface nms;
    public void Init()
    {
    }

    private void Start()
    {
        GenerateNavmesh();
    }

    private void GenerateNavmesh()
    {
        nms.BuildNavMesh();
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
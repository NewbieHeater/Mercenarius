using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
public class CameraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    void Start()
    {
        camera = GetComponentInChildren<CinemachineVirtualCamera>();
        camera.Follow = CharacterManager.Instance.character.GetComponent<Transform>();
    }
}

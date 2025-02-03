using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSystem : MonoBehaviour
{
    [SerializeField] protected GameObject Settings;
    private void Start()
    {
        Settings.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(Managers.KeyInput.GetKeyCode("Settings")))
        {
            if (Settings.activeInHierarchy)
            {
                GameManager.Instance.isUIOpen = false;
                Settings.SetActive(false);
            }
            else
            {
                GameManager.Instance.isUIOpen = true;
                Settings.SetActive(true);
            }
        }
    }
}
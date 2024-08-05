using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSystem : MonoBehaviour
{
    [SerializeField] protected GameObject Settings;
    public static bool isPause { get; private set; }

    protected void Awake()
    {
        isPause = false;
        if (Settings.activeSelf)
        {
            Settings.SetActive(false);
        }
    }
    protected void Update()
    {
        if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("Settings")))
        {
            if (isPause == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    void Pause()
    {
        Settings.SetActive(true);
        isPause = true;
        Time.timeScale = 0f;
    }
    void Resume()
    {
        Settings.SetActive(false);
        isPause = false;
        Time.timeScale = 1f;
    }
}
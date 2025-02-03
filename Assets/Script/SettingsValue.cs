using System.Collections.Generic;
using UnityEngine;

public class SettingsValue : MonoBehaviour
{
    protected Dictionary<string, int> volumeSettings = new Dictionary<string, int>();

    public void SaveSettings()
    {
        foreach (var setting in volumeSettings)
        {
            PlayerPrefs.SetInt(setting.Key, setting.Value);
        }
        PlayerPrefs.Save();
        Debug.Log("설정 저장 완료");
    }
}

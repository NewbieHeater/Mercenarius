using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ResolutionSetting : SettingsValue, ISettingsSaver
{
    public TMP_Dropdown resolutionDropdown;

    private List<Resolution> resolutions = new List<Resolution>();
    private int optimalResolutionIndex = 0;

    private const string ResolutionPrefKey = "ResolutionIndex";

    private void Start()
    {
        // 해상도 목록 추가
        resolutions.Add(new Resolution { width = 1280, height = 720 });
        resolutions.Add(new Resolution { width = 1280, height = 800 });
        resolutions.Add(new Resolution { width = 1440, height = 900 });
        resolutions.Add(new Resolution { width = 1600, height = 900 });
        resolutions.Add(new Resolution { width = 1680, height = 1050 });
        resolutions.Add(new Resolution { width = 1920, height = 1080 });
        resolutions.Add(new Resolution { width = 1920, height = 1200 });
        resolutions.Add(new Resolution { width = 2048, height = 1280 });
        resolutions.Add(new Resolution { width = 2560, height = 1440 });
        resolutions.Add(new Resolution { width = 2560, height = 1600 });
        resolutions.Add(new Resolution { width = 2880, height = 1800 });
        resolutions.Add(new Resolution { width = 3480, height = 2160 });

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        // 현재 시스템 해상도와 일치하는 옵션에 별표(*) 추가
        for (int i = 0; i < resolutions.Count; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                optimalResolutionIndex = i;
                option += " *";
            }
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);

        // 저장된 해상도 인덱스가 있다면 불러오기 (저장된 값이 유효한지 확인)
        if (PlayerPrefs.HasKey(ResolutionPrefKey))
        {
            int savedIndex = PlayerPrefs.GetInt(ResolutionPrefKey);
            if (savedIndex >= 0 && savedIndex < resolutions.Count)
            {
                optimalResolutionIndex = savedIndex;
            }
        }

        resolutionDropdown.value = optimalResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // 게임 실행 시 선택된(또는 기본) 해상도로 적용
        SetResolution(optimalResolutionIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Count)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            Debug.Log("해상도 설정: " + resolution.width + " x " + resolution.height);
        }
        else
        {
            Debug.LogWarning("잘못된 해상도 인덱스: " + resolutionIndex);
        }
    }

    // ISettingsSaver 인터페이스 구현: 해상도 설정 저장
    public void SaveSettings()
    {
        // 현재 드롭다운에 선택된 해상도 인덱스를 저장
        PlayerPrefs.SetInt(ResolutionPrefKey, resolutionDropdown.value);
        PlayerPrefs.Save();
        Debug.Log("해상도 설정 저장됨: 인덱스 " + resolutionDropdown.value);
    }
}

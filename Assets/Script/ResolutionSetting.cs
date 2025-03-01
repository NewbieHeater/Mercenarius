using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionSetting : SettingsValue, ISettingsSaver
{
    public TMP_Dropdown resolutionDropdown;

    private List<Resolution> resolutions = new List<Resolution>();
    private int optimalResolutionIndex = 0;

    private const string ResolutionPrefKey = "ResolutionIndex";

    private void Start()
    {
        try
        {
            if (resolutionDropdown == null)
            {
                Debug.LogError("ResolutionDropdown�� �Ҵ���� �ʾҽ��ϴ�.");
                return;
            }

            // �ػ� ��� �߰�
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

            // ���� �ý��� �ػ󵵿� ��ġ�ϴ� �ɼǿ� ��ǥ(*) �߰�
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

            // ����� �ػ� �ε����� �ִٸ� �ҷ����� (����� ���� ��ȿ���� Ȯ��)
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

            // ���� ���� �� ���õ�(�Ǵ� �⺻) �ػ󵵷� ����
            SetResolution(optimalResolutionIndex);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("ResolutionSetting Start ����: " + ex.Message);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Count)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            Debug.Log("�ػ� ����: " + resolution.width + " x " + resolution.height);
        }
        else
        {
            Debug.LogWarning("�߸��� �ػ� �ε���: " + resolutionIndex);
        }
    }

    // ISettingsSaver �������̽� ����: �ػ� ���� ����
    public void SaveSettings()
    {
        try
        {
            PlayerPrefs.SetInt(ResolutionPrefKey, resolutionDropdown.value);
            PlayerPrefs.Save();
            Debug.Log("�ػ� ���� �����: �ε��� " + resolutionDropdown.value);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("ResolutionSetting SaveSettings ����: " + ex.Message);
        }
    }

    // IResettable �������̽� ����: �⺻��(1920x1080)���� ����
    public void ResetToDefault()
    {
        // �⺻ �ػ� 1920x1080�� ����� index 5��� ����
        int defaultIndex = 5;
        resolutionDropdown.value = defaultIndex;
        resolutionDropdown.RefreshShownValue();
        SetResolution(defaultIndex);
        Debug.Log("�ػ� �⺻��(1920 x 1080)���� ���µ�");
    }
}

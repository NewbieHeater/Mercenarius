using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeVariable : MonoBehaviour, ISettingsSaver
{
    [System.Serializable]
    public class VolumeControl
    {
        public string key; // ��: "MainVolume", "MusicVolume", "SFXVolume"
        public Slider slider;
        public Button decreaseButton;
        public Button increaseButton;
        public TextMeshProUGUI showCurrentValue;
    }

    // Inspector�� ���� VolumeControl�� �Ҵ��մϴ�.
    public List<VolumeControl> volumeControls;

    private const int MinVolume = 0;
    private const int MaxVolume = 100;

    // �� ���� ���� �����ϴ� ��ųʸ� (key: VolumeControl.key)
    private Dictionary<string, int> volumeSettings = new Dictionary<string, int>();

    private void Start()
    {
        if (volumeControls == null || volumeControls.Count == 0)
        {
            Debug.LogWarning("VolumeVariable: ���� ��Ʈ���� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        foreach (var control in volumeControls)
        {
            InitializeVolumeControl(control);
        }
    }

    private void InitializeVolumeControl(VolumeControl control)
    {
        if (control.slider == null || control.decreaseButton == null || control.increaseButton == null || control.showCurrentValue == null)
        {
            Debug.LogError($"{control.key} ���� UI ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // PlayerPrefs���� ����� ���� �ҷ�����, ������ �⺻�� 50 ���
        int savedVolume = PlayerPrefs.GetInt(control.key, 50);
        volumeSettings[control.key] = savedVolume;

        // �����̴� �ʱ� ����
        control.slider.minValue = MinVolume;
        control.slider.maxValue = MaxVolume;
        control.slider.wholeNumbers = true;
        control.slider.value = savedVolume;
        control.showCurrentValue.text = savedVolume.ToString();

        // �����̴�, ��ư �̺�Ʈ�� ������ ���� ���
        control.slider.onValueChanged.RemoveAllListeners();
        control.slider.onValueChanged.AddListener(value => SetVolume(control, control.key, (int)value));

        control.decreaseButton.onClick.RemoveAllListeners();
        control.decreaseButton.onClick.AddListener(() => ChangeVolume(control, control.key, -1));

        control.increaseButton.onClick.RemoveAllListeners();
        control.increaseButton.onClick.AddListener(() => ChangeVolume(control, control.key, 1));
    }

    private void SetVolume(VolumeControl control, string key, int value)
    {
        value = Mathf.Clamp(value, MinVolume, MaxVolume);
        volumeSettings[key] = value;
        if (control.slider != null)
            control.slider.value = value;
        if (control.showCurrentValue != null)
            control.showCurrentValue.text = value.ToString();
        ApplyVolume(key, value);
    }

    private void ChangeVolume(VolumeControl control, string key, int delta)
    {
        int newValue = Mathf.Clamp((int)control.slider.value + delta, MinVolume, MaxVolume);
        SetVolume(control, key, newValue);
    }

    private void ApplyVolume(string key, int value)
    {
        float normalizedValue = value / (float)MaxVolume;
        switch (key)
        {
            case "MainVolume":
                AudioListener.volume = normalizedValue;
                break;
            case "MusicVolume":
                // AudioMixer�� ����ϴ� ���, ���⿡ ���� ���� ���� ���� �߰�
                break;
            case "SFXVolume":
                // AudioMixer�� ����ϴ� ���, ���⿡ SFX ���� ���� ���� �߰�
                break;
            default:
                break;
        }
    }

    public void SaveSettings()
    {
        try
        {
            foreach (var setting in volumeSettings)
            {
                PlayerPrefs.SetInt(setting.Key, setting.Value);
            }
            PlayerPrefs.Save();
            Debug.Log("���� ���� ���� �Ϸ�");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("VolumeVariable SaveSettings ����: " + ex.Message);
        }
    }

    public void ResetToDefault()
    {
        foreach (var control in volumeControls)
        {
            SetVolume(control, control.key, 50);
        }
        Debug.Log("���� ������ �⺻��(50)���� ���µ�");
    }

    private void OnDestroy()
    {
        if (volumeControls != null)
        {
            foreach (var control in volumeControls)
            {
                if (control.slider != null)
                    control.slider.onValueChanged.RemoveAllListeners();
                if (control.decreaseButton != null)
                    control.decreaseButton.onClick.RemoveAllListeners();
                if (control.increaseButton != null)
                    control.increaseButton.onClick.RemoveAllListeners();
            }
        }
    }
}

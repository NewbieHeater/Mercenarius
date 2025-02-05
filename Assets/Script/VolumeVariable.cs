using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeVariable : SettingsValue, ISettingsSaver
{
    [System.Serializable]
    public class VolumeControl
    {
        public string name;
        public Slider slider;
        public Button decreaseButton;
        public Button increaseButton;
        public TextMeshProUGUI showCurrentValue;
    }

    public VolumeControl mainVolume;
    public VolumeControl musicVolume;
    public VolumeControl sfxVolume;

    private const int MinVolume = 0;
    private const int MaxVolume = 100;

    // 볼륨 설정 값을 저장할 Dictionary
    private Dictionary<string, int> volumeSettings = new Dictionary<string, int>();

    private void Start()
    {
        // 각 볼륨 컨트롤 초기화 (키값은 PlayerPrefs에 저장할 때 사용)
        InitializeVolumeControl(mainVolume, "MainVolume");
        InitializeVolumeControl(musicVolume, "MusicVolume");
        InitializeVolumeControl(sfxVolume, "SFXVolume");
    }

    private void InitializeVolumeControl(VolumeControl control, string key)
    {
        // 저장된 값이 있으면 불러오고, 없으면 기본값 50을 사용
        int savedVolume = PlayerPrefs.GetInt(key, 50);
        volumeSettings[key] = savedVolume;

        control.slider.minValue = MinVolume;
        control.slider.maxValue = MaxVolume;
        control.slider.wholeNumbers = true;
        control.slider.value = savedVolume;
        control.showCurrentValue.text = savedVolume.ToString();

        // 슬라이더 값이 변경되면 SetVolume() 호출
        control.slider.onValueChanged.AddListener(value => SetVolume(control, key, (int)value));
        // 버튼 클릭 시 볼륨 증가/감소 처리
        control.decreaseButton.onClick.AddListener(() => ChangeVolume(control, key, -1));
        control.increaseButton.onClick.AddListener(() => ChangeVolume(control, key, 1));
    }

    private void SetVolume(VolumeControl control, string key, int value)
    {
        value = Mathf.Clamp(value, MinVolume, MaxVolume);
        volumeSettings[key] = value;
        control.slider.value = value;
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
        // 0 ~ 100 값을 0.0 ~ 1.0 사이의 값으로 정규화하여 적용
        float normalizedValue = value / (float)MaxVolume;
        switch (key)
        {
            case "MainVolume":
                AudioListener.volume = normalizedValue;
                break;
            case "MusicVolume":
                // AudioMixer를 사용하는 경우 여기서 음악 볼륨을 적용합니다.
                break;
            case "SFXVolume":
                // AudioMixer를 사용하는 경우 여기서 SFX 볼륨을 적용합니다.
                break;
        }
    }

    // ISettingsSaver 인터페이스 구현: 볼륨 설정을 PlayerPrefs에 저장
    public void SaveSettings()
    {
        foreach (var setting in volumeSettings)
        {
            PlayerPrefs.SetInt(setting.Key, setting.Value);
        }
        PlayerPrefs.Save();
        Debug.Log("볼륨 설정 저장 완료");
    }

    // UI 버튼 등에서 호출하여 저장 기능을 실행할 수 있도록 추가한 메서드
    public void SaveVolumeSettings()
    {
        SaveSettings();
    }
}

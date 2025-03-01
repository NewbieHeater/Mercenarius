using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeVariable : MonoBehaviour, ISettingsSaver
{
    [System.Serializable]
    public class VolumeControl
    {
        public string key; // 예: "MainVolume", "MusicVolume", "SFXVolume"
        public Slider slider;
        public Button decreaseButton;
        public Button increaseButton;
        public TextMeshProUGUI showCurrentValue;
    }

    // Inspector에 여러 VolumeControl을 할당합니다.
    public List<VolumeControl> volumeControls;

    private const int MinVolume = 0;
    private const int MaxVolume = 100;

    // 각 볼륨 값을 저장하는 딕셔너리 (key: VolumeControl.key)
    private Dictionary<string, int> volumeSettings = new Dictionary<string, int>();

    private void Start()
    {
        if (volumeControls == null || volumeControls.Count == 0)
        {
            Debug.LogWarning("VolumeVariable: 볼륨 컨트롤이 할당되지 않았습니다.");
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
            Debug.LogError($"{control.key} 관련 UI 컴포넌트가 할당되지 않았습니다.");
            return;
        }

        // PlayerPrefs에서 저장된 값을 불러오되, 없으면 기본값 50 사용
        int savedVolume = PlayerPrefs.GetInt(control.key, 50);
        volumeSettings[control.key] = savedVolume;

        // 슬라이더 초기 설정
        control.slider.minValue = MinVolume;
        control.slider.maxValue = MaxVolume;
        control.slider.wholeNumbers = true;
        control.slider.value = savedVolume;
        control.showCurrentValue.text = savedVolume.ToString();

        // 슬라이더, 버튼 이벤트를 루프를 통해 등록
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
                // AudioMixer를 사용하는 경우, 여기에 음악 볼륨 적용 로직 추가
                break;
            case "SFXVolume":
                // AudioMixer를 사용하는 경우, 여기에 SFX 볼륨 적용 로직 추가
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
            Debug.Log("볼륨 설정 저장 완료");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("VolumeVariable SaveSettings 오류: " + ex.Message);
        }
    }

    public void ResetToDefault()
    {
        foreach (var control in volumeControls)
        {
            SetVolume(control, control.key, 50);
        }
        Debug.Log("볼륨 설정이 기본값(50)으로 리셋됨");
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

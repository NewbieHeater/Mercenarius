using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeVariable : SettingsValue
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

    private void Start()
    {
        InitializeVolumeControl(mainVolume, "MainVolume");
        InitializeVolumeControl(musicVolume, "MusicVolume");
        InitializeVolumeControl(sfxVolume, "SFXVolume");
    }

    private void InitializeVolumeControl(VolumeControl control, string key)
    {
        int savedVolume = PlayerPrefs.GetInt(key, 50);
        volumeSettings[key] = savedVolume;

        control.slider.minValue = MinVolume;
        control.slider.maxValue = MaxVolume;
        control.slider.wholeNumbers = true;
        control.slider.value = savedVolume;
        control.showCurrentValue.text = savedVolume.ToString();

        control.slider.onValueChanged.AddListener(value => SetVolume(control, key, (int)value));
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
        float normalizedValue = value / (float)MaxVolume;
        switch (key)
        {
            case "MainVolume":
                AudioListener.volume = normalizedValue;
                break;
            case "MusicVolume":
                // AudioMixer 사용 시 적용
                break;
            case "SFXVolume":
                // AudioMixer 사용 시 적용
                break;
        }
    }

    public void SaveVolumeSettings()
    {
        SaveSettings();
    }
}

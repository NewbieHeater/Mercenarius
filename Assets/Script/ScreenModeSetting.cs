using UnityEngine;
using UnityEngine.UI;

public class ScreenModeSetting : SettingsValue, ISettingsSaver
{
    // PlayerPrefs에 저장할 때 사용할 키
    private const string FullScreenPrefKey = "FullScreen";

    public void SetFullScreen()
    {
        Screen.fullScreen = true;
        Debug.Log("전체화면 모드 활성화");
    }

    // 창 모드(윈도우 모드)로 전환하는 메서드
    public void SetWindowedMode()
    {
        Screen.fullScreen = false;
        Debug.Log("창 모드 활성화");
    }

    // 현재 화면 모드를 토글(전환)하는 메서드
    public void ToggleScreenMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("화면 모드 전환: " + (Screen.fullScreen ? "전체화면" : "창 모드"));
    }

    public void SaveSettings()
    {
        // Screen.fullScreen 은 bool 타입이므로, 1(true) 또는 0(false)로 변환하여 저장합니다.
        PlayerPrefs.SetInt(FullScreenPrefKey, Screen.fullScreen ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("화면 설정 저장됨: " + (Screen.fullScreen ? "전체화면" : "창 모드"));
    }
}

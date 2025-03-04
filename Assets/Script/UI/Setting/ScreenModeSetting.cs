using UnityEngine;

public class ScreenModeSetting : MonoBehaviour, ISettingsSaver
{
    private const string FullScreenPrefKey = "FullScreen";

    public void SetFullScreen()
    {
        Screen.fullScreen = true;
        Debug.Log("전체화면 모드 활성화");
    }

    public void SetWindowedMode()
    {
        Screen.fullScreen = false;
        Debug.Log("창 모드 활성화");
    }

    public void ToggleScreenMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("화면 모드 전환: " + (Screen.fullScreen ? "전체화면" : "창 모드"));
    }

    public void SaveSettings()
    {
        try
        {
            PlayerPrefs.SetInt(FullScreenPrefKey, Screen.fullScreen ? 1 : 0);
            PlayerPrefs.Save();
            Debug.Log("화면 설정 저장됨: " + (Screen.fullScreen ? "전체화면" : "창 모드"));
        }
        catch (System.Exception ex)
        {
            Debug.LogError("ScreenModeSetting SaveSettings 오류: " + ex.Message);
        }
    }

    public void ResetToDefault()
    {
        SetFullScreen(); // 기본값은 전체화면
        Debug.Log("화면 모드가 기본값(전체화면)으로 리셋됨");
    }
}

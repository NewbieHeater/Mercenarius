using System.Collections.Generic;
using UnityEngine;

public interface ISettingsSaver
{
    void SaveSettings();
    void ResetToDefault();
}

public class SettingsValue : MonoBehaviour
{
    private List<ISettingsSaver> settingsSavers;

    private void Awake()
    {
        // 현재 오브젝트와 자식 오브젝트에 있는 모든 ISettingsSaver 컴포넌트를 찾아 리스트로 저장
        settingsSavers = new List<ISettingsSaver>(GetComponentsInChildren<ISettingsSaver>());
    }

    public void SaveAllSettings()
    {
        foreach (var saver in settingsSavers)
        {
            saver.SaveSettings();
        }
        Debug.Log("모든 설정 저장 완료");
    }

    public void ResetAllDefaults()
    {
        foreach (var saver in settingsSavers)
        {
            saver.ResetToDefault();
        }
        Debug.Log("모든 설정이 기본값으로 초기화되었습니다.");
    }

    // UI 버튼과 연결할 수 있는 메서드 예시
    public void OnSaveButtonClicked()
    {
        SaveAllSettings();
    }

    public void OnResetButtonClicked()
    {
        ResetAllDefaults();
    }
}

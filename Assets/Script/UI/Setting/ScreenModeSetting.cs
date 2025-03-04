using UnityEngine;

public class ScreenModeSetting : MonoBehaviour, ISettingsSaver
{
    private const string FullScreenPrefKey = "FullScreen";

    public void SetFullScreen()
    {
        Screen.fullScreen = true;
        Debug.Log("��üȭ�� ��� Ȱ��ȭ");
    }

    public void SetWindowedMode()
    {
        Screen.fullScreen = false;
        Debug.Log("â ��� Ȱ��ȭ");
    }

    public void ToggleScreenMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("ȭ�� ��� ��ȯ: " + (Screen.fullScreen ? "��üȭ��" : "â ���"));
    }

    public void SaveSettings()
    {
        try
        {
            PlayerPrefs.SetInt(FullScreenPrefKey, Screen.fullScreen ? 1 : 0);
            PlayerPrefs.Save();
            Debug.Log("ȭ�� ���� �����: " + (Screen.fullScreen ? "��üȭ��" : "â ���"));
        }
        catch (System.Exception ex)
        {
            Debug.LogError("ScreenModeSetting SaveSettings ����: " + ex.Message);
        }
    }

    public void ResetToDefault()
    {
        SetFullScreen(); // �⺻���� ��üȭ��
        Debug.Log("ȭ�� ��尡 �⺻��(��üȭ��)���� ���µ�");
    }
}

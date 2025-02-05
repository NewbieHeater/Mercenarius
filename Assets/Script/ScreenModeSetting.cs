using UnityEngine;
using UnityEngine.UI;

public class ScreenModeSetting : SettingsValue, ISettingsSaver
{
    // PlayerPrefs�� ������ �� ����� Ű
    private const string FullScreenPrefKey = "FullScreen";

    public void SetFullScreen()
    {
        Screen.fullScreen = true;
        Debug.Log("��üȭ�� ��� Ȱ��ȭ");
    }

    // â ���(������ ���)�� ��ȯ�ϴ� �޼���
    public void SetWindowedMode()
    {
        Screen.fullScreen = false;
        Debug.Log("â ��� Ȱ��ȭ");
    }

    // ���� ȭ�� ��带 ���(��ȯ)�ϴ� �޼���
    public void ToggleScreenMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("ȭ�� ��� ��ȯ: " + (Screen.fullScreen ? "��üȭ��" : "â ���"));
    }

    public void SaveSettings()
    {
        // Screen.fullScreen �� bool Ÿ���̹Ƿ�, 1(true) �Ǵ� 0(false)�� ��ȯ�Ͽ� �����մϴ�.
        PlayerPrefs.SetInt(FullScreenPrefKey, Screen.fullScreen ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("ȭ�� ���� �����: " + (Screen.fullScreen ? "��üȭ��" : "â ���"));
    }
}

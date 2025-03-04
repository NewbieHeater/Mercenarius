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
        // ���� ������Ʈ�� �ڽ� ������Ʈ�� �ִ� ��� ISettingsSaver ������Ʈ�� ã�� ����Ʈ�� ����
        settingsSavers = new List<ISettingsSaver>(GetComponentsInChildren<ISettingsSaver>());
    }

    public void SaveAllSettings()
    {
        foreach (var saver in settingsSavers)
        {
            saver.SaveSettings();
        }
        Debug.Log("��� ���� ���� �Ϸ�");
    }

    public void ResetAllDefaults()
    {
        foreach (var saver in settingsSavers)
        {
            saver.ResetToDefault();
        }
        Debug.Log("��� ������ �⺻������ �ʱ�ȭ�Ǿ����ϴ�.");
    }

    // UI ��ư�� ������ �� �ִ� �޼��� ����
    public void OnSaveButtonClicked()
    {
        SaveAllSettings();
    }

    public void OnResetButtonClicked()
    {
        ResetAllDefaults();
    }
}

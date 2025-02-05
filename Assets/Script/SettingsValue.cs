using UnityEngine;
public interface ISettingsSaver
{
    void SaveSettings();
}
public class SettingsValue : MonoBehaviour
{
    // �� ��ũ��Ʈ�� �Ҹ� ����(VolumeVariable)�� �ػ� ����(ResolutionManager)�� ���Ե� �θ� ������Ʈ�� ���Դϴ�.
    // ��ư�� OnClick �̺�Ʈ�� �� �޼��带 �����ϼ���.
    public void SaveAllSettings()
    {
        // �θ� ������Ʈ�� �ڽĵ� �� ISettingsSaver�� ������ ��� ������Ʈ�� �����ɴϴ�.
        ISettingsSaver[] settingSavers = GetComponentsInChildren<ISettingsSaver>();

        foreach (ISettingsSaver saver in settingSavers)
        {
            saver.SaveSettings();
        }
        Debug.Log("��� ���� ���� �Ϸ�");
    }
}
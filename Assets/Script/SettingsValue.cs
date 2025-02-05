using UnityEngine;
public interface ISettingsSaver
{
    void SaveSettings();
}
public class SettingsValue : MonoBehaviour
{
    // 이 스크립트를 소리 설정(VolumeVariable)과 해상도 설정(ResolutionManager)이 포함된 부모 오브젝트에 붙입니다.
    // 버튼의 OnClick 이벤트에 이 메서드를 연결하세요.
    public void SaveAllSettings()
    {
        // 부모 오브젝트의 자식들 중 ISettingsSaver를 구현한 모든 컴포넌트를 가져옵니다.
        ISettingsSaver[] settingSavers = GetComponentsInChildren<ISettingsSaver>();

        foreach (ISettingsSaver saver in settingSavers)
        {
            saver.SaveSettings();
        }
        Debug.Log("모든 설정 저장 완료");
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class volume : MonoBehaviour
{
    public Slider slider; // UI Slider 연결 (예를 들어, 볼륨 조절용)

    private void Start()
    {
        // 이전에 설정한 값이 있으면 Slider에 적용
        float savedValue = PlayerPrefs.GetFloat("SliderValue", 0.5f); // 기본값 0.5로 설정
        slider.value = savedValue;
    }

    public void SaveAndPlay()
    {
        // Slider에서 값을 가져와서 저장
        float sliderValue = slider.value;
        PlayerPrefs.SetFloat("SliderValue", sliderValue);
        PlayerPrefs.Save(); // 변경 사항을 저장

        // 다음 씬(게임 플레이 씬)으로 이동
        SceneManager.LoadScene("GamePlayScene");
    }
}

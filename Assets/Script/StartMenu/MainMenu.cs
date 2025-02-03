using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void ClickBtn()
    {
        print("버튼 클릭");

        // 방금 클릭한 게임 오브젝트를 가져와서 저장
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        // 방금 클릭한 게임 오브젝트의 이름과 버튼 속 문자 출력
        print(clickObject.name + ", " + clickObject.GetComponentInChildren<Text>().text);
    }
}
//Managers.UI.SwitchPanel("MainMenu");
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void ClickBtn()
    {
        print("��ư Ŭ��");

        // ��� Ŭ���� ���� ������Ʈ�� �����ͼ� ����
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        // ��� Ŭ���� ���� ������Ʈ�� �̸��� ��ư �� ���� ���
        print(clickObject.name + ", " + clickObject.GetComponentInChildren<Text>().text);
    }
}
//Managers.UI.SwitchPanel("MainMenu");
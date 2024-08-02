using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction { UP,DOWN,LEFT,RIGHT,KEYCOUNT } //KEYACTION 이름으로 열거형을 하나 생성

public static class KeySetting { public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>(); } //DICIONARY를 이용하여 각 값을 추가하고 불러옴

public class KeyManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[]{ KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D }; //KeyCode 배열을 만들어서 W,A,S,D로 초기

    private void Start()
    {                       //KeyAction은 정수형이 아니기 때문에 캐스팅
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++) //for문을 이용하여 keys.Add를 반복
        {
            KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
        }
    }
    private void OnGUI()
    {
            Event keyEvent = Event.current; // Event를 이용하여 현재 실행되는 Event를 불러옴
            if (keyEvent.isKey) //key가 눌렀을때만 실행시키기 위해 조건을 달아줌,Event의 isKey로 키보드가 눌렸는지 알 수 있음
            {
                KeySetting.keys[(KeyAction)key] = keyEvent.keyCode; //받아온 KeyCode를 keys에 널어줌
                key = -1;
            }
    }

    int key = -1;
    public void ChangeKey(int num)
    {
            key = num;
    }

}

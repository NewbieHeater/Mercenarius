using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_InGameSettings : UI_Popup
{
    enum Buttons
    {
        Resume,
        System,
        Achivement,
        MainMenu,
    }

    enum Texts
    {

    }

    enum GameObjects
    {

    }

    enum Images
    {

    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.Resume).gameObject.BindEvent((PointerEventData data) => { gameObject.SetActive(false); });
        GetButton((int)Buttons.System).gameObject.BindEvent((PointerEventData data) => { Managers.UI.ShowPopupUI<UI_SystemMenuBind>("SystemMenu"); });
        GetButton((int)Buttons.Achivement).gameObject.BindEvent((PointerEventData data) => { Debug.Log("도전과제"); });
        GetButton((int)Buttons.MainMenu).gameObject.BindEvent((PointerEventData data) => { Managers.Scene.LoadScene(Define.Scene.MainMenu); });
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_SaveMenu : UI_Popup
{
    enum Buttons
    {
        Exit,
        Save1,
        Save2,
        Save3,
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

        GetButton((int)Buttons.Exit).gameObject.BindEvent((PointerEventData data) => { gameObject.SetActive(false); });
        GetButton((int)Buttons.Save1).gameObject.BindEvent((PointerEventData data) => { Managers.Scene.LoadScene(Define.Scene.Game); });
        GetButton((int)Buttons.Save2).gameObject.BindEvent((PointerEventData data) => { Managers.Scene.LoadScene(Define.Scene.Game); });
        GetButton((int)Buttons.Save3).gameObject.BindEvent((PointerEventData data) => { Managers.Scene.LoadScene(Define.Scene.Game); });
    }
}

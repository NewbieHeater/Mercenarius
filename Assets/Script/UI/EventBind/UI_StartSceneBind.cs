using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StartSceneBind : UI_Popup
{
    enum Buttons
    {
        NewGameButton,
        LoadGameButton,
        SystemButton,
        QuitGameButton,
    }

    enum Texts
    {
        
    }

    enum GameObjects
    {

    }

    enum Images
    {
        Arrow,
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

        GetButton((int)Buttons.NewGameButton).gameObject.BindEvent(OnNewGameButtonClicked);
        GetButton((int)Buttons.LoadGameButton).gameObject.BindEvent(OnLoadGameButtonClicked);
        GetButton((int)Buttons.SystemButton).gameObject.BindEvent(OnSystemButtonClicked);
        GetButton((int)Buttons.QuitGameButton).gameObject.BindEvent((PointerEventData data) => { Application.Quit(); });
        //GameObject go = GetImage((int)Buttons.NewGameButton).gameObject;
        //BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }



    public void OnNewGameButtonClicked(PointerEventData data)
    {
        ClosePopupUI();   

    }

    public void OnSystemButtonClicked(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_SystemMenuBind>("SystemMenu");
    }

    public void OnLoadGameButtonClicked(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_SystemMenuBind>("SystemMenu");
    }

}

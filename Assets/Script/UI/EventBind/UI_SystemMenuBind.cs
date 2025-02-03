using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_SystemMenuBind : UI_Popup
{
    public Sprite[] uperSprite;
    private Buttons curState = Buttons.Graphics;
    private Image SystemUperScreen;
    private RectTransform content;
    private Dictionary<Buttons, GameObjects> buttonToPanelMap = new Dictionary<Buttons, GameObjects>();

    enum Buttons
    {
        Sound,
        Graphics,
        KeySetting,
    }

    enum Texts
    {
        MasterVolumeText,
        MusicVolumeText,
        SoundEffectVolumeText,
    }

    enum GameObjects
    {
        OptionVolumeParent,
        OptionGraphicParent,
        OptionKeySettingParent,
        Content,
    }

    enum Images
    {
        SystemUper,
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

        SystemUperScreen = GetImage((int)Images.SystemUper);
        content = GetObject((int)GameObjects.Content).GetComponent<RectTransform>();

        // 버튼과 패널 매핑 (Dictionary 사용)
        buttonToPanelMap[Buttons.Sound] = GameObjects.OptionVolumeParent;
        buttonToPanelMap[Buttons.Graphics] = GameObjects.OptionGraphicParent;
        buttonToPanelMap[Buttons.KeySetting] = GameObjects.OptionKeySettingParent;

        // 버튼 이벤트 등록 자동화
        foreach (Buttons btn in Enum.GetValues(typeof(Buttons)))
        {
            GetButton((int)btn).gameObject.BindEvent((PointerEventData data) =>
            {
                ChangeSettingScreen(btn);
            });
        }

        // 초기화면 설정
        ChangeSettingScreen(Buttons.Sound);
    }

    private void ChangeSettingScreen(Buttons selectedState)
    {
        if (curState == selectedState) return;

        curState = selectedState;
        SystemUperScreen.sprite = uperSprite[(int)selectedState];

        // 활성화할 패널 가져오기
        GameObjects activeObject = buttonToPanelMap[selectedState];

        // 모든 패널 비활성화 후, 선택된 패널만 활성화
        foreach (var entry in buttonToPanelMap.Values)
        {
            GetObject((int)entry).SetActive(entry == activeObject);
        }

        // content 크기 업데이트 (한 번만 `GetComponent` 호출)
        RectTransform activeRect = GetObject((int)activeObject).GetComponent<RectTransform>();
        content.sizeDelta = new Vector2(1160, activeRect.sizeDelta.y);
    }
}

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

        // ��ư�� �г� ���� (Dictionary ���)
        buttonToPanelMap[Buttons.Sound] = GameObjects.OptionVolumeParent;
        buttonToPanelMap[Buttons.Graphics] = GameObjects.OptionGraphicParent;
        buttonToPanelMap[Buttons.KeySetting] = GameObjects.OptionKeySettingParent;

        // ��ư �̺�Ʈ ��� �ڵ�ȭ
        foreach (Buttons btn in Enum.GetValues(typeof(Buttons)))
        {
            GetButton((int)btn).gameObject.BindEvent((PointerEventData data) =>
            {
                ChangeSettingScreen(btn);
            });
        }

        // �ʱ�ȭ�� ����
        ChangeSettingScreen(Buttons.Sound);
    }

    private void ChangeSettingScreen(Buttons selectedState)
    {
        if (curState == selectedState) return;

        curState = selectedState;
        SystemUperScreen.sprite = uperSprite[(int)selectedState];

        // Ȱ��ȭ�� �г� ��������
        GameObjects activeObject = buttonToPanelMap[selectedState];

        // ��� �г� ��Ȱ��ȭ ��, ���õ� �гθ� Ȱ��ȭ
        foreach (var entry in buttonToPanelMap.Values)
        {
            GetObject((int)entry).SetActive(entry == activeObject);
        }

        // content ũ�� ������Ʈ (�� ���� `GetComponent` ȣ��)
        RectTransform activeRect = GetObject((int)activeObject).GetComponent<RectTransform>();
        content.sizeDelta = new Vector2(1160, activeRect.sizeDelta.y);
    }
}

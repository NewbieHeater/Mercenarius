using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public GameObject GameObject;
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        Managers.Input.KeyAction -= SettingTogle;
        Managers.Input.KeyAction += SettingTogle;
        //Managers.UI.ShowSceneUI<UI_Inven>();

        GameObject.SetActive(false);
    }

    public void SettingTogle()
    {
        if (Managers.KeyInput.GetKeyDown("Settings"))
        {
            if(GameObject.activeSelf)
            {
                GameObject.SetActive(false);
            }
            else
            {
                GameObject.SetActive(true);
            }
        }
    }

    public override void Clear()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.StartScene;
        
    }
    private void Start()
    {
        Managers.UI.ShowPopupUI<UI_StartSceneBind>("MainMenu");
    }


    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}

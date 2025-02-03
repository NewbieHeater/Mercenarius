using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.MainMenu;
        
    }
    private void Start()
    {
        Managers.UI.ShowPopupUI<UI_Popup>("MainMenu");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Select;
        UIMaker.Instance.CreateCanvas(0);
    }

    private void Update()
    {
        
    }

    public override void Clear()
    {
        Debug.Log("Select Scene Clear!");
    }
}

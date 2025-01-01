using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : Character
{
    protected override void Start()
    {
        base.Start();
    }
    
    private void Update()
    {
        if(CharacterState == "Idle")
        {
            if(Input.GetMouseButtonDown(0))
            {
                sm.SetState(dicState["Move"]);
            }
            if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("BasicAttack")))
            {
                sm.SetState(dicState["Attack"]);
            }
            
        }
        if (CharacterState == "Move")
        {

        }
        if (CharacterState == "Attack")
        {

        }
    }
}
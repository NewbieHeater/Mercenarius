using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagement : MonoBehaviour
{
    private Character parentCharacter;
    void Awake()
    {
        // 부모 Character 스크립트를 가져옵니다.
        parentCharacter = GetComponentInParent<Character>();
    }

    public void ComboEnable()
    {
        if (parentCharacter != null)
        {
            parentCharacter.ComboEnable();
        }
    }
    public void ComboDisable()
    {
        if (parentCharacter != null)
        {
            parentCharacter.ComboDisable();
        }
    }
    public void ComboExit()
    {
        if (parentCharacter != null)
        {
            parentCharacter.ComboExit();
        }
    }
    public void AttackEnd()
    {
        if (parentCharacter != null)
        {
            parentCharacter.AttackEnd();
        }
    }
}

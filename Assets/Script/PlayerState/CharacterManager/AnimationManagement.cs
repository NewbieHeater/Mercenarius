using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagement : MonoBehaviour
{
    private Character parentCharacter;
    void Awake()
    {
        // �θ� Character ��ũ��Ʈ�� �����ɴϴ�.
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
            parentCharacter.animator.SetTrigger("NextCombo");
        }
    }
    public void AttackEnd()
    {
        if (parentCharacter != null)
        {
            parentCharacter.animator.ResetTrigger("NextCombo");
            parentCharacter.animator.SetBool("Attack", false);
        }
    }
}

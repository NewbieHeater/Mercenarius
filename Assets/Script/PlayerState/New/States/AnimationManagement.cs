using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagement : MonoBehaviour
{
    void OnAnimationEnd()
    {
        // �θ� ������Ʈ�� ��ũ��Ʈ�� "OnAttackEnd" �޼��� ȣ��
        transform.parent.SendMessage("OnAttackAnimeEnd", SendMessageOptions.DontRequireReceiver);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagement : MonoBehaviour
{
    void OnAnimationEnd()
    {
        // 부모 오브젝트의 스크립트에 "OnAttackEnd" 메서드 호출
        transform.parent.SendMessage("OnAttackAnimeEnd", SendMessageOptions.DontRequireReceiver);
    }
}

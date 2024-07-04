using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisappear : MonoBehaviour
{
    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 \n" +
        CancelInvoke();
    }
}

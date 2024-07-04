using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisappear : MonoBehaviour
{
    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);    // �� ��ü�� �ѹ��� \n" +
        CancelInvoke();
    }
}

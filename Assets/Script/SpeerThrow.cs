using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeerThrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);    // �� ��ü�� �ѹ��� 
        CancelInvoke();    // Monobehaviour�� Invoke�� �ִٸ� 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

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
        ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaerThrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);   
        CancelInvoke();    
    }
    // Update is called once per frame
    void Update()
    {

    }
}
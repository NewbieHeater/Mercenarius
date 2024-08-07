using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(Vector3.up * 1000f);
        }
    }
}

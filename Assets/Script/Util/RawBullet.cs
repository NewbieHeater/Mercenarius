using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RawBullet : MonoBehaviour
{
    Rigidbody rb;
    public float bulletSpeed = 200f;
    public int remainTime = 5;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed * 2f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //transform.rotation = Quaternion.identity;

        StartCoroutine(DisappearOnTime());
    }

    IEnumerator DisappearOnTime()
    {
        yield return new WaitForSeconds(remainTime);
        this.gameObject.SetActive(false);
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy"))
        {
            this.gameObject.SetActive(false);
        }
    }
}

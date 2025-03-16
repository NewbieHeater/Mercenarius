using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class RawBullet : MonoBehaviour
{
    Rigidbody rb;
    public float bulletSpeed = 200f;
    public int remainTime = 5;
    public float damageBuffer = 1.2f;
    Character character;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        character = CharacterManager.Instance.character.GetComponent<Character>();
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
            other.GetComponent<EnemyGolemController>().TakeDamage(character.statData.curAttack * damageBuffer);
            this.gameObject.SetActive(false);
        }
    }
}

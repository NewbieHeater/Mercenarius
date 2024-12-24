using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Character character;
    private Enemy_AcceptDamage enemy_AcceptDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject target_Enemy = GameObject.Find(other.gameObject.name);
            enemy_AcceptDamage = target_Enemy.GetComponent<Enemy_AcceptDamage>();
            //enemy_AcceptDamage.Accept_Damage_Oneshot(character.statData.curAttack);
        }
    }
}

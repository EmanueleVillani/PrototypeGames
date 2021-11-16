using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject damageEffect;
    public int damageAmount = 40;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Instantiate(damageEffect, transform.position, damageEffect.transform.rotation);
            other.GetComponent<Enemy>().TakeDamage(damageAmount);
        }

        if (other.CompareTag("Insect"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damageAmount);

            Debug.Log("COLLIDED");
        }
    }
}

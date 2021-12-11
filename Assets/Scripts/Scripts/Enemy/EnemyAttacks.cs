using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public Collider attackzone;
    public void Attack(int damageAmount)
    {
        //Debug.Log(damageAmount);
        attackzone.enabled = true;
        //PlayerManager.currentHealth -= damageAmount;
    }

    public void DisableAttack()
    {
        attackzone.enabled = false;
    }
}

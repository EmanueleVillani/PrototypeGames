using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth1 : MonoBehaviour
{
    [SerializeField]
    private int enemyHealth = 100;

    [SerializeField]
    private FireBall FireballScript;

    [SerializeField]
    private GameObject insectChildModel;


    private void Start()
    {
       // Invoke("Dead", 5f);
    }



    public void TakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;

        if (enemyHealth < 0)
            enemyHealth = 0;

        if (enemyHealth == 0)
        {

            insectChildModel.GetComponent<Animator>().SetTrigger("Death");

            insectChildModel.GetComponent<Rigidbody>().isKinematic = false;

            GetComponent<Rigidbody>().isKinematic = true;

             GetComponent<FlyingEnemy>().enabled = false;

            Destroy(gameObject, 5f);
            

        }


    }
    
    void Dead()
    {

        insectChildModel.GetComponent<Animator>().SetTrigger("Death");

        insectChildModel.GetComponent<Rigidbody>().isKinematic = false;

        GetComponent<FlyingEnemy>().enabled = false;

        Destroy(gameObject, 5f);



    }




}

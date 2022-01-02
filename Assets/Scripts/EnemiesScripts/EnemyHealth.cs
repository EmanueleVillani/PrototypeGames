using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int enemyHealth = 100;

    public bool isDead;
 
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

            isDead = true;
            PlayerManager.instancePlayerManager.AddCount();
           // GameManager.gameManagerInstance.AddCount();
            gameObject.GetComponent<Animator>().SetTrigger("Death");
         //   gameObject.GetComponent<Rigidbody>().isKinematic = false;
          //  gameObject.GetComponent<Animator>().applyRootMotion = false;
            //  gameObject.GetComponent<Collider>().isTrigger = true;
            // gameObject.GetComponentInParent<Rigidbody>().isKinematic = true;
            // gameObject.GetComponentInParent<Collider>().isTrigger = true;
            gameObject.GetComponentInParent<FlyingEnemy>().enabled = false;
          
          
           

            Destroy(gameObject.transform.parent.gameObject, 3f);
        }

    }
    
    void Dead()
    {
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Death");
        gameObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
        gameObject.GetComponentInChildren<FlyingEnemy>().enabled = false;

        
        Destroy(gameObject, 5f);
    }




}

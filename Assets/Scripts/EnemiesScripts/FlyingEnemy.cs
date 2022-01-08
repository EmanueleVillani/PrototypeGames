using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemy : MonoBehaviour
{
 
    private string current_State = "IdleState";

    [SerializeField]
    private float chaseRange = 20f;

    [SerializeField]
    private float distanceToAttackOnAir = 10f;

   
    public bool canMove;

    private float timerAttackFly;
    private float attackTimeFlyThreshold = 3f;   //regola l'attacco mentre l'enemy vola

    private Transform target;

    private float distanceTarget;

    [SerializeField]
    private Animator anim;

    
    public float speed = 10f;

    private float yPosition;  //fix della posizione dell'enemy lungo l'asse y

  
    public bool isFlying;

    [SerializeField]
    private int enemyHealth = 100;

    [SerializeField]
    private Slider healthBar;

    public bool isDead;


    private void Start()
    {
        healthBar.maxValue = enemyHealth;
        healthBar.value = enemyHealth;
        target = GameObject.FindWithTag("Player").transform;
        transform.rotation = Quaternion.Euler(0, 90, 0);   
      
        canMove = true;
        yPosition = transform.position.y;

    }


    private void Update()
    {

        if (!isFlying)     
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);

        distanceTarget = Vector3.Distance(transform.position, target.position);
       
        healthBar.value = enemyHealth;

        if (current_State == "IdleState")
        {
            if (distanceTarget < chaseRange)
            {

                current_State = "ChaseState";

              
            }
        } 
        else if (current_State=="ChaseState")
        {
            anim.SetBool("isAttacking", false);

            anim.SetTrigger("chase");

            anim.SetTrigger("flyStart");


            if (distanceTarget < distanceToAttackOnAir && isFlying)
            {
               current_State = "AttackState";
            }


            Invoke("StartFly", 1.5f);  

            MoveEnemy(); 
            
        }
        else if (current_State == "AttackState")
        {
            if (isFlying)
            {
                if(distanceTarget > distanceToAttackOnAir)
                {
                    current_State = "ChaseState";
                }

                    AttackAir();   
            }
        }
    }
    public void TakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;

        if (enemyHealth < 0)
            enemyHealth = 0;

        if (enemyHealth == 0)
        {
            isDead = true;
            InventoryManager.Instance.ModifyInventory(Item.kill,true,1);
            //PlayerManager.Instance.AddCount();
            gameObject.GetComponentInChildren<Animator>().SetTrigger("Death");
            gameObject.GetComponentInParent<FlyingEnemy>().enabled = false;
            healthBar.gameObject.SetActive(false);

            Destroy(gameObject, 1.5f);
        }

    }
    void MoveEnemy()
    {   
        int xMovement=1;

        Vector3 targetPos = new Vector3 (target.transform.position.x, transform.position.y, transform.position.z);

        if (isFlying && canMove)
        {        
           transform.Translate(Vector3.forward * xMovement * speed * Time.deltaTime);
        }


        if (transform.position.x > target.position.x)
        {
             transform.rotation = Quaternion.Euler(0, 270, 0);
            xMovement = 1;
        }
        else
        {         
            transform.rotation = Quaternion.Euler(0, 90, 0);
            xMovement = 1;
        }



    }
    
    void AttackAir()
    {
        if (Time.time > timerAttackFly )
        {
            
          timerAttackFly = Time.time + attackTimeFlyThreshold;
          anim.SetTrigger("attackFly");

        }
    }


    void StartFly()
    {
        isFlying = true;      
    }
   

    


   
}

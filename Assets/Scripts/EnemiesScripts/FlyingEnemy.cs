using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
 
    private string current_State = "IdleState";

    [SerializeField]
    private float chaseRange = 20f;

    [SerializeField]
    private float distanceToAttackOnAir = 10f;


   
    private float timeStun = 8;
    private bool isStunning;
    private float minHighAnimation=3f, maxHighAnimation = 9f;

    
    private float timerAttackFly;
    private float attackTimeFlyThreshold = 3f;   //regola l'attacco mentre l'enemy vola

    private Transform target;

    [SerializeField]
    private Transform model;

    private float distanceTarget;

    [SerializeField]
    private Animator anim;

    
    public float speed = 10f;

    private float yPosition;  //fix della posizione dell'enemy lungo l'asse y

  
    private bool isFlying;

    private float minDistance = 8f;

   
    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        transform.rotation = Quaternion.Euler(0, 0, 0);
       
        yPosition = transform.position.y;

    }



    private void Update()
    {

        if (!isFlying)     
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);

        distanceTarget = Vector3.Distance(transform.position, target.position);


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

    public void Stun()
    {
        if (!GetComponentInChildren<EnemyHealth>().isDead)
        {
            if (model.position.y < minHighAnimation)
                model.position = new Vector3(model.position.x, minHighAnimation, model.position.z);

            if (model.position.y > maxHighAnimation)
                model.position = new Vector3(model.position.x, maxHighAnimation, model.position.z);

        }

        if (!isStunning)
        {
            isStunning = true;
            anim.SetBool("Stun", true);

            StartCoroutine(_ReturnNormal(timeStun));

        }
        
    }


    void MoveEnemy()
    {
        
        float step = speed * Time.deltaTime;

        Vector3 targetPos = new Vector3 (target.transform.position.x, transform.position.y, transform.position.z);
          
        if(isFlying)
           transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if (transform.position.x > target.position.x)
        {
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
           
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }
    


    void AttackAir()
    {
        if (Time.time > timerAttackFly )
        {
            
          timerAttackFly = Time.time + attackTimeFlyThreshold;

        //  transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x + Random.Range(-10f, 10f), transform.position.y + Random.Range(-5f, 5f), transform.position.z),100f*Time.deltaTime);

            anim.SetTrigger("attackFly");

        }
    }


    void StartFly()
    {
        isFlying = true;
        
    }
   

    IEnumerator _ReturnNormal(float timerStun)
    {
        yield return new WaitForSeconds(timerStun);

        anim.SetBool("Stun", false);
    
        isStunning = false;
    }


   
}

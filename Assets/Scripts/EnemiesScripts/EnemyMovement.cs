using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;

    private Animator anim;

    private SpriteRenderer sr;

    [SerializeField]
    private float maxDistanceEnemyPlayer = 10f;  //When the distance Enemy-Player is less than this variable the enemy will moves towards the player.

    [SerializeField]
    private float minDistanceEnemyPlayer = 0.2f; //When the Enemy is close to the player will starts to attack.

    private PlayerHealth playerHealth;

    private Transform target;

    [SerializeField]
    private int damage = 20;

    private bool canAttack=true;

    private float attackTimerThreshold = 2f;
    private float attackTimer;


    private void Awake()
    {
        anim = GetComponent<Animator>();
       

        target = GameObject.FindWithTag("Player").transform;

        playerHealth = target.GetComponent<PlayerHealth>();
        sr = GetComponent<SpriteRenderer>();

    }


    private void Start()
    {
        anim.Play("Idle");

        transform.rotation = Quaternion.Euler(0, 270, 0);

       
    }


    private void Update()
    {
        if (!target)
            return;

        if ((Vector3.Distance(transform.position, target.position) < maxDistanceEnemyPlayer))
        {
            if((Vector3.Distance(transform.position, target.position) > minDistanceEnemyPlayer))
            {
               
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);

                anim.SetBool("isMoving",true);

            }
            else
            {

             //   AttackPlayer(); 
               // anim.SetBool("Walk", false);
                
            }

        }
        else
        {
            anim.SetBool("isMoving", false);

        }

        if (target.position.x < transform.position.x)
        {

            Quaternion rotation = Quaternion.LookRotation(Vector3.left);
            transform.rotation = rotation;

        }     
        else
        {
           
                Quaternion rotation = Quaternion.LookRotation(Vector3.right);
                transform.rotation = rotation;
        }
            
   
    }





    public void AttackPlayer()
    {   
       
        if (Time.time > attackTimer )
        {
            
           Debug.Log("AttackPlayer");
           playerHealth.currentHealth -= damage;

            attackTimer = Time.time + attackTimerThreshold;

           // if (playerHealth.currentHealth <= 0)
            //    GameManager.gameManagerInstance.DestroyPlayer();
           
        }

    }

}

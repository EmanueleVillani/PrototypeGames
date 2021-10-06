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

    private PlayerHealth player;

    [SerializeField]
    private int damage = 20;

    private bool canAttack=true;

    private float attackTimerThreshold = 2f;
    private float attackTimer;

   
    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        

        sr = GetComponent<SpriteRenderer>();

    }


    private void Start()
    {
        anim.Play("Idle");

    }


    private void Update()
    {
        if (!player)
            return;


        if ((Vector2.Distance(transform.position, player.transform.position) < maxDistanceEnemyPlayer))
        {
            if((Vector2.Distance(transform.position, player.transform.position) > minDistanceEnemyPlayer))
            {
               
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);

                anim.SetBool("Walk",true);

            }
            else
            {

                AttackPlayer(); 
                anim.SetBool("Walk", false);
                
            }

        }

        if (player.gameObject.transform.position.x < transform.position.x)
            sr.flipX = false;
        else
            sr.flipX = true;

        if (gameObject.CompareTag("Ghost"))
        {
            if (player.gameObject.transform.position.x < transform.position.x)
                sr.flipX = true;
            else
                sr.flipX = false;

        }
   
    }





    public void AttackPlayer()
    {   
       
        if (Time.time > attackTimer )
        {
            
           Debug.Log("AttackPlayer");
           player.currentHealth -= damage;

            attackTimer = Time.time + attackTimerThreshold;

            if (player.currentHealth <= 0)
                GameManager.gameManagerInstance.DestroyPlayer();
           
        }

    }

}

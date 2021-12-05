using UnityEngine;

public class Enemy : MonoBehaviour
{
    private string currentState = "IdleState";
    private Transform target;

    public float chaseRange = 5;
    public float attackRange = 2;
    public float speed = 3;

    public int health;
    public int maxHealth;

    public Animator animator;
    public CharacterController controller;
    public float stunnedtime =1.2f;
    float cooldown = 2f;
    public float timetowaitdestun = 0;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
    }
    Vector3 lastpos = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.gameOver)
        {
            animator.enabled = false;
            this.enabled = false;
        }
        if (timetowaitdestun >= stunnedtime)
        {
            if (animator.GetBool("stun"))
            {
                animator.SetBool("stun", false);
                isstunned = false;
                timetowaitdestun = cooldown;
            }
        }

        if (isstunned)
        {
            timetowaitdestun += Time.deltaTime;
            return;
        }
        else if (timetowaitdestun != 0)
        {
            timetowaitdestun -= Time.deltaTime;
        }
        
        if (timetowaitdestun < 0)
            timetowaitdestun = 0;

        float distance = Vector3.Distance(transform.position, target.position);

        if(currentState == "IdleState")
        {
            animator.applyRootMotion = false;
            if (distance < chaseRange)
                currentState = "ChaseState";
        }
        else if(currentState == "ChaseState")
        {
            animator.applyRootMotion = false;
            //transform.localPosition = pos;
            //play the run animation
            animator.SetTrigger("chase");
            animator.SetBool("isAttacking", false);

            if(distance < attackRange)
                currentState = "AttackState";

            //move towards the player
            if(target.position.x > transform.position.x)
            {
                //move right
                controller.Move(-transform.right * speed * Time.deltaTime);
                // transform.Translate(transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                //move left
                controller.Move(-transform.right * speed * Time.deltaTime);
                //transform.Translate(-transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.identity;
            }
        }
        else if(currentState == "AttackState")
        {
            //animator.applyRootMotion = true;
            animator.SetBool("isAttacking", true);

            if (distance > attackRange)
                currentState = "ChaseState";
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (!isstunned)
            currentState = "ChaseState";

        if (health < 0)
        {
            Die();
        }
    }
    public bool isstunned = false;
    public void Stun()
    {
        if (isstunned || timetowaitdestun!=0)
            return;
        isstunned = true;
        animator.SetBool("stun",true);
        Debug.Log("stun "+ timetowaitdestun);
    }
    private void Die()
    {
        PlayerManager.instancePlayerManager.AddCount();
        animator.applyRootMotion = true;
        //play a die animation
        animator.SetTrigger("isDead");
        //disable the script and the collider
        GetComponent<CapsuleCollider>().enabled = false;
        controller.enabled = false;
        Destroy(gameObject, 3);
        this.enabled = false;
    }
}

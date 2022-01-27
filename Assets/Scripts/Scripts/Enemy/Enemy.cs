using UnityEngine;
using UnityEngine.UI;

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
     float axis = -1f;
    public float timetowaitdestun = 0;
    private EnemyAttacks attacks;

    [SerializeField]
    private Slider healthBar;


    void Start()
    {
        attacks =gameObject.GetComponentInChildren<EnemyAttacks>(true);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        axis = target.GetComponent<MoveByAnimation>().axis;
        health = maxHealth;
        AudioManager.instance.Play3DSound(gameObject,"IdleEnemy");

        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }
    // Update is called once per frame
    void Update()
    {
          healthBar.value = health;

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

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Mutant Attack"))
        {
            currentState = "ChaseState";
            return;
        }

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
            animator.applyRootMotion = true;
            animator.SetBool("isAttacking", true);

            if (distance > attackRange)
                currentState = "ChaseState";
        }
    }
    public void LateUpdate()
    {
        Vector3 newp = transform.position;
        newp.z = axis;
        transform.position = newp;
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Mutant Attack"))
        {
            if(attacks.attackzone.enabled)
                attacks.attackzone.enabled = false;
            transform.GetChild(0).localPosition = new Vector3(0, -0.93f, 0);
            transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (!isstunned && currentState!= "AttackState")
            currentState = "ChaseState";

        if (health < 0)
        {
            AudioManager.instance.Play("DeathEnemy");
            healthBar.gameObject.SetActive(false);
            Die();
        }
    }
    public bool isstunned = false;
    public void Stun()
    {
        if (isstunned || timetowaitdestun!=0)
            return;
        AudioManager.instance.Play("StunEnemy");
        attacks.attackzone.enabled = false;
        isstunned = true;
        animator.SetBool("stun",true);
        Debug.Log("stun "+ timetowaitdestun);
    }
    private void Die()
    {
        InventoryManager.Instance.ModifyInventory(Item.kill,true,1);
        //PlayerManager.Instance.AddCount();
        //GameManager.gameManagerInstance.AddCount();
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

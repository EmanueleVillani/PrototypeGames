using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemy : MonoBehaviour
{
 
    private string current_State;


    public bool canMove;

    private float timerAttackFly;
    private float attackTimeFlyThreshold = 2f;   //regola l'attacco mentre l'enemy vola

    [SerializeField]
    private float timerAttack = 5f;  //Durata dell'attacco della mosquito prima di ritirarsi

    [SerializeField]
    private float timerPause = 10f; //Durata del ritiro prima di riattaccare

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

    [SerializeField]
    private float attackDistance = 20f; //distanza mosquito-player quando attacca

    [SerializeField]
    private float idleDistance = 30f;  // distanza mosquito-player quando la mosquito si ritira

    public bool isDead;
    
    private void Start()
    {
        healthBar.maxValue = enemyHealth;
        healthBar.value = enemyHealth;
        target = GameObject.FindWithTag("Player").transform;
        transform.rotation = Quaternion.Euler(0, 90, 0);
        canMove = true;
        yPosition = transform.position.y;

        current_State = "PreAttack";
        Invoke("StartFly", 1f);

    }


    private void Update()
    {
        distanceTarget = Vector3.Distance(transform.position, target.position);
        healthBar.value = enemyHealth;


        if (!isFlying)
        {
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
            current_State = "PreAttack";
        }

      
        if (current_State == "PreAttack")  //Appena parte il livello la mosquito inizia a librarsi in aria
        {
            if (isFlying)
            {                            
                current_State = "AttackState";
                StartCoroutine(_ChangeState());
            }
        } 
        
        else if (current_State == "AttackState")
        {
                 AttackAir();              
        }

        else if (current_State == "PauseAttack")
        {
            MoveEnemy();
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
        if (Vector3.Distance(transform.position, target.position) < idleDistance)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);     
    }
    
    void AttackAir()
    {
        if (Vector3.Distance(transform.position, target.position) < attackDistance)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        else if(Vector3.Distance(transform.position, target.position) > attackDistance+3f)
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);

        if (Time.time > timerAttackFly)
        {
            
          timerAttackFly = Time.time + attackTimeFlyThreshold;
          GetComponentInChildren<MosquitoAnimation>().Stun();
          anim.SetTrigger("attackFly");

        }
    }


    void StartFly()
    {
        anim.SetTrigger("flyStart");
    }
   

    IEnumerator _ChangeState()
    {
        if (current_State == "AttackState")
        {
            yield return new WaitForSeconds(timerAttack);
            current_State = "PauseAttack";

        }
        else if (current_State == "PauseAttack")
        {
            yield return new WaitForSeconds(timerPause);
            current_State = "AttackState";
        }

        StartCoroutine(_ChangeState());
    }


   
}

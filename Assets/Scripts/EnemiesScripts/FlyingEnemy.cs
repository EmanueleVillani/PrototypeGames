using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemy : MonoBehaviour
{
    public string current_State;
    public bool canMove;
    public Vector3 attackDirection;
    private float timerAttackFly;
    private float attackTimeFlyThreshold = 2f;   //regola l'attacco mentre l'enemy vola
    Vector3[] frustumCorners = new Vector3[4];

    [SerializeField]
    private float timerAttack = 5f;  //Durata dell'attacco della mosquito prima di ritirarsi

    [SerializeField]
    private float timerPause = 10f; //Durata del ritiro prima di riattaccare

    private Transform target;

   // private float distanceTarget;

    [SerializeField]
    private float distancePauseAttack = 22f;

    [SerializeField]
    private Animator anim;
 
    public float speed = 10f;

    private float yPosition;  //fix della posizione dell'enemy lungo l'asse y
 
    public bool isFlying;
   
    public int enemyHealth = 100;

    [SerializeField]
    private Slider healthBar;

    [SerializeField]
    private float attackDistance = 20f; //distanza mosquito-player quando attacca

    private float minAttackDistance = 8f; 
    public bool isDead;
    
    private void Start()
    {
        healthBar.maxValue = enemyHealth;
        healthBar.value = enemyHealth;
        target = GameObject.FindWithTag("Player").transform;
        canMove = true;
        yPosition = transform.position.y;

        current_State = "PauseAttack";
        Invoke("StartFly", 1f);

        Camera.main.CalculateFrustumCorners(new Rect(0, 0, 1, 1), Camera.main.transform.position.z - (-1.3f), Camera.MonoOrStereoscopicEye.Mono, frustumCorners);
        for (int i = 0; i < 4; i++)
        {
            frustumCorners[i] = Camera.main.transform.TransformVector(frustumCorners[i]);
        }

    }


    private void Update()
    {
        //distanceTarget = Vector3.Distance(transform.position, target.position);
        healthBar.value = enemyHealth;
        if (enemyHealth <= 0)
            return;

        if (!isFlying)
        {
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
            current_State = "PauseAttack";
        }
        else
        {
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
                GoToAttackZone();
            }
            else if (current_State == "PauseAttack")
            {
                MoveEnemy();
            }
        }
    }


    public void TakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;

        if (enemyHealth <= 0)
        {
            PlayerManager.Instance.inv = true;
            isDead = true;
            InventoryManager.Instance.ModifyInventory(Item.kill, true, 1);
            //PlayerManager.Instance.AddCount();
            AudioManager.instance.Play("MosquitoShot");
            gameObject.GetComponentInChildren<Animator>().SetTrigger("Death");
            anim.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            healthBar.gameObject.SetActive(false);
            anim.applyRootMotion = false;
            GetComponentInChildren<Rigidbody>().isKinematic = false;
            GetComponentInChildren<Rigidbody>().useGravity = true;
            StartCoroutine(EndLevel());
        }
    }
    public IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(1f);
        anim.enabled = false;
        yield return new WaitForSeconds(3f);
        FindObjectOfType<SpawnLv2>().gameObject.SetActive(false);
        GameManager.Instance.gameOver = true;
        AudioManager.instance.Stop("LifeUnder");
        GameManager.Instance.LoadEnd();
        Destroy(gameObject);
    }
    void MoveEnemy()
    {
        if (anim.GetBool("attackFly"))
            anim.SetBool("attackFly", false);
        
        point = Vector3.zero;
        if (Vector3.Distance(transform.position, target.position) < distancePauseAttack)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    Vector3 point = Vector3.zero;
    public void GoToAttackZone()
    {
        // if(anim.applyRootMotion)
        // anim.applyRootMotion = false;
        if(!attack)
            anim.gameObject.transform.LookAt(target, Vector3.up);

        if (point == Vector3.zero)
        {
            point = CalculateRandomPoint();
            //point = target.transform.position + Vector3.up * 2f + (Vector3)Random.insideUnitCircle * 3;
        }

        Debug.DrawLine(transform.position, point);

        if (Vector3.Distance(transform.position, point) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);
        }
        else
        {
            if (!attack)
            {
                attack = true;
                attackDirection =(target.transform.position + Vector3.up ) - GetComponentInChildren<AttackFlyScript>().ballAttackStartPoint.position ;
               // Debug.DrawLine( GetComponentInChildren<AttackFlyScript>().ballAttackStartPoint.position, target.transform.position + Vector3.up);
               // Debug.Break();
                StartCoroutine(WaitAndAttack());
            }
        }
    }
    bool attack = false;
    public IEnumerator WaitAndAttack()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.Play("MosquitoAttack");
        if (!anim.GetBool("attackFly"))
            anim.SetBool("attackFly", true);
        yield return new WaitForSeconds(0.5f);
        attack = false;
        current_State = "PauseAttack";
    }
    void AttackAir()
    {
        anim.SetBool("attackFly", true);
        timerAttackFly = Time.time + attackTimeFlyThreshold;

        GetComponentInChildren<MosquitoAnimation>().Stun();

        if (attackDistance < minAttackDistance)
            attackDistance = minAttackDistance;
    }


    void StartFly()
    {
        anim.SetTrigger("flyStart");
        StartCoroutine(_ChangeState());
    }
   

    IEnumerator _ChangeState()
    {
        while (true)
        {
            if (current_State == "PauseAttack")
            {
                yield return new WaitForSeconds(timerPause);
                current_State = "AttackState";
                attackDistance--;
            }

            yield return null;
        }
    }
    private void OnDrawGizmos()
    {
        //point= CalculateRandomPoint();
        if (point != Vector3.zero)
            Gizmos.DrawSphere(point, 1);
    }
    private Vector3 CalculateRandomPoint()
    {
       // if (!Application.isPlaying)
       //     return;
        Camera.main.CalculateFrustumCorners(new Rect(0, 0, 1, 1), Camera.main.transform.position.z - (-1.3f), Camera.MonoOrStereoscopicEye.Mono, frustumCorners);
        float xmax = -1000, xmin = 1000, z = 0, ymin = 1000, ymax = -1000;
        for (int i = 0; i < 4; i++)
        {
            frustumCorners[i] = Camera.main.transform.TransformVector(frustumCorners[i]);
            Debug.DrawRay(Camera.main.transform.position, frustumCorners[i], Color.red);
        }
        z = frustumCorners[0].z;
        for (int i = 0; i < 4; i++)
        {
            if (frustumCorners[i].x < xmin)
                xmin = frustumCorners[i].x;
            if (frustumCorners[i].x > xmax)
                xmax = frustumCorners[i].x;
            if (frustumCorners[i].y < ymin)
                ymin = frustumCorners[i].y;
            if (frustumCorners[i].y > ymax)
                ymax = frustumCorners[i].y;
        }
        float yl = ymax - ymin;
        ymax -= yl / 5;
        ymin += yl / 2;
        float x = Random.Range(xmin, xmax);
        float y = Random.Range(ymin, ymax);
        return Camera.main.transform.position + new Vector3(x, y, z);
    }


}

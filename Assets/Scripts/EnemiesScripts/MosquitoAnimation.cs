using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoAnimation : MonoBehaviour
{
    private Animator anim;
    //private EnemyHealth health;
    private FlyingEnemy flyingEnemy;
    private float animSpeed=15f;

    private float timeStun = 5;
    private bool isStunning;
    private float minHighAnimation = 3f, maxHighAnimation = 6f;
    public float rotationSpeed = 5f;

    
    private void Start()
    {
        anim = GetComponent<Animator>();
       // health = GetComponent<EnemyHealth>();
        flyingEnemy = GetComponentInParent<FlyingEnemy>();
    }

    private void Update()
    {
        if (flyingEnemy.isDead)
        {
            transform.Translate(Vector3.down * Time.deltaTime * animSpeed);

            if (transform.position.y < 0)
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        }

        if (flyingEnemy.isFlying)
        {
            if (!GetComponentInParent<FlyingEnemy>().isDead)
            {
                if (transform.position.y < minHighAnimation)
                    transform.position = new Vector3(transform.position.x, minHighAnimation, transform.position.z);

                if (transform.position.y > maxHighAnimation)
                    transform.position = new Vector3(transform.position.x, maxHighAnimation, transform.position.z);

            }
        }
        
    }

    public void Stun()
    {
        if (!isStunning)
        {
             isStunning = true;
             GetComponentInParent<FlyingEnemy>().canMove = false;
             anim.SetBool("Stun", true);
            StartCoroutine(_ReturnNormal(timeStun));
        }

    }

    IEnumerator _ReturnNormal(float timerStun)
    {
        yield return new WaitForSeconds(timerStun);

        anim.SetBool("Stun", false);
        GetComponentInParent<FlyingEnemy>().canMove = true;

        isStunning = false;
    }

}




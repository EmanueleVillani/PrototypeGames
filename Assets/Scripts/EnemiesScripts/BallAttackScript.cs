using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttackScript : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 30;

    [SerializeField]
    private float speedBall = 100;

    private Vector3 target;

    private Rigidbody myBody;

    private float offset = 2f;

    

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform.position;

        myBody = GetComponent<Rigidbody>();

        Vector3 direction = target - transform.position + new Vector3(0f,offset,0f) ;

        myBody.velocity = direction.normalized * speedBall;

        Destroy(gameObject, 1f);

    }

    private void Update()
    {

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")  || other.CompareTag("Ground"))
        {

            PlayerManager.currentHealth -= damageAmount;
            Destroy(gameObject);

        }

        
    }

   


}

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

    public void Shoot(Vector3 direction)
    {
        //target = GameObject.FindWithTag("Player").transform.position;
        //Vector3 direction = target - transform.position + new Vector3(0f, offset, 0f);
        myBody = GetComponent<Rigidbody>();
        myBody.velocity = direction.normalized * speedBall;
        Destroy(gameObject, 3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")  || other.CompareTag("Ground"))
        {
            PlayerManager.Instance.currentHealth -= damageAmount;
            Destroy(gameObject);
        }
    }
}

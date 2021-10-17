using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    public float healt;
    protected Rigidbody2D rb;
    protected Animator anim;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(-1, rb.velocity.y);
    }
    public void TakeDamage(float damage)
    {
        healt -= damage;
        if(healt <= 0)
        {
            Destroy(gameObject);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour

{
    public float speed;
    public float jumpForce;

    //INIZIALIZZIAMO LE VARIABILI

    private GatherInput gI;
    private Rigidbody2D rb;
    private Animator anim;

    // var per la direzione del player effetto Mirow
    private int direction = 1;


    public float rayLength; 
    public LayerMask groundLayer;

    // variabile per ancorare il player alla piattaforma senza sbavature
    public Transform leftPoint;
    public Transform rightPoint;

    private bool grounded = true;



    // Start is called before the first frame update
    void Start()
    {
        // ASSEGNAZIONE VARIABILI 
        gI = GetComponent<GatherInput>();

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>(); // ANIMATOR 
        
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorValues();
    }

    private void FixedUpdate()
    {
        CheckStatus();
        Move();
        jumpPlayer();
        
    }


    private void Move()
        
    {
        flip();
        rb.velocity = new Vector2(speed * gI.valueX, rb.velocity.y);
    }

    private void jumpPlayer()
    {
        if (gI.jumpInput)
        {
            if (grounded)
            {
                rb.velocity = new Vector2(gI.valueX * speed, jumpForce);
            }
            
        }
        gI.jumpInput = false;
    }

    // Func per ancorare il player alla piattaforma
    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightCheckHit = Physics2D.Raycast(rightPoint.position, Vector2.down, rayLength, groundLayer);

        if (leftCheckHit || rightCheckHit)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        SeeRays(leftCheckHit, rightCheckHit);
    }


    private void SeeRays(RaycastHit2D leftCheckHit, RaycastHit2D rightCheckHit)
    {
        Color color1 = leftCheckHit ? Color.red : Color.green;

        Color color2 = rightCheckHit ? Color.red : Color.green;


        Debug.DrawRay(leftPoint.position, Vector2.down * rayLength, color1);
    }

    // Func la direzione del plyer destra e sinistra  -1 = sinistra / +1 = destra
    private void flip()
    {
        if(gI.valueX*direction < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            direction *= -1;
        }
    }



    private void SetAnimatorValues()
    {
        anim.SetFloat("Speed",Mathf.Abs(rb.velocity.x));
        anim.SetFloat("vSpeed", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
    }
}

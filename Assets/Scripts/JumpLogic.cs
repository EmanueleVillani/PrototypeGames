using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLogic : MonoBehaviour
{
    CharacterController controller;
    private Animator animator;
    Vector3 direction=Vector3.down;
    public float gravity = 20f;
    public float jumpforce = 9f;
    public GameObject feet1,feet2;
    public LayerMask groundLayer;
    GatherInput gI;
    private float hInput;
    public bool isGrounded;
    public float initialForceJump = 5;
    public float physicsRange = 0.25f;
    public bool flip = false;
    float delta = 0;
    // Start is called before the first frame update
    void Start()
    {
        gI = GetComponent<GatherInput>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        delta = transform.position.y + controller.center.y - Mathf.Min(feet1.transform.position.y,feet2.transform.position.y);
        isGrounded = Physics.CheckSphere(transform.position + controller.center - Vector3.up*(delta), physicsRange, groundLayer);
        if (flip)
            hInput = -gI.valueX;
        else
            hInput = gI.valueX;

        if (isGrounded)
        {
            animator.SetBool("isGrounded", isGrounded);

            controller.height = 1.7f;
            direction.x = 0;
            direction.y = -1;
            // ableToMakeADoubleJump = true;
            // if (Input.GetButtonDown("Jump"))
            //if (Input.GetKeyDown(KeyCode.Space))
            if (gI.jumpInput)
            {
                direction.x = hInput* initialForceJump;
                animator.SetBool("isGrounded", false);
                direction.y = jumpforce; 
                controller.Move(direction * 2 * Time.deltaTime);
            }
            //if (Input.GetKeyDown(KeyCode.F))
            // if (gI.fireInput)
            // {
            //     animator.SetTrigger("fireBallAttack");
            // }
        }
        else
        {
          //controller.height = 1.7f;
          direction.x = hInput * initialForceJump;
          direction.y += gravity * Time.deltaTime;//Add Gravity
          //if (ableToMakeADoubleJump && Input.GetButtonDown("Jump"))
          //if (ableToMakeADoubleJump && gI.jumpInput)
          //{
          //    DoubleJump();
          //}
        }

        controller.height = delta * 2;
        controller.Move(direction*Time.deltaTime);
    }

    public void OnDrawGizmos()
    {
        if(controller!=null)
        Gizmos.DrawSphere(transform.position + controller.center - Vector3.up * (delta),0.2f);
    }
}



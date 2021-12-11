using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;
    public float speed = 8;

    public float jumpForce = 10;
    public float gravity = -20;
    public Transform groundCheck;
    public LayerMask groundLayer;


    public bool ableToMakeADoubleJump = true;

    public Animator animator;
    public Transform model;

    private GatherInput gI;
    private float startZ;
    public bool isGrounded;
    private void Start()
    {
        startZ = transform.position.z;
        gI = GetComponent<GatherInput>();
    }
    void Update()
    {
        if (PlayerManager.gameOver)
        {
            //play death animation
            animator.SetTrigger("die");

            //disable the script
            this.enabled = false;
        }

        //Take the horizontal input to move the player
       float hInput = Input.GetAxis("Horizontal");
       //float hInput = gI.valueX;
       //direction.x = hInput * speed;
       //animator.SetFloat("speed", Mathf.Abs(hInput));

        //Check if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded)
        {
            controller.height = 1.7f;
            direction.x = hInput * speed;
            direction.y = -1;
            ableToMakeADoubleJump = true;
           // if (Input.GetButtonDown("Jump"))
            if (gI.jumpInput)
            {
                Jump();
                controller.Move(direction*2*Time.deltaTime);
            }
            //if (Input.GetKeyDown(KeyCode.F))
           // if (gI.fireInput)
           // {
           //     animator.SetTrigger("fireBallAttack");
           // }
        }
        else
        {
            controller.height = 1.7f;
            direction.x = hInput * speed;
            direction.y += gravity * Time.deltaTime;//Add Gravity
            //if (ableToMakeADoubleJump && Input.GetButtonDown("Jump"))
            if (ableToMakeADoubleJump && gI.jumpInput)
            {
                DoubleJump();
            }
        }

       // if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fireball Attack"))
       //     return;

        //Flip the player
       //if(hInput != 0)
       //{
       //    Quaternion newRotation = Quaternion.LookRotation(new Vector3(hInput, 0, 0));
       //    model.rotation = newRotation;
       //}

        //Move the player using the character controller
       // direction.x = 0;
       if(!isGrounded)//airborne movement
        controller.Move(direction * Time.deltaTime);

        //Reset Z Position
      // if (transform.position.z != startZ)
      //     transform.position = new Vector3(transform.position.x, transform.position.y, startZ);

        //win level
        if (PlayerManager.winLevel)
        {
            animator.SetTrigger("win");
            this.enabled = false;
        }
    }

    private void DoubleJump()
    {
        //Double Jump
        animator.SetTrigger("doubleJump");
        direction.y = jumpForce;
        ableToMakeADoubleJump = false;
    }
    private void Jump()
    {
        //Jump
        direction.y = jumpForce;
    }
}

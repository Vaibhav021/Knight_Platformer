using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Components
    private Rigidbody2D rb;
    private Animator anim;

    //bool    
    public bool onGround;    
    public bool wallGrab = false;
    private bool jumping = false;

    //Speed & Force
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float slideSpeed = 5f;

    //Key_Input
    private float moveInput;
    
    //Jump Gravity
    public float fallMultiplier = 5f;
    public float lowJumpMultiplier = 3f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //starting position
        //Vector2 temp = transform.position;
        //temp.x = -8f;
        //temp.y = -5.5f;
        //transform.position = temp;

    }

    // Update is called once per frame
    void Update()
    {      

        moveInput = Input.GetAxis("Horizontal");
        if(!SceneManage.isWin)
        {
            Move(moveInput);

            Jump();
        }
              

        
        
    }//update

    public void Move(float dir)
    {
        if(dir != 0)
            rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(0f, rb.velocity.y);

        if(!onGround)
            rb.velocity = new Vector2(dir * (moveSpeed - 1), rb.velocity.y);

        
        //Flip and Animate
        if (moveInput > 0)
        {
            Vector2 temp = transform.localScale;
            if (temp.x < 0)
                temp.x *= -1;
            transform.localScale = temp;

            //Anim
            anim.SetBool("Walk", true);

        }
        else if (moveInput < 0)
        {
            Vector2 temp = transform.localScale;
            if (temp.x > 0)
                temp.x *= -1;
            transform.localScale = temp;

            //Anim
            anim.SetBool("Walk", true);

        }
        else
        {
            //Anim
            anim.SetBool("Walk", false);
        }

    }

    public void Jump()
    {
        //Make Jump
        if (Input.GetAxisRaw("Vertical") > 0)
        {          
            if(onGround)
            {
                jumping = true;
                rb.velocity += Vector2.up * jumpForce;
            }                     
			else
			{
				jumping = false;
			}

        }


        //Jump animation Check
		if (Input.GetAxisRaw("Vertical") > 0)
        {        
            if (onGround)
            {
                anim.SetTrigger("TakeOff");                
            }
        }
        if (onGround)
        {
            //jumping = false;
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

        //Jump Gravity
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !jumping)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }

          
    























}//class

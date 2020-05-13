using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAudioHandler hurtAudio;

    public float hitForceX;
    public float hitFotceY;

    //private float dazeTime;
    //public float setDazeTime;

    public float health;
    public float numOfHearts;

    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Start is called before the first frame update
    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        hurtAudio = GetComponent<PlayerAudioHandler>();

        health = numOfHearts;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // show either FULL or EMPTY heart
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            //show total number of hearts
            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        ////Daze Time
        //if (dazeTime > 0)
        //    dazeTime -= Time.deltaTime;
        
    }    

    public void PlayerHitForce()
    {
        if(transform.localScale.x > 0)
        {
            if (hitForceX > 0)
                hitForceX *= -1;
        }

        else if (transform.localScale.x < 0)
        {
            if (hitForceX < 0)
                hitForceX *= -1;
        }

        rb.AddForce (new Vector2(hitForceX, hitFotceY));


    }    

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //dazeTime = setDazeTime;
            health -= 1f;
            hurtAudio.PlayHurtSound();

            if (health > 0)
                PlayerHitForce();
        }
    }

    //public void OnCollisionStay2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Enemy")
    //    {
    //        if (dazeTime <= 0)
    //            health -= 1f;

    //        dazeTime = setDazeTime;
    //    }
    //}















}//class

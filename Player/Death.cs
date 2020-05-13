using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private Health playerHealth;
    private PlayerMovement playerMove;

    private SceneManage sceneManager;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerHealth = GetComponent<Health>();
        playerMove = GetComponent<PlayerMovement>();
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManage>();               

        playerMove.enabled = true;
        gameObject.SetActive(true);

        anim.SetBool("isDead", false);

    }

    // Update is called once per frame
    void Update()
    {
        if(sceneManager.isDead == true)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        //Death
        if (playerHealth.health == 0)
        {
            PlayerIsDead();
        }

    }

    public void PlayerIsDead()
    {
        Invoke("DeadPanel", 3f);
        playerMove.enabled = false;        
        anim.SetBool("isDead", true);
    }

    public void DeadPanel()
    {
        sceneManager.isDead = true;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "DeadEnd")
        {
            sceneManager.isDead = true;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WinLevel")
        {
            SceneManage.isWin = true;
            gameObject.SetActive(false);
        }
    }














}//class

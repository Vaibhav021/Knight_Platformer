using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryCube : MonoBehaviour
{
    public float setSpeed = 2f;

    [HideInInspector]
    public float speed;

    public GameObject hitParticle;

    private bool movingRight = true;

    private EnemyHealth health;
    private float healthCheck;

    private Vector2 trans;

    [Header("Detectors")]
    public Transform groundDetector;
    public Transform wallDetector;

    [Header("Wall Detector Data")]
    public float colliderRadius;
    public LayerMask setLayer;
    private bool touchWall;

    [Header("Player Detection Ray")]
    public float rayLength;


    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealth>();

        speed = setSpeed;

        trans = transform.position;

        healthCheck = health.health;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.right * speed * Time.deltaTime);
        if(movingRight == true)
            trans.x += speed * Time.deltaTime;
        else
            trans.x -= speed * Time.deltaTime;


        transform.position = trans;
        
        GroundCheck();
        WallCheck();
        PlayerCheck();

        if (health.health == 0)
        {
            Destroy(gameObject);
        }

        if(healthCheck > health.health)
        {
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            FlipIt();
            healthCheck = health.health;
        }

    }

    public void GroundCheck()
    {
        RaycastHit2D groundHitCheck = Physics2D.Raycast(groundDetector.position, Vector2.down, 1f);

        if (groundHitCheck.collider == false)
        {
            FlipIt();

        }
    }

    public void WallCheck()
    {
        touchWall = Physics2D.OverlapCircle(wallDetector.position, colliderRadius, setLayer);

        if (touchWall == true)
        {
            FlipIt();            
        }

    }

    public void PlayerCheck()
    {
        RaycastHit2D playerHitCheckLeft = Physics2D.Raycast(wallDetector.position, Vector2.left, rayLength);
        RaycastHit2D playerHitCheckRight = Physics2D.Raycast(wallDetector.position, Vector2.right, rayLength);

        if (playerHitCheckLeft.collider.gameObject.tag == "Player" || playerHitCheckRight.collider.gameObject.tag == "Player")
        {            
            speed = setSpeed + 3;            
        }
        else
        {
            speed = setSpeed;
        }

    }

    public void FlipIt()
    {
        Vector2 temp;
        temp = transform.localScale;

        if (movingRight == true)
        {
            
            temp.x = -1;
            transform.localScale = temp;
            //transform.eulerAngles = new Vector3(0f, 180f, 0f);
            movingRight = false;
        }
        else
        {
            temp.x = 1;
            transform.localScale = temp;
            //transform.eulerAngles = new Vector3(0f, 0f, 0f);
            movingRight = true;
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            FlipIt(); 
        }
                   
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(wallDetector.position, colliderRadius);

        //Raycast
        Vector3 directionRight = wallDetector.TransformDirection(Vector2.right) * rayLength;
        Gizmos.DrawRay(wallDetector.position, directionRight);
        Vector3 directionLeft = wallDetector.TransformDirection(Vector2.left) * rayLength;
        Gizmos.DrawRay(wallDetector.position, directionLeft);

    }
























}//class

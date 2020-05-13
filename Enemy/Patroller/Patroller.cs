using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    public float speed = 2f;    

    private bool movingRight = true;

    public GameObject deadParticle;

    private EnemyHealth Health;

    [Header("Detectors")]
    public Transform groundDetector;
    public Transform wallDetector;

    [Header("Wall Detector Data")]    
    public float colliderRadius;
    public LayerMask setLayer;
    private bool touchWall;


    // Start is called before the first frame update
    void Start()
    {
        Health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        GroundCheck();
        WallCheck();

        if (Health.health == 0)
        {
            Instantiate(deadParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
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

        if(touchWall == true)
        {
            FlipIt();            
        }
            
                
    }

    public void FlipIt()
    {
        if (movingRight == true)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            movingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            movingRight = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(deadParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(wallDetector.position, colliderRadius);

    }

















}//class

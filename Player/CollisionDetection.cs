using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset;
    public LayerMask groundLayer;

    private PlayerMovement playerMove;

    

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMove.onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);        

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
                
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);        

    }







}

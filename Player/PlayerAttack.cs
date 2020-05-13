using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private CameraShake camShake;
    private AudioSource hitAudio;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public float attackRadius;
    public Transform attackPos;
    public LayerMask EnemyLayer;

    private int attackPower = 1;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        camShake = GetComponent<CameraShake>();
        hitAudio = GetComponent<AudioSource>();

        timeBtwAttack = startTimeBtwAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("Attack");                

                timeBtwAttack = startTimeBtwAttack;
            }

        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

    }

    public void Attack()
    {
        CameraShake();

        Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRadius, EnemyLayer);

        for (int i = 0; i < enemyToDamage.Length; i++)
        {
            enemyToDamage[i].GetComponent<EnemyHealth>().TakeDamage(attackPower);
        }
    }

    public void CameraShake()
    {
        camShake.ShakeIt();
    }

    public void PlayHitAudio()
    {
        hitAudio.Play();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRadius);

    }







}

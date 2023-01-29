using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossai : MonoBehaviour
{
    //public Vector2 pos1;
    public Transform pos1;
    public Transform pos2;

    //public Vector2 pos2;


    //public float leftrightspeed;
    private float oldposition;
    public float distance;
    private Animator animator;
    private Transform target;
    public float followspeed;
    public bosscombat bosscombat;


    public GameObject player;
    private Transform playerPos;
    private Vector2 currentPos;
    public float speedenemy;
    private Rigidbody2D rb;


    bool canjump = true;
    public float jumpspeed = 5;
    public float walkSpeed = 4;



    public float minAttackCooldown = 0.5f;
    public float maxAttackCooldown = 2f;
    public float aiCooldown;
    private bool isAttacking;
    private Vector2 positionTarget;

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        target = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        bosscombat = GetComponent<bosscombat>();

        playerPos = player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();


    }


    void Update()
    {
        // transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speedenemy, 1.0f));

        // if (transform.position.x > oldposition)
        // {
        //     transform.localRotation = Quaternion.Euler(0, 180, 0);

        // }
        // if (transform.position.x < oldposition)
        // {
        //     transform.localRotation = Quaternion.Euler(0, 0, 0);
        // }
        // oldposition = transform.position.x;
        enemyai();
    }

    void enemyai()
    {

        RaycastHit2D hitenemy = Physics2D.Raycast(transform.position, -transform.right, distance);

        if (Vector2.Distance(transform.position, playerPos.position) < distance)
        {
            //transform.position = Vector2.MoveTowards(transform.position, playerPos.position, followspeed * Time.deltaTime);
            float moveInput = Input.GetAxisRaw("Horizontal");

            rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, walkSpeed * Time.deltaTime);
            animator.SetBool("isWalking", true);

            if (Vector2.Distance(transform.position, playerPos.position) <= 10)
            {
                animator.SetBool("isattack", true);

                rb.AddForce(Vector2.up * jumpspeed);
                bosscombat.DamagePlayer();
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, followspeed * Time.deltaTime);
                rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
                canjump = false;

            }
            animator.SetBool("isattack", false);


            aiCooldown -= Time.deltaTime;

            if (aiCooldown <= 0f)
            {
                //float moveInput = Input.GetAxisRaw("Horizontal");

                rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, jumpspeed * Time.deltaTime);
                animator.SetBool("isattack", true);

                isAttacking = !isAttacking;
                aiCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                positionTarget = Vector2.zero;

                animator.SetBool("isattack", isAttacking);
            }
            if (isAttacking)
            {
                animator.SetBool("isattack", true);
                rb.AddForce(Vector2.up * jumpspeed);
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, jumpspeed * Time.deltaTime);
                rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
                //canjump = false;

            }

            else
            {
                if (positionTarget == Vector2.zero)
                {
                    Vector2 randomPoint = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

                    positionTarget = Camera.main.ViewportToWorldPoint(randomPoint);
                }
                if (GetComponent<Collider2D>().OverlapPoint(positionTarget))
                {

                    positionTarget = Vector2.zero;
                }

            }

            animator.SetBool("isWalking", false);
        }
        else
        {
            //animator.SetBool("isWalking", false);

            if (Vector2.Distance(transform.position, currentPos) <= 0)
            {

            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, currentPos, followspeed * Time.deltaTime);
            }
        }
    }
}


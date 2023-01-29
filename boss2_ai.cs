using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2_ai : MonoBehaviour
{
    public Vector2 pos1;
    public Vector2 pos2;


    //public float leftrightspeed;
    private float oldposition;
    public float distance;
    private Animator animator;
    private Transform target;
    public float followspeed;
    public boss2_combat boss2combat;


    public GameObject player;
    private Transform playerPos;
    private Vector2 currentPos;

    public float speedenemy;
    public float smalldistance;
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        target = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        boss2combat = GetComponent<boss2_combat>();

        playerPos = player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;

    }


    void Update()
    {
        //transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speedenemy, 1.0f));

        if (transform.position.x > oldposition)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);

        }
        if (transform.position.x < oldposition)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        oldposition = transform.position.x;
        enemyai();
    }
    void enemyai()
    {
        RaycastHit2D hitenemy = Physics2D.Raycast(transform.position, -transform.right, distance);
        RaycastHit2D hitenemy2 = Physics2D.Raycast(transform.position, -transform.right, smalldistance);

        if (Vector2.Distance(transform.position, playerPos.position) < distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, followspeed * Time.deltaTime);
            animator.SetBool("isAttack", true);
            //4.lvl hayaletlerin saldırı kodu
            boss2combat.DamagePlayer();

            // if (Vector2.Distance(transform.position, playerPos.position) <= smalldistance)
            // {
            //     animator.SetBool("attack2", true);
            //     transform.position = Vector2.MoveTowards(transform.position, playerPos.position, 1 * Time.deltaTime);
            //     enemycombat2.DamagePlayer();
            // }
            // animator.SetBool("attack2", false);
        }


        else
        {
            animator.SetBool("isAttack", false);

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

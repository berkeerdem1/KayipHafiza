using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onlyaistill : MonoBehaviour
{

    public Vector2 pos1;
    public Vector2 pos2;


    //public float leftrightspeed;
    private float oldposition;
    public float distance;
    private Animator animator;
    private Transform target;
    public float followspeed;
    public enemy_combat enemycombat;


    public GameObject player;
    private Transform playerPos;
    private Vector2 currentPos;

    public float speedenemy;

    public GameObject enemy;

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        target = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        enemycombat = GetComponent<enemy_combat>();

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

        if (Vector2.Distance(transform.position, playerPos.position) < distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, followspeed * Time.deltaTime);
            animator.SetBool("isAttack", true);
            //4.lvl hayaletlerin saldırı kodu
            enemycombat.DamagePlayer();
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

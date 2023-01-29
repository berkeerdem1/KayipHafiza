using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class robert_move : MonoBehaviour
{
    public int speed;
    public float faster;


    public float jumpSpeed;

    Animator animator;
    Rigidbody2D rb;

    Vector2 forward;
    public Vector3 offset;
    RaycastHit2D hit;

    bool faceRight = true;
    bool canjump = true;




    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();


    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     animator.SetBool("gunchange", true);

        // }
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     animator.SetBool("gunchange", false);
        // }


        if (moveInput > 0 || moveInput < 0)
        {
            animator.SetBool("iswalking", true);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector2(moveInput * faster, rb.velocity.y);
                animator.SetBool("isFaster", true);

            }
            else
            {
                animator.SetBool("isFaster", false);

            }
        }
        else
        {
            animator.SetBool("iswalking", false);

        }

        animator.SetBool("isJumping", false);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //canjump = true;
            jump();

            animator.SetBool("isJumping", true);
            canjump = false;
        }



        // else
        // {
        //     animator.SetBool("isJumping", false);
        // }


        if (faceRight == true && moveInput < 0)
        { //ÖNEMLi
            Flip();
        }
        else if (faceRight == false && moveInput > 0)
        {
            Flip();
        }

        Debug.Log(faceRight);
    }
    void Flip() //KARAKTERi X EKSENiNDE TERS YÖNE DÖNDÜRME
    {
        faceRight = !faceRight;
        transform.Rotate(new Vector3(0, 180, 0));
    }
    //sola döndürme
    public bool getfaceRight()
    {
        return faceRight;
    }
    private void jump()
    {
        if (canjump)
        {
            //rb.AddForce(Vector2.up * jumpSpeed);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            canjump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.transform.tag == "platform"))
        {
            canjump = true;
        }

    }
    


}



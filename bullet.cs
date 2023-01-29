using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float bulletSpeed;
    public float endTime;
    public GameObject patlama;
    //public GameObject patlama;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, endTime);

        GameObject sil = Instantiate(patlama, transform.position, transform.rotation);
        Destroy(sil, 0.100f);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            collision.GetComponent<enemy_move>().TakeDamage(25);
            collision.gameObject.SendMessage("DamagePlayer");
            Destroy(gameObject);
            Debug.Log("değdi");
        }
        if (collision.CompareTag("boss2"))
        {
            collision.GetComponent<boss2_move>().TakeDamage(20);
            Destroy(gameObject);
            Debug.Log("değdi");
        }



    }
}

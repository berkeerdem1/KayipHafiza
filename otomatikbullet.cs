using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otomatikbullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float bulletSpeed;
    public float endTime;
    public GameObject patlama;
    //public GameObject patlama;

    public int ForceAmount = 1; //geri teptirme

    public float slowdownFactor = 0.5f; //düşman hızı


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
        if (collision.CompareTag("enemy2"))
        {
            collision.GetComponent<enemy_move>().TakeDamage(15);
            Destroy(gameObject);

        }
        if (collision.CompareTag("boss"))
        {
            collision.GetComponent<bosshealth>().TakeDamage(5);
            Destroy(gameObject);

            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>(); //düşmanı geri itme
            Vector2 forceDirection = collision.transform.position - transform.position;
            enemyRb.AddForce(forceDirection * ForceAmount, ForceMode2D.Impulse);

            enemyRb.velocity = enemyRb.velocity * slowdownFactor; //düşmanın hızını azaltma
        }
        if (collision.CompareTag("telkapi"))
        {
            collision.GetComponent<telkapi>().TakeDamage(5);
            Destroy(gameObject, 0.15f);

        }


    }


    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3);

    }



}


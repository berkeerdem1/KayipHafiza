using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class boss2_move : MonoBehaviour
{
    Animator animator;
    public float maxHealth = 600;
    public float currentHealth;

    [SerializeField] Slider slider;





    void Start()
    {
        currentHealth = maxHealth;
        slider.value = currentHealth;
        animator = GetComponent<Animator>();
        //healthbar.Sethealth(currentHealth,maxHealth);


    }
    void update()
    {
        //healthbar.fillAmount = (float)maxHealth / (float)currentHealth;

    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        slider.value = currentHealth;

        animator.SetBool("ishurt", true);



        if (currentHealth <= 0)
        {
            animator.SetTrigger("isDie");
            currentHealth = 0;
            Die();
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            currentHealth -= 10;
            slider.value = currentHealth;

            animator.SetBool("ishurt", true);


            if (currentHealth <= 0)
            {
                animator.SetTrigger("isDie");
                Die();
            }
        }

    }

    void Die()
    {
        animator.SetBool("isDie", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;


        //düşman öldüğünde tüm kodlar durur
        //GetComponent<enemy_move>().enabled = false;
        GetComponent<boss2_move>().enabled = false;
        if (GetComponent<aiStill>())
            GetComponent<aiStill>().enabled = false;
        if (GetComponent<bossai>())
            GetComponent<bossai>().enabled = false;
        if (GetComponent<bosscombat>())
            GetComponent<bosscombat>().enabled = false;
        if (GetComponent<bosshealth>())
            GetComponent<bosshealth>().enabled = false;
        if (GetComponent<enemy_combat>())
            GetComponent<enemy_combat>().enabled = false;
        if (GetComponent<enemy_move>())
            GetComponent<enemy_move>().enabled = false;
        if (GetComponent<enemyAI>())
            GetComponent<enemyAI>().enabled = false;
        if (GetComponent<enemycombat2>())
            GetComponent<enemycombat2>().enabled = false;
        if (GetComponent<lvl8enemyAI>())
            GetComponent<lvl8enemyAI>().enabled = false;
        if (GetComponent<lvl8enemycombat>())
            GetComponent<lvl8enemycombat>().enabled = false;
        if (GetComponent<onlyaistill>())
            GetComponent<onlyaistill>().enabled = false;
        if (GetComponent<boss2_ai>())
            GetComponent<boss2_ai>().enabled = false;
        if (GetComponent<boss2_combat>())
            GetComponent<boss2_combat>().enabled = false;
        if (GetComponent<boss2_move>())
            GetComponent<boss2_move>().enabled = false;
        Destroy(gameObject, 1.5f);
    }
}

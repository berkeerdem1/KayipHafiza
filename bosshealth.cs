using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class bosshealth : MonoBehaviour
{
    Animator animator;
    public float maxHealth = 800;
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

        //animator.SetTrigger("isHurt");



        if (currentHealth <= 50)
        {
            animator.SetTrigger("ishurt");
            currentHealth = 50;
            StartCoroutine(waiter());
            //Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            currentHealth -= 5;
            slider.value = currentHealth;

            //animator.SetTrigger("isHurt");
            if (currentHealth <= 50)
            {
                currentHealth = 50;
                slider.value = currentHealth;
                animator.SetTrigger("ishurt");
                StartCoroutine(waiter());
                //Die();
            }
        }

    }



    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3);
        Die();
    }
    void Die()
    {
        //animator.SetBool("isDie", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;


        //düşman öldüğünde tüm kodlar durur
        //GetComponent<enemy_move>().enabled = false;
        GetComponent<bosshealth>().enabled = false;
        GetComponent<bossai>().enabled = false;


        if (GetComponent<aiStill>())
            GetComponent<aiStill>().enabled = false;
        if (GetComponent<enemyAI>())
            GetComponent<enemyAI>().enabled = false;
        if (GetComponent<enemycombat2>())
            GetComponent<enemycombat2>().enabled = false;
        if (GetComponent<enemy_combat>())
            GetComponent<enemy_combat>().enabled = false;
        if (GetComponent<lvl8enemycombat>())
            GetComponent<lvl8enemycombat>().enabled = false;
        if (GetComponent<bosscombat>())
            GetComponent<bosscombat>().enabled = false;
        if (GetComponent<bossai>())
            GetComponent<bossai>().enabled = false;
        if (GetComponent<onlyaistill>())
            GetComponent<onlyaistill>().enabled = false;
        if (GetComponent<bosshealth>())
            GetComponent<bosshealth>().enabled = false;
        if (GetComponent<otomatikbullet>())
            GetComponent<otomatikbullet>().enabled = false;
        if (GetComponent<o_boxammo>())
            GetComponent<o_boxammo>().enabled = false;
        if (GetComponent<rotomatik_move>())
            GetComponent<rotomatik_move>().enabled = false;
        //Destroy(gameObject, 1.5f);

        SceneManager.LoadScene(21);


    }
}

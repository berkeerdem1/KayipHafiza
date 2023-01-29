using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class robert_health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public healthbar healthbar;

    public bool enemyattack;
    public float enemytimer;
    public Animator animator;


    public GameObject[] medicine;
    private int medicineAmount;
    public int allmedicine = 2;

    public GameObject ilac;
    public TextMeshProUGUI canyazi;


    //public int SleepTimeout;
    void Start()
    {
        currentHealth = maxHealth;
        enemytimer = 1.5f;
        animator = GetComponent<Animator>();

        //canvasdaki ilaçların gözükmesi için
        for (int i = 0; i <= 1; i++)
        {
            medicine[i].gameObject.SetActive(true);
        }
        medicineAmount = 2;
    }
    //düşmanın zarar verme aralığı
    void enemeyAttackSpacing()
    {
        if (enemyattack == false)
        {
            enemytimer -= Time.deltaTime;
        }
        if (enemytimer < 0)
        {
            enemytimer = 0f;
        }
        if (enemytimer == 0f)
        {
            enemyattack = true;
            enemytimer = 1.5f;
        }
    }
    //düşmanı kitleme
    void characterDamage()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            enemyattack = false;
        }
    }
    //karakterin zarar görmesi
    public void TakeDamage(int damage)
    {
        if (enemyattack)
        {
            currentHealth -= 20;
            canyazi.text = "can:100/" + currentHealth;
            enemyattack = false;
        }
        healthbar.setHealth(currentHealth);


        if (currentHealth <= 0)
        {
            //animator.SetBool("isDie", true);
            animator.SetTrigger("isDie");
            Time.timeScale = 1.5f;
            //SleepTimeout = 10;
            currentHealth = 0;
            //dead();
            StartCoroutine(waiter());
        }

    }

    public void dead()
    {
        GetComponent<robert_move>().enabled = false;
        //yield  new WaitForSeconds(1);

        Destroy(gameObject, 1f);
        // if (gameObject == null)
        // {
        //     SceneManager.LoadScene(13);
        // }
        SceneManager.LoadScene(40);



    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(2);
        dead();
    }



    void Update()
    {
        enemeyAttackSpacing();
        characterDamage();

        if (
                allmedicine > 0 &&
                Input.GetKeyDown(KeyCode.X) &&
                currentHealth != 100 && allmedicine > 0
          )
        {

            currentHealth += 30;
            healthbar.setHealth(currentHealth);
            canyazi.text = "can:100/" + currentHealth;

            medicineAmount -= 1;//bir ilaç eksilt
            allmedicine -= 1;//tüm ilaçlarda bir tane eksilt
            medicine[medicineAmount].gameObject.SetActive(false);//bir ilaç objesini canvasdan kaldır
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("medicine") && currentHealth != 100)
        {
            Destroy(ilac);//yerdeki ilaç paketini yok et
            currentHealth = maxHealth;
            healthbar.setHealth(currentHealth);//can barını güncelle
            canyazi.text = "can:100/" + currentHealth;//can miktarını barda yaz

        }
    }

}

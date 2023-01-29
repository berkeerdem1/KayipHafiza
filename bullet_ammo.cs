using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_ammo : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    private Transform firepoint;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    public GameObject[] ammo;

    private int ammoAmount;
    public int allammo = 32;


    public float reloadTime = 1f;
    private bool isReloading = false;
    bool faceRight = true;



    public GameObject[] sarjor;
    private int sarjoramount;
    public int allsarjor = 4;

    Animator anim;

    public Transform casingPoint;
    public GameObject bulletCasing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        for (int i = 0; i <= 7; i++)
        {
            ammo[i].gameObject.SetActive(true);
        }
        ammoAmount = 8;

        for (int i = 0; i <= 3; i++)
        {
            sarjor[i].gameObject.SetActive(true);
        }
        sarjoramount = 4;
    }

    void Update()
    {
        //karakterin sola da ateş edebilmesi için
        faceRight = GetComponent<robert_move>().getfaceRight();

        if (Input.GetButtonDown("Fire1") && ammoAmount > 0)
        {

            if (faceRight == true)
            {
                //mermi kovanı
                var spawnedbulletcasing = Instantiate(bulletCasing, casingPoint.position, casingPoint.rotation);
                spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f);
                spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(casingPoint.up + new Vector3(0f, 5f, 0f));
                Destroy(spawnedbulletcasing, 3f);
                //mermi
                var spawnedBullet = Instantiate(bullet, firepoint.position, firepoint.rotation);
                spawnedBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 2000f);


            }
            else
            {
                var spawnedbulletcasing = Instantiate(bulletCasing, casingPoint.position, casingPoint.rotation);
                spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f);
                spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(casingPoint.up + new Vector3(0f, 5f, 0f));
                Destroy(spawnedbulletcasing, 3f);

                var spawnedBullet = Instantiate(bullet, firepoint.position, firepoint.rotation);
                spawnedBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 2000f);

            }


            ammoAmount -= 1;
            allammo -= 1;
            ammo[ammoAmount].gameObject.SetActive(false);
        }
        // if (Input.GetKey(KeyCode.R))
        // {

        //     ammoAmount = 8;
        //     for (int i = 0; i <= 7; i++)
        //     {
        //         ammo[i].gameObject.SetActive(true);
        //     }
        // }

        if (isReloading)
        {
            return;
        }

        anim.SetBool("gun1sarjor", false);
        if (allsarjor > 0 && Input.GetKey(KeyCode.R))
        {
            sarjoramount -= 1;
            allsarjor -= 1;
            sarjor[sarjoramount].gameObject.SetActive(false);


            anim.SetBool("gun1sarjor", true);

            StartCoroutine(Reload());


            return;

        }

    }
    //mermi bittiğinde şarjör değişimim esansında 1 sn beklemesini sağlar
    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        if (allammo > 0)
        {
            ammoAmount = 8;
            for (int i = 0; i <= 7; i++)
            {
                ammo[i].gameObject.SetActive(true);
            }
        }
        isReloading = false;
    }

}
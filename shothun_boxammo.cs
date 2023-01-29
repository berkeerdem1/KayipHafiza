using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shothun_boxammo : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    private Transform firepoint;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject[] ammo;
    private int ammoAmount; //şarjör
    public int allAmmo = 12;  //tüm cephane
    bool faceRight = true;

    private float bulletspeed = 50f;

    public GameObject bulletCasing;
    public Transform casingPoint;
    public Animator anim;

    private recoil recoil; //geri tepme

    //şarjör değiştirme süresi
    public float reloadTime = 1.5f;
    public float reloadtime2 = 5f;

    private bool isReloading = false;


    public GameObject[] sarjor2;
    private int sarjoramount2;
    public int allsarjor2 = 2;


    void Start()
    {
        recoil = GetComponent<recoil>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        for (int i = 0; i <= 3; i++)
        {
            ammo[i].gameObject.SetActive(true);
        }
        ammoAmount = 4;

        for (int i = 0; i <= 1; i++)
        {
            sarjor2[i].gameObject.SetActive(true);
        }
        sarjoramount2 = 2;
    }

    void Update()
    {
        //karakterin sola da ateş edebilmesi için
        faceRight = GetComponent<robertmove2>().getfaceRight();

        anim.SetBool("sarjor", false);
        if (Input.GetButtonDown("Fire1") && ammoAmount > 0)
        {
            if (isReloading)
            {
                return;
            }
            StartCoroutine(Reload2());
            anim.SetBool("sarjor", true);


            recoil.addrecoil(); //geri tepme

            if (faceRight == true)
            {
                //mermi kovanı 
                var spawnedbulletcasing = Instantiate(bulletCasing, casingPoint.position, casingPoint.rotation);
                spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f);//hız
                Destroy(spawnedbulletcasing, 3f);//yok olma süresi
                for (int i = 0; i <= 3; i++)
                {
                    var spawnedBullet = Instantiate(bullet, firepoint.position, firepoint.rotation);//mermi çıkışı
                    spawnedBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 1500f);
                    //merminin sola gitme denemesi

                    //mermi hızı
                    spawnedBullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * bulletspeed);


                    //sağa doğru 4 farklı yöne saçma atabilmesi için
                    switch (i)
                    {
                        case 0://1.mermi vektörü
                            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * bulletspeed + new Vector3(0f, -90f, 0f));
                            spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(casingPoint.up + new Vector3(0f, 40f, 0f));
                            break;
                        case 1://2.mermi
                            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * bulletspeed + new Vector3(0f, 0f, 0f));
                            spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(casingPoint.up + new Vector3(0f, 40f, 0f));
                            break;
                        case 2://3.mermi
                            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * bulletspeed + new Vector3(0f, 0f, 0f));
                            spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(casingPoint.up + new Vector3(0f, 40f, 0f));
                            break;
                        case 3://4.mermi
                            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * bulletspeed + new Vector3(0f, 90f, 0f));
                            spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(casingPoint.up + new Vector3(0f, 40f, 0f));
                            break;
                    }

                }


            }


            else
            {
                //geri tepme
                recoil.addrecoil();

                var spawnedbulletcasing = Instantiate(bulletCasing, casingPoint.position, casingPoint.rotation);
                spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10f);
                Destroy(spawnedbulletcasing, 3f);
                for (int i = 0; i <= 3; i++)
                {
                    var spawnedBullet = Instantiate(bullet, firepoint.position, firepoint.rotation);
                    spawnedBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 1500f);

                    //ne olduğunu bilmiyorum
                    spawnedBullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * bulletspeed);




                    //sola doğru 4 farklı yöne saçma atabilmesi için
                    switch (i)
                    {
                        case 0:
                            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * bulletspeed + new Vector3(0f, -90f, 0f));
                            spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(casingPoint.up + new Vector3(0f, 40f, 0f));
                            break;
                        case 1:
                            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * bulletspeed + new Vector3(0f, 0f, 0f));
                            spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(casingPoint.up + new Vector3(0f, 40f, 0f));
                            break;
                        case 2:
                            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * bulletspeed + new Vector3(0f, 0f, 0f));
                            spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(casingPoint.up + new Vector3(0f, 40f, 0f));
                            break;
                        case 3:
                            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * bulletspeed + new Vector3(0f, 90f, 0f));
                            spawnedbulletcasing.GetComponent<Rigidbody2D>().AddForce(casingPoint.up + new Vector3(0f, 40f, 0f));
                            break;
                    }
                }
            }
            allAmmo -= 1;
            ammoAmount -= 1;
            ammo[ammoAmount].gameObject.SetActive(false);

        }



        // if (Input.GetKey(KeyCode.R))
        // {
        //     if (allAmmo > 0)
        //     {
        //         ammoAmount = 4;
        //         for (int i = 0; i <= 3; i++)
        //         {
        //             ammo[i].gameObject.SetActive(true);
        //         }
        //     }
        // }
        if (isReloading)
        {
            return;
        }
        if (allsarjor2 > 0 && Input.GetKey(KeyCode.R))
        {
            sarjoramount2 -= 1;
            allsarjor2 -= 1;
            sarjor2[sarjoramount2].gameObject.SetActive(false);

            StartCoroutine(Reload());
            //anim.SetBool("sarjor", true);
            anim.SetBool("tamsarjor", true);
            return;

        }
        //anim.SetBool("sarjor", false);
        anim.SetBool("tamsarjor", false);



    }
    //her sarjör arasındaki süre için
    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);
        if (allAmmo > 0)
        {
            ammoAmount = 4;
            for (int i = 0; i <= 3; i++)
            {
                ammo[i].gameObject.SetActive(true);
            }
        }

        isReloading = false;
    }
    //her mermi atışı arasındaki süre için
    IEnumerator Reload2()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadtime2);


        isReloading = false;
    }


}

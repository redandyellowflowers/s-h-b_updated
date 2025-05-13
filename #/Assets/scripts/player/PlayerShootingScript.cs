using System.Collections;
using TMPro;
using UnityEngine;
using EZCameraShake;

public class PlayerShootingScript : MonoBehaviour
{
    [Header("stats")]
    public int currentAmmo;
    public int maxAmmo;
    public int damage = 5;
    public float range = 15f;

    private float nextTimeToFire = 0f;
    public float fireRate = 1f;

    public bool Reloadable;

    [Header("UI")]
    public TextMeshProUGUI ammoCountText;

    [Header("gameObjects")]
    public GameObject firePoint;
    public LineRenderer lineRenderer;
    public GameObject impactEffect;
    public GameObject bloodImpactEffect;
    public ParticleSystem muzzleflash;
    public Animator anim;

    private bool hasLineOfSight = false;
    private bool isShooting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ammoCountText.text = currentAmmo + "/" + maxAmmo.ToString();
        currentAmmo = maxAmmo;

        anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Reload();

        if (currentAmmo > 0)
        {
            lineRenderer.gameObject.SetActive(true);
        }
    }

    public void Shoot()
    {
        isShooting = false;

        if (Input.GetKey(KeyCode.Mouse0))
        {

            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;//adds a bit of delay before the enemy fires (by dividing 1 by the fire rate and adding that to the time.time, which is the current "game" time) as for them to not gun down the player in seconds

                StartCoroutine(targetRays());

                isShooting = true;
                //camerashake
                CameraShaker.Instance.ShakeOnce(3f, 1f, .1f, .4f);
                MuzzleFlash();

                currentAmmo -= 1;

                FindAnyObjectByType<AudioManager>().Play("shoot");//REFERENCING AUDIO MANAGER

                //print("Ammo count " + currentAmmo);
                ammoCountText.text = currentAmmo + "/" + maxAmmo.ToString();

                if (currentAmmo <= 0)
                {
                    isShooting = false;

                    currentAmmo = 0;

                    if (Reloadable)
                    {
                        ammoCountText.text = "reload".ToString();
                    }
                    else
                    {
                        ammoCountText.text = "reload".ToString();
                    }

                    FindAnyObjectByType<AudioManager>().Stop("shoot");//REFERENCING AUDIO MANAGER
                    FindAnyObjectByType<AudioManager>().Play("empty");//REFERENCING AUDIO MANAGER

                    //print("out of ammo");

                    lineRenderer.gameObject.SetActive(false);
                }
            }
        }
        anim.SetBool("isShooting", isShooting);
    }

    public IEnumerator targetRays()//as i understand it, IEnumerators allow for timers, or other such functions, and is convenient for that
    {
        RaycastHit2D ray = Physics2D.Raycast(firePoint.transform.position, transform.up, range);
        if (ray.collider != null)
        {
            hasLineOfSight = ray.collider.CompareTag("Enemy");
            if (hasLineOfSight)
            {
                Debug.DrawRay(firePoint.transform.position, transform.up * range, Color.green);

                TargetHealthScript target = ray.transform.GetComponent<TargetHealthScript>();
                if (target != null && currentAmmo != 0)//meaning that this will only happen when the object being fired at has a target script
                {
                    target.takeDamage(damage);
                }

                lineRenderer.SetPosition(0, firePoint.transform.position);
                lineRenderer.SetPosition(1, ray.point);
                //lineRenderer.material.SetColor("_Color", Color.red);

                if (currentAmmo != 0)
                {
                    GameObject impactGameobject = Instantiate(bloodImpactEffect, ray.point, Quaternion.LookRotation(ray.normal));
                    Destroy(impactGameobject, 2f);
                }
            }
            else
            {
                Debug.DrawRay(firePoint.transform.position, transform.up * range, Color.red);

                lineRenderer.SetPosition(0, firePoint.transform.position);
                lineRenderer.SetPosition(1, ray.point);
                //lineRenderer.material.SetColor("_Color", Color.white);

                if (currentAmmo != 0)
                {
                    GameObject impactGameobject = Instantiate(impactEffect, ray.point, Quaternion.LookRotation(ray.normal));
                    Destroy(impactGameobject, 2f);
                }
            }

            lineRenderer.enabled = true;

            yield return new WaitForSeconds(0.02f);

            lineRenderer.enabled = false;

            /*
            if (ray.collider.CompareTag("Respawn"))
            {
                print("I will not do that.");
            }
            */
        }
    }


    public void MuzzleFlash()
    {
        muzzleflash.Emit(30);
    }

    public void Reload()
    {
        if (Reloadable == true && Input.GetKeyDown(KeyCode.R))
        {
            isShooting = false;

            if (currentAmmo < maxAmmo)
            {
                FindAnyObjectByType<AudioManager>().Play("reload");//REFERENCING AUDIO MANAGER
            }

            currentAmmo = maxAmmo;
            ammoCountText.text = currentAmmo + "/" + maxAmmo.ToString();

            lineRenderer.gameObject.SetActive(true);
        }
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        ammoCountText.text = currentAmmo + "/" + maxAmmo.ToString();

        if (currentAmmo >= maxAmmo)
        {
            currentAmmo = maxAmmo;
        }

        lineRenderer.gameObject.SetActive(true);
    }
}

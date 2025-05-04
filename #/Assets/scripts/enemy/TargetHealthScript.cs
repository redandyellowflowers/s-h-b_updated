using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TargetHealthScript : MonoBehaviour
{
    public int health = 10;

    public GameObject impactEffect;
    public GameObject deathParticleEffect;

    public Light2D lightOBJ;

    public TextMeshPro text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (text != null)
        {
            text.text = health.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.rotation = Quaternion.Euler(0f, 0f, gameObject.transform.rotation.z * -1f);
    }

    public void takeDamage(int amount)
    {
        health -= amount;

        StartCoroutine(LightFX());

        if (text != null)
        {
            text.text = health.ToString();
        }

        //print("Enemy Damage " + health);
        if (health <= 0)
        {
            GameObject impactGameobject = Instantiate(impactEffect, gameObject.transform.position, gameObject.transform.rotation);

            GameObject BloodExplosion = Instantiate(deathParticleEffect, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(BloodExplosion, 1f);

            FindAnyObjectByType<AudioManager>().Play("enemy death");//REFERENCING AUDIO MANAGER

            //Destroy(impactGameobject, .5f);
            Destroy(gameObject);
        }
    }

    public IEnumerator LightFX()//when the enemy is hit, a red light is emmitted to indictate this
    {
        lightOBJ.enabled = true;

        yield return new WaitForSeconds(.1f);

        lightOBJ.enabled = false;
    }
}

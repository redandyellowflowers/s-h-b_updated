using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    [Header("stats")]
    public int currentHealth;
    public int maxHealth;
    //int lowHealthAlert = 4;

    [Header("UI")]
    public Slider healthBar;
    //public TextMeshProUGUI healthText;

    [Header("gameObjects")]
    public GameObject impactEffect;
    public GameObject deathParticleEffect;
    //public GameObject damageScreenUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //healthText.text = currentHealth + "/" + maxHealth.ToString();

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (currentHealth < lowHealthAlert)
        {
            damageScreenUI.SetActive(true);
        }
        else
            damageScreenUI.SetActive(false);
        */
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        healthBar.value = currentHealth;
        //healthText.text = currentHealth + "/" + maxHealth.ToString();
        //print("Enemy Damage " + currentHealth);

        GameObject impactGameobject = Instantiate(impactEffect, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        Destroy(impactGameobject, 3f);

        if (currentHealth <= 0)
        {
            FindAnyObjectByType<AudioManager>().Play("enemy death");//REFERENCING AUDIO MANAGER

            GameObject BloodExplosion = Instantiate(deathParticleEffect, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(BloodExplosion, 1f);
            //damageScreenUI.SetActive(false);

            Destroy(gameObject);
        }
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        healthBar.value = currentHealth;
        //healthText.text = currentHealth + "/" + maxHealth.ToString();

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}

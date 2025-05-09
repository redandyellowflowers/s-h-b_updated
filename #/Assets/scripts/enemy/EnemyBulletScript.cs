using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public int bullletDamage = 5;
    public GameObject impactEffect;
    public GameObject bloodImpactEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindAnyObjectByType<AudioManager>().Play("impact");//REFERENCING AUDIO MANAGER

            GameObject impactGameobject = Instantiate(bloodImpactEffect, collision.transform.position, Quaternion.LookRotation(collision.transform.up));
            Destroy(impactGameobject, 2f);

            PlayerHealthScript playerHP = collision.GetComponent<PlayerHealthScript>();
            playerHP.TakeDamage(bullletDamage);
            Destroy(gameObject);
        }
            Destroy(gameObject);
    }
}

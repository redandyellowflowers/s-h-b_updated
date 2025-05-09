using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class HealthPickUpScript : MonoBehaviour
{
    public int health = 5;

    public GameObject interactText;

    private void Awake()
    {
        interactText.GetComponent<TextMeshPro>().enabled = false;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactText.GetComponent<TextMeshPro>().enabled = false;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        interactText.GetComponent<TextMeshPro>().enabled = true;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = true;

        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))//MAYBE BRING BACK THE PRESS E TO INTERACT HERE, IF THIS AFFECTS THE GAME FEEL CONSIDERABLY********
        {
            PlayerHealthScript playerHealth = collision.GetComponent<PlayerHealthScript>();

            if (playerHealth.currentHealth < playerHealth.maxHealth)
            {
                FindAnyObjectByType<AudioManager>().Play("health pick");//REFERENCING AUDIO MANAGER

                playerHealth.AddHealth(health);
                Destroy(gameObject);
            }
            else
                playerHealth.currentHealth = playerHealth.maxHealth;//meaning nothing happens
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactText.GetComponent<TextMeshPro>().enabled = false;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
    }
}

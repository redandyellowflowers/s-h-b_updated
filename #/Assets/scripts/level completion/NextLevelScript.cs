using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelScript : MonoBehaviour
{
    public GameObject interactText;
    public GameObject pauseMenu_gameManager;
    public GameObject levelCompleteUi;

    //'serializedfield' means that these are still private, but are now accessable in the editor >> removed the variables, but still good to know

    public void Awake()
    {
        pauseMenu_gameManager = GameObject.Find("game manager");

        gameObject.SetActive(false);

        //pressInteractText = GameObject.Find("finish level button");
        interactText.GetComponent<TextMeshPro>().enabled = false;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelCompleteUi.SetActive(false);

        FindAnyObjectByType<AudioManager>().Play("enemies are dead");//REFERENCING AUDIO MANAGER
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        interactText.GetComponent<TextMeshPro>().enabled = true;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = true;

        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            InteractToWin();

            collision.gameObject.GetComponent<PlayerControllerScript>().enabled = false;
            collision.gameObject.GetComponent<PlayerShootingScript>().enabled = false;

            pauseMenu_gameManager.GetComponent<PauseMenuScript>().enabled = false;

            levelCompleteUi.SetActive(true);
        }
    }

    public void InteractToWin()
    {
        //levelCompleteUi.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (interactText != null)
        {
            interactText.GetComponent<TextMeshPro>().enabled = false;
            interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
        }
    }
}

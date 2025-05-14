using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelScript : MonoBehaviour
{
    [Header("gameObjects")]
    public GameObject interactText;
    public GameObject pauseMenu_gameManager;
    public GameObject levelCompleteUi;

    public GameObject hud;

    [Header("animation/s")]
    public Animator anim;
    public bool levelCompleted;

    //'serializedfield' means that variable is still private, but is now accessable in the editor >> removed the serialized variable, but still good to know

    public void Awake()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

        pauseMenu_gameManager = GameObject.Find("game manager");

        //pressInteractText = GameObject.Find("finish level button");
        interactText.GetComponent<TextMeshPro>().enabled = false;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
        levelCompleteUi.SetActive(false);

        //FindAnyObjectByType<AudioManager>().Play("_enemies are dead");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("level complete", levelCompleted);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        interactText.GetComponent<TextMeshPro>().enabled = true;
        interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = true;

        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            //InteractToWin();
            levelCompleted = true;

            collision.gameObject.GetComponent<PlayerControllerScript>().enabled = false;
            collision.gameObject.GetComponent<PlayerShootingScript>().enabled = false;
            collision.gameObject.SetActive(false);

            //hud.SetActive(false);

            //SOMEWAY TO SHOW THAT THE PLAYER HAS MADE PROGRESS

            pauseMenu_gameManager.GetComponent<PauseMenuScript>().enabled = false;

            StartCoroutine(drive());
        }
    }


    IEnumerator drive()
    {
        levelCompleted = true;
        yield return new WaitForSeconds(2f);
        hud.SetActive(false);
        levelCompleteUi.SetActive(true);
    }

    /*
    public void InteractToWin()
    {
        //levelCompleteUi.SetActive(true);
        Time.timeScale = 0f;
    }
    */

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (interactText != null)
        {
            interactText.GetComponent<TextMeshPro>().enabled = false;
            interactText.GetComponent<TextMeshPro>().GetComponent<VertexJitter>().enabled = false;
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseUI;

    public GameObject player;

    private bool gameIsPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            gameObject.GetComponent<PauseMenuScript>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused == false)
        {
            pauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused == true)
        {
            resumeGame();
        }
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);

        gameIsPaused = true;

        player.GetComponent<PlayerControllerScript>().enabled = false;
        player.GetComponent<PlayerShootingScript>().enabled = false;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);

        gameIsPaused = false;

        player.GetComponent<PlayerControllerScript>().enabled = true;
        player.GetComponent<PlayerShootingScript>().enabled = true;
    }
}

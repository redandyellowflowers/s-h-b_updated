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
        if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused == false)
        {
            pauseGame();
            player.GetComponent<PlayerControllerScript>().enabled = false;
            player.GetComponent<PlayerShootingScript>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused == true)
        {
            resumeGame();
            player.GetComponent<PlayerControllerScript>().enabled = true;
            player.GetComponent<PlayerShootingScript>().enabled = true;
        }
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);

        gameIsPaused = true;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);

        gameIsPaused = false;
    }
}

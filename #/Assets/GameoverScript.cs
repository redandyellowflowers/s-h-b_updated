using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverScript : MonoBehaviour
{
    [Header("gameObjects")]
    public GameObject player;
    public GameObject gameOverUI;

    [Header("text")]
    public TextMeshProUGUI gameoverText;
    public string text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameoverText = gameOverUI.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            gameOverUI.SetActive(true);
            gameoverText.text = text.ToString();

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}

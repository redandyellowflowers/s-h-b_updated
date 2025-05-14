using TMPro;
using UnityEngine;

public class GameoverScript : MonoBehaviour
{
    [Header("player")]
    public GameObject player;

    [Header("gameObjects")]
    public GameObject gameOverUI;

    [Header("text")]
    public TextMeshProUGUI gameoverText;

    [TextArea(2, 4)]
    public string text;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        gameoverText = gameOverUI.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            FindAnyObjectByType<AudioManager>().Stop("background");
            //FindAnyObjectByType<AudioManager>().Play("enemies are dead");

            gameOverUI.SetActive(true);
            gameoverText.text = text.ToString();
        }
    }
}

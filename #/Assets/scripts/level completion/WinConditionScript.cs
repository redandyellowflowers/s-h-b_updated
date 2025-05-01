using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class WinConditionScript : MonoBehaviour
{
    public string completionPrompt = "leave";//to be displayed once all enemies have been killed

    public TextMeshProUGUI enemyCount;

    public GameObject endTrigger;

    GameObject obstruction;

    public GameObject[] enemies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyCount.text = enemies.Length.ToString();
        //endTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        obstruction = GameObject.FindGameObjectWithTag("Obstruction");

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int numberOfEnemies = enemies.Length;
        enemyCount.text = "enemies = " + enemies.Length.ToString();

        if (numberOfEnemies <= 0)
        {
            enemyCount.text = completionPrompt.ToString();

            FindAnyObjectByType<AudioManager>().Stop("background");//REFERENCING AUDIO MANAGER

            if (endTrigger != null)
            {
                endTrigger.SetActive(true);
            }

            if (obstruction != null)//this is where we can add the keycard system with a '&& has keycard bool == true' or something 
            {
                Destroy(obstruction);
            }
        }
    }
}

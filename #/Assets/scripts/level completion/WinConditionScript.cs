using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class WinConditionScript : MonoBehaviour
{
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
        enemyCount.text = enemies.Length.ToString();

        if (numberOfEnemies == 0)
        {
            numberOfEnemies = 0;
            enemyCount.text = "leave this <#FF0000>place</color>!".ToString();
            //FindAnyObjectByType<AudioManager>().Stop("background");
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

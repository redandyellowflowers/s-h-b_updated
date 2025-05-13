using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class WinConditionScript : MonoBehaviour
{
    [Header("text")]
    public string completionPrompt = "'leave!'";//to be displayed once all enemies have been killed

    public TextMeshProUGUI enemyCount;

    [Header("gameObjects")]
    public Image flashScreen;

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

        if (numberOfEnemies <= 0)
        {
            StartCoroutine(Flash());
            enemyCount.text = completionPrompt.ToString();

            FindAnyObjectByType<AudioManager>().Stop("background");//REFERENCING AUDIO MANAGER
            FindAnyObjectByType<AudioManager>().Play("enemies are dead");

            if (endTrigger != null)
            {
                endTrigger.SetActive(true);
            }

            if (obstruction != null)//THIS IS WHERE THE KEYCARD SYSTEM CAN BE PLACED?
            {
                Destroy(obstruction);
            }
        }
    }

    IEnumerator Flash()
    {
        if (flashScreen != null)
        {
            flashScreen.enabled = true;

            yield return new WaitForSeconds(0.02f);

            flashScreen.enabled = false;
            gameObject.GetComponent<WinConditionScript>().enabled = false;
        }
    }
}

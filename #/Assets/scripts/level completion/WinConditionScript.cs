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
    public TextMeshProUGUI keyCount;

    [Header("gameObjects")]
    public Image flashScreen;

    public GameObject endTrigger;

    GameObject obstruction;

    public GameObject[] enemies;
    public GameObject[] keys;

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
        enemyCount.text = "<size=100%><#ff0000>enemies</color> in level: <size=100%>" + enemies.Length.ToString();

        keys = GameObject.FindGameObjectsWithTag("Respawn");
        int maxNumberOfKeys = keys.Length;
        keyCount.text = "<size=40%>no. of <#ff0000>hints</color> in level: <size=40%><#ff0000>" + keys.Length.ToString();

        if (maxNumberOfKeys == 0)
        {
            keyCount.text = "<size=40%>all <#ff0000>hints</color> have been collected";
        }

        if (numberOfEnemies <= 0 && maxNumberOfKeys <= 0)
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

using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    //public GameObject player;

    public TextMeshProUGUI dialogueText;
    public GameObject introUi;
    public TextMeshProUGUI pressToContinue;

    public GameObject nextLevelButton;

    [Header("dialogue")]
    public float dialogueSpeed;
    public string npcName;
    [TextArea(2, 4)] public string[] sentences;
    private int index = 0;//tracking the sentences

    private bool isDoneTalking = true;

    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NextSentence();
        //player.GetComponent<PlayerControllerScript>().enabled = false;
        //player.GetComponent<PlayerShootingScript>().enabled = false;

        introUi.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isDoneTalking)
        {
            dialogueText.text = npcName + "<br>" + "<br>" + "";
            NextSentence();
        }
    }

    void NextSentence()
    {
        if (index <= sentences.Length - 1)
        {
            dialogueText.text = npcName + "<br>" + "<br>" + "";
            StartCoroutine(WriteSentence());
        }
        else
        {
            dialogueText.text = npcName + "<br>" + "<br>" + "";
            index = 0;

            nextLevelButton.SetActive(true);

            introUi.SetActive(false);
            //player.GetComponent<PlayerControllerScript>().enabled = true;
            //player.GetComponent<PlayerShootingScript>().enabled = true;

            //Destroy(gameObject, .5f);
        }
    }

    IEnumerator WriteSentence()
    {
        foreach (char character in sentences[index].ToCharArray())
        {
            dialogueText.text += character;
            //FindAnyObjectByType<AudioManager>().PlayForButton("typing");//REFERENCING AUDIO MANAGER
            yield return new WaitForSeconds(dialogueSpeed);

            isDoneTalking = false;
            pressToContinue.enabled = false;
        }
        isDoneTalking = true;
        pressToContinue.enabled = true;
        index++;
    }
}

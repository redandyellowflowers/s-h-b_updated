using EZCameraShake;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    bool intro;

    public TextMeshProUGUI dialogueText;
    public GameObject introUi;
    public TextMeshProUGUI pressToContinue;

    [Header("dialogue")]
    public float dialogueSpeed;
    public string npcName;
    [TextArea(2, 4)] public string[] sentences;
    private int index = 0;//tracking the sentences

    private bool isDoneTalking = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FindAnyObjectByType<AudioManager>().Play("enemies are dead");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NextSentence();
        player.GetComponent<PlayerControllerScript>().enabled = false;
        player.GetComponent<PlayerShootingScript>().enabled = false;

        introUi.SetActive(true);

        intro = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isDoneTalking)
        {
            dialogueText.text = npcName + "<br>" + "<br>" + "";
            NextSentence();
        }

        anim.SetBool("intro", intro);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DisableUi());
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
            StartCoroutine(DisableUi());
            //Destroy(gameObject, .5f);
        }
    }

    IEnumerator WriteSentence()
    {
        foreach (char character in sentences[index].ToCharArray())
        {
            dialogueText.text += character;
            FindAnyObjectByType<AudioManager>().PlayForButton("typing");//REFERENCING AUDIO MANAGER
            yield return new WaitForSeconds(dialogueSpeed);

            isDoneTalking = false;
            pressToContinue.enabled = false;
        }
        isDoneTalking = true;
        pressToContinue.enabled = true;
        index++;
    }

    public void SkipButton()
    {
        StartCoroutine(DisableUi());
    }

    IEnumerator DisableUi()
    {
        FindAnyObjectByType<AudioManager>().PlayForButton("click_backward");//REFERENCING AUDIO MANAGER

        dialogueText.text = npcName + "<br>" + "<br>" + "";
        index = 0;

        intro = false;

        FindAnyObjectByType<AudioManager>().Play("background");
        FindAnyObjectByType<AudioManager>().Stop("enemies are dead");

        yield return new WaitForSeconds(.75f);

        introUi.SetActive(false);
        gameObject.GetComponent<IntroScript>().enabled = false;
        FindAnyObjectByType<AudioManager>().Stop("typing");//REFERENCING AUDIO MANAGER
        player.GetComponent<PlayerControllerScript>().enabled = true;
        player.GetComponent<PlayerShootingScript>().enabled = true;
    }
}

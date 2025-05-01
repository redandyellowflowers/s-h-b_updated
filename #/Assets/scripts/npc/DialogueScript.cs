using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    /*
    Title: Animated Dialogue System - Unity Tutorial
    Author: Zyger
    Date: 27 April 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=hvgfFNorZH8
    */

    [Header("player")]
    public GameObject player;
    public CameraFollowScript cam;
    public PlayerControllerScript MovementScript;

    [Header("UI")]
    public GameObject textBox;
    public Image headshot;
    //public GameObject interactPromptText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI pressToContinue;
    public GameObject interactText;

    [Header("dialogue")]
    public float dialogueSpeed;
    public string npcName;
    [TextArea(2, 4)]public string[] sentences;
    private int index = 0;//tracking the sentences

    private bool isDoneTalking = true;

    [Header("event after dialogue")]
    public GameObject nextNPC;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollowScript>();
        MovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //interactPromptText.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");

        headshot.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        interactText.transform.rotation = Quaternion.Euler(0f, 0f, gameObject.transform.rotation.z * -1f);

        if (player == null)
        {
            gameObject.GetComponent<DialogueScript>().enabled = false;
        }

        if (gameObject.GetComponent<NpcScript>().distance < gameObject.GetComponent<NpcScript>().detectionRadius/2 && gameObject.GetComponent<NpcScript>().hasLineOfSight)
        {
            interactText.GetComponent<TextMeshPro>().enabled = true;

            if (Input.GetKeyDown(KeyCode.E) && isDoneTalking)
            {
                MovementScript.moveSpeed = 0f;
                cam.yOffset = -3f;

                textBox.SetActive(true);
                dialogueText.text = npcName + " : " + "";

                headshot.enabled = true;
                dialogueText.enabled = true;
                NextSentence();
            }
        }
        else if (gameObject.GetComponent<NpcScript>().distance > gameObject.GetComponent<NpcScript>().detectionRadius/2)
        {
            interactText.GetComponent<TextMeshPro>().enabled = false;

            //textBox.SetActive(false);
            dialogueText.text = npcName + " : " + "";
            index = 0;
            headshot.enabled = false;
            dialogueText.enabled = false;
        }
    }

    void NextSentence()
    {
        if (index <= sentences.Length - 1)
        {
            dialogueText.text = npcName + " : " + "";
            StartCoroutine(WriteSentence());
        }
        else
        {
            dialogueText.text = npcName + " : " + "";
            index = 0;

            MovementScript.moveSpeed = 12f;
            cam.yOffset = 0f;

            headshot.enabled = false;
            textBox.SetActive(false);

            //Destroy(gameObject, .5f);

            if (nextNPC != null)
            {
                nextNPC.SetActive(true);
            }
            else
            {
                print("nothing");
            }
        }
    }

    IEnumerator WriteSentence()
    {
        foreach (char character in sentences[index].ToCharArray())
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(dialogueSpeed);

            isDoneTalking = false;
            pressToContinue.enabled = false;
        }
        isDoneTalking = true;
        pressToContinue.enabled = true;
        index++;
    }
}

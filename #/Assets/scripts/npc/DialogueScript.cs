using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DialogueScript : MonoBehaviour
{
    /*
    Title: Animated Dialogue System - Unity Tutorial
    Author: Zyger
    Date: 27 April 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=hvgfFNorZH8
    */

    public bool isBrokenSophie;
    public GameObject impactEffect;
    public GameObject deathParticleEffect;

    public Image flashScreen;

    [Header("player")]
    public GameObject player;
    public CameraFollowScript cam;
    public PlayerControllerScript MovementScript;
    public float newOffset = -3f;
    PlayerShootingScript playerShoot;

    [Header("UI")]
    public GameObject textBox;
    public GameObject dialogueUI;
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

        playerShoot = MovementScript.gameObject.GetComponent<PlayerShootingScript>();
        interactText = gameObject.GetComponentInChildren<TextMeshPro>().gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactText.GetComponent<TextMeshPro>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        interactText.transform.rotation = Quaternion.Euler(0f, 0f, gameObject.transform.rotation.z * -1f);

        if (player == null)
        {
            gameObject.GetComponent<DialogueScript>().enabled = false;
        }

        if (gameObject.GetComponent<NpcScript>().distance < gameObject.GetComponent<NpcScript>().detectionRadius / 2 && gameObject.GetComponent<NpcScript>().hasLineOfSight)
        {
            interactText.GetComponent<TextMeshPro>().enabled = true;

            if (Input.GetKeyDown(KeyCode.E) && isDoneTalking)
            {
                //FindAnyObjectByType<AudioManager>().PlayForButton("click_forward");

                playerShoot.enabled = false;

                MovementScript.moveSpeed = 0f;
                cam.yOffset = newOffset;

                textBox.SetActive(true);
                dialogueUI.SetActive(true);
                dialogueText.text = npcName + "<br>" + "<br>" + "";

                dialogueText.enabled = true;
                NextSentence();
            }
        }
        else if (gameObject.GetComponent<NpcScript>().distance > gameObject.GetComponent<NpcScript>().detectionRadius / 2)
        {
            interactText.GetComponent<TextMeshPro>().enabled = false;
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
            playerShoot.enabled = true;

            dialogueText.text = npcName + "<br>" + "<br>" + "";
            index = 0;

            MovementScript.moveSpeed = 16f;
            cam.yOffset = 0f;

            dialogueUI.SetActive(false);
            textBox.SetActive(false);

            if (isBrokenSophie)
            {
                StartCoroutine(Flash());

                GameObject impactGameobject = Instantiate(impactEffect, gameObject.transform.position, gameObject.transform.rotation);

                GameObject BloodExplosion = Instantiate(deathParticleEffect, gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                Destroy(BloodExplosion, 1f);

                FindAnyObjectByType<AudioManager>().Play("enemy death");//REFERENCING AUDIO MANAGER
            }


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

            FindAnyObjectByType<AudioManager>().PlayForButton("typing");

            yield return new WaitForSeconds(dialogueSpeed);

            isDoneTalking = false;
            pressToContinue.enabled = false;
        }
        isDoneTalking = true;
        pressToContinue.enabled = true;
        index++;
    }

    IEnumerator Flash()
    {
        if (flashScreen != null)
        {
            flashScreen.enabled = true;

            yield return new WaitForSeconds(0.02f);

            flashScreen.enabled = false;

            Destroy(gameObject);
        }
    }
}

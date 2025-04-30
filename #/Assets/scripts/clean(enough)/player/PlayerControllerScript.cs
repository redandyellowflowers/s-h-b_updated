using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{
    [Header("base movement")]
    public float moveSpeed = 12f;

    public Rigidbody2D rigidBody;

    public Camera cam;

    public bool isIsometric;

    [Header("slow motion")]
    public float currentStamina;
    public float MaxStamina;
    public float slowMoRate;
    public Slider bulletTimeBar;
    //public Text slowMoText;

    [Header("rendering")]
    public GameObject render;

    public Light2D PlayerLight;

    Vector2 mousePos;

    //private Animator anim;
    //bool isWalking = false;

    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;

        bulletTimeBar.maxValue = MaxStamina;
        bulletTimeBar.value = currentStamina;

        //anim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        moveMent();

        if (isIsometric)
        {
            render.transform.rotation = Quaternion.Euler(0f, 0f, gameObject.transform.rotation.z * -1f);//locks rotation of child object by countering the rotation of the parent of object, important for potential walk anim
        }
        //slowMoText.text = currentStamina.ToString("0");
    }

    void FixedUpdate()
    {
        /*
        Title: TOP DOWN SHOOTING in Unity
        Author: Asbjørn Thirslund / Brackeys
        Date: 06 April 2025
        Code version: 1
        Availability: https://www.youtube.com/watch?v=LNLVOjbrQj4

        >>>it is the "character controller facing the cursor" mechanic part of the above tutorial being referenced
        */
        Vector2 lookDirection = mousePos - rigidBody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rigidBody.rotation = angle;
    }

    public void moveMent()
    {
        //isWalking = false;

        Vector3 movePosition = Vector3.zero;//we're defined the vector (the 3 axis with which we need for our controller to move)

        if (Input.GetKey(KeyCode.W))
        {
            movePosition.y += moveSpeed;

            //isWalking = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movePosition.y -= moveSpeed;

            //isWalking = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movePosition.x -= moveSpeed;

            //isWalking = true;
            //transform.localScale = new Vector3(-1, transform.localScale.y);//flip the sprite left
        }
        if (Input.GetKey(KeyCode.D))
        {
            movePosition.x += moveSpeed;

            //isWalking = true;
            //transform.localScale = new Vector3(1, transform.localScale.y);//flip the sprite left
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //ENABLE/RUN BULLET TIME screen effect HERE***

            Time.timeScale = .5f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            PlayerLight.pointLightOuterRadius = 20;
            PlayerLight.color = Color.red;

            currentStamina -= slowMoRate;

            if (currentStamina <= 0)
            {
                //DISABLE/RUN BULLET TIME VISUALIZER HERE***

                currentStamina = 0;
                Time.timeScale = 1f;

                PlayerLight.pointLightOuterRadius = 20;
                PlayerLight.color = Color.white;
            }

            bulletTimeBar.value = currentStamina;
            //slowMoText.text = currentStamina.ToString("0");
        }
        else
            if (currentStamina != MaxStamina)
        {
            //DISABLE/RUN BULLET TIME VISUALIZER HERE***

            Time.timeScale = 1f;

            PlayerLight.pointLightOuterRadius = 20;
            PlayerLight.color = Color.white;

            currentStamina += slowMoRate;

            bulletTimeBar.value = currentStamina;

            if (currentStamina >= MaxStamina)
            {
                currentStamina = MaxStamina;
            }
        }

            /*
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            */

            rigidBody.transform.position += movePosition.normalized * moveSpeed * Time.deltaTime;//this line (more so the "normalized" and "time.deltatime") essentially stops the controller from building an unfixed amount of momentum

            //anim.SetBool("isWalking", isWalking);
    }
}

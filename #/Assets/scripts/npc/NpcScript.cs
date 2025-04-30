using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NpcScript : MonoBehaviour
{
    [Header("player")]
    public GameObject player;


    [HideInInspector]
    public bool hasLineOfSight = false;

    [Header("gameObjects")]
    public Rigidbody2D rigidBody;
    public GameObject render;
    public GameObject firePoint;

    [Header("stats")]
    public int detectionRadius = 10;

    public bool isIsometric;

    [HideInInspector]
    public float distance;

    Vector2 playerPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public void Update()
    {
        if (isIsometric)
        {
            render.transform.rotation = Quaternion.Euler(0f, 0f, gameObject.transform.rotation.z * -1f);
        }

        if (player != null)
        {
            CollisionDetection();
            playerPos = player.transform.position;
        }
        else
            gameObject.GetComponent<NpcScript>().enabled = false;
    }

    public void FixedUpdate()//used primarily when dealing with physics
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < detectionRadius && hasLineOfSight && player != null)//if the object has line of sight of the player, then they look, otherwise, theyre static
        {
            Vector2 lookDirection = playerPos - rigidBody.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            rigidBody.rotation = angle;
        }
    }

    public void CollisionDetection()
    {
        RaycastHit2D ray = Physics2D.Raycast(firePoint.transform.position, player.transform.position - firePoint.transform.position);

        if (ray.collider != null)
        {
            hasLineOfSight = ray.collider.CompareTag(player.tag);

            if (hasLineOfSight && distance < detectionRadius)
            {
                Debug.DrawRay(firePoint.transform.position, player.transform.position - firePoint.transform.position, Color.green);
            }
            else
            {
                Debug.DrawRay(firePoint.transform.position, player.transform.position - firePoint.transform.position, Color.red);
            }
        }
    }
}

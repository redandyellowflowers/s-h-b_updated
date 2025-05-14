using UnityEngine;

public class OutOfBoundsScript : MonoBehaviour
{
    [Header("player")]
    public GameObject player;

    [Header("gameObjects")]
    public OutOfBoundsScript outOfBounds;

    [TextArea(2, 4)]
    public string text = "gameover" + "<align=right><#ffffff><br><size=25%><br>you have <#ff0000>died</color>. you cannot through here, not ever.";

    [Header("stats")]
    public int detectionRadius = 30;

    [HideInInspector]
    public float distance;

    Vector2 playerPos;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        outOfBounds = gameObject.GetComponent<OutOfBoundsScript>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void FixedUpdate()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance > detectionRadius && player != null)
            {
                player.GetComponent<PlayerHealthScript>().TakeDamage(100);
                outOfBounds.enabled = false;

                gameObject.GetComponent<GameoverScript>().text = text;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

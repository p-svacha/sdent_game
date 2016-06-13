using UnityEngine;
using System.Collections;

public class boss_cook : MonoBehaviour
{

    GameObject player;
    Rigidbody2D rb2d;
    Rigidbody2D playerBody;
    Vector2 startposition;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        startposition = rb2d.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distVec = playerBody.position - rb2d.position;

        
    }

    void OnCollisionEnter2D(Collision2D collider)
    {

        if (collider.gameObject == player)
        {
            Destroy(rb2d.gameObject);
        }
    }
}

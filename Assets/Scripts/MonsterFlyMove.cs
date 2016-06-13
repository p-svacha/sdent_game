using UnityEngine;
using System.Collections;

public class MonsterFlyMove : MonoBehaviour {

    GameObject player;
    Rigidbody2D rb2d;
    Rigidbody2D playerBody;
    Vector2 startposition;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        playerBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        startposition = rb2d.position;
    }
	
	// Update is called once per frame
	void Update () {
        var dist = Vector2.Distance(rb2d.position, playerBody.position);
        var distVec = new Vector2();
        if (dist < 1)
        {
            distVec = playerBody.position - rb2d.position;
        } else
        {
            distVec = startposition - rb2d.position;
        }
        if (distVec.magnitude > 0.01f)
        {
            rb2d.MovePosition(rb2d.position + distVec.normalized * 0.01f);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collider)
    {

        if (collider.gameObject == player)
        {
            Destroy(rb2d.gameObject);
        }
    }
}

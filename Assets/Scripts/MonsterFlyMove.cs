using UnityEngine;
using System.Collections;

public class MonsterFlyMove : MonoBehaviour {

    GameObject player;
    Rigidbody2D rb2d;
    private Rigidbody2D playerBody;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerBody = player.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        var dist = Vector2.Distance(rb2d.position, playerBody.position);
        var distVec = playerBody.position - rb2d.position;
        if (dist < 1)
        {
            rb2d.MovePosition(rb2d.position + distVec * 0.01f);
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

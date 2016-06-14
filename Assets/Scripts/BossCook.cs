using UnityEngine;
using System.Collections;

public class BossCook : MonoBehaviour {

    GameObject player;
    Rigidbody2D rb2d;
    Rigidbody2D playerBody;
    Vector2 startposition;
    Animator anim;
    public bool fighting;
    public GameObject platePrefab;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        startposition = rb2d.position;
        anim = GetComponent<Animator>();
        fighting = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootPlate()
    {
        GameObject o = (GameObject)Instantiate(platePrefab, new Vector2(playerBody.position.x, rb2d.position.y), new Quaternion());
    }

    public void EnableFight(bool fight)
    {
        anim.SetBool("boss_cook_fight", fight);
        fighting = fight;
        if (fight)
        {
            InvokeRepeating("ShootPlate", 0, 1);
        } else
        {
            CancelInvoke();
        }
    }


}

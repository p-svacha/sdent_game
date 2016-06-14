using UnityEngine;
using System.Collections;

public class BossCook : MonoBehaviour {

    GameObject player;
    Rigidbody2D rb2d;
    Rigidbody2D playerBody;
    Vector2 startposition;
    Animator anim;
    bool fighting;

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
        if (fighting)
        {
            //move boss
        }
    }

    public void EnableFight(bool fight)
    {
        anim.SetBool("boss_cook_fight", fight);
        fighting = fight;
    }


}

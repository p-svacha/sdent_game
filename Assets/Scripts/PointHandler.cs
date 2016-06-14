using UnityEngine;
using System.Collections;
using System;

public class PointHandler : MonoBehaviour {

    private int meatCount;
    private int lifeCount;
    private Vector2 startPosition;

	// Use this for initialization
	void Start () {
        meatCount = 5;
        lifeCount = 3;
        startPosition = GetComponent<Rigidbody2D>().position;
	}
	
	// Update is called once per frame
	void Update () {
	    if (meatCount == 0)
        {
            //remove Barrier
        }
        if (lifeCount == 0)
        {
            //gameover
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject == GameObject.Find("boss_cook"))
        {
            lifeCount--;
            initPlayer();
        } else
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("enemy");
            for (int i = 0; i < go.Length; i++)
            {
                if (coll.gameObject == go[i])
                {
                    lifeCount--;
                    initPlayer();
                    break;
                }
            }

            go = GameObject.FindGameObjectsWithTag("meat");
            for (int i = 0; i < go.Length; i++)
            {
                if (coll.gameObject == go[i])
                {
                    meatCount--;
                    Destroy(go[i]);
                    break;
                }
            }
        }
    }

    private void initPlayer()
    {
        GetComponent<Rigidbody2D>().position = startPosition;
    }
}

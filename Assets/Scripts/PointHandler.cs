using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class PointHandler : MonoBehaviour {

    private int meatCount;
    private int lifeCount;
    private Vector2 startPosition;
    private PlayerMovement pm;

	// Use this for initialization
	void Start () {
        meatCount = 5;
        lifeCount = 3;
        startPosition = GetComponent<Rigidbody2D>().position;
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject == GameObject.Find("boss_cook"))
        {
            OnDamage();
        }
        else
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("enemy");
            for (int i = 0; i < go.Length; i++)
            {
                if (coll.gameObject == go[i])
                {
                    OnDamage();
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
                    if (meatCount == 0)
                    {
                        pm.cameraOnBarrier();
                    }
                    break;
                }
            }
        }
    }

    public void OnDamage()
    {
        Destroy(GameObject.Find("Heart" + lifeCount));
        lifeCount--;
        if (lifeCount == 0)
        {
            SceneManager.LoadScene(0);
        }
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            FindObjectOfType<PlayerMovement>().EndSwordSwing();
        }
        InitPlayer();
    }



    private void InitPlayer()
    {
        GetComponent<Rigidbody2D>().position = startPosition;
    }
}

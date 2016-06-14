using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("coll true");

        if (other.gameObject == GameObject.Find("Player"))
        {
            FindObjectOfType<BossCook>().EnableFight(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("coll false");

        if (other.gameObject == GameObject.Find("Player"))
        {
            FindObjectOfType<BossCook>().EnableFight(false);
        }
    }
}

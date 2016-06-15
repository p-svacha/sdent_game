using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

    float startTime;
    float endTime;
    Rigidbody2D pos;
	public AudioClip hit;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        endTime = startTime + 0.5f;
        pos = FindObjectOfType<PlayerMovement>().GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (Time.time < endTime)
        {
            transform.RotateAround(pos.position, Vector3.forward, 360 / (endTime - startTime) * Time.deltaTime);
        }
        else
        {
            FindObjectOfType<PlayerMovement>().EndSwordSwing();
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var boss = FindObjectOfType<BossCook>();
        if (boss != null)
        {
            if (other.gameObject == boss.gameObject)
            {
                if (boss.fighting)
                {
                    Destroy(boss.gameObject);
                }
            }
        }

        GameObject[] go = GameObject.FindGameObjectsWithTag("enemy");
        for (int i = 0; i < go.Length; i++)
        {
            if (other.gameObject == go[i])
            {
                Destroy(go[i]);
				GetComponent<AudioSource>().PlayOneShot (hit);
                break;
            }
        }
    }
}

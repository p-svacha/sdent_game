using UnityEngine;
using System.Collections;

public class Plate : MonoBehaviour {

    private Vector2 startPosition;
    private Vector2 endPosition;
    private Rigidbody2D rBody;
    private float factor;
    private float t = 0;

    // Use this for initialization
    void Start ()
    {
        this.rBody = GetComponent<Rigidbody2D>();
        this.startPosition = rBody.position;
        this.endPosition = new Vector2(startPosition.x, startPosition.y - 3);
        this.factor = 1f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (t < factor)
        {
            t += Time.deltaTime;
            rBody.MovePosition(Vector2.Lerp(startPosition, endPosition, t * factor));
        }
        else
        {
            if (rBody.position.y <= endPosition.y)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("coll true");

        if (other.gameObject == GameObject.Find("Player"))
        {
            FindObjectOfType<PointHandler>().OnDamage();
            Destroy(this.gameObject);
        }
    }
}

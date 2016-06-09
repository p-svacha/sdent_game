using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

    public int x, y;
    public float gridSize;
    private Rigidbody2D rBody;
    private bool moving = false;
    private float t;
    private float factor = 0.2f;
    private Vector2 startPosition, endPosition;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
	
	}

    public void moveTo(int x, int y)
    {
        if(!moving)
        {
            startPosition = rBody.position;
            this.x += x;
            this.y += y;

            endPosition = new Vector2(startPosition.x + x * gridSize, startPosition.y + y * gridSize);
            moving = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            if (t < factor)
            {
                t += Time.deltaTime;
                rBody.MovePosition(Vector2.Lerp(startPosition, endPosition, t * 1 / factor));
            }
            else
            {
                t = 0;
                rBody.MovePosition(endPosition);
                moving = false;
            }

        }
    }
}

using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

    public GameObject rock;
    public Rigidbody2D rBody;
    private bool moving = false;
    private float t;
    private float factor = 0.2f;
    private Vector2 startPosition, endPosition;
    private Vector2 input;
    public Position actualPos;
    private Position targetPos;
    private MapLogic map;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        input = new Vector2();
        targetPos = new Position();
        map = GameObject.Find("MapLogic").GetComponent<MapLogic>();
    }
	
	// Update is called once per frame
	void Update () {
        move();
    }

    public void startRockMove(Vector2 input)
    {
        this.input = input;
        
    }

    public void move()
    {
        if (moving)
        {
            if (t < factor)
            {
                t += Time.deltaTime;
                rBody.MovePosition(Vector3.Lerp(startPosition, endPosition, t * 1 / factor));
            }
            else
            {
                t = 0;
                rBody.MovePosition(endPosition);
                moving = false;
            }

        }
        else
        {
            targetPos.x = actualPos.x + (int)input.x;
            targetPos.y = actualPos.y + (int)input.y * -1;

            if (input != Vector2.zero)
            {
                startPosition = rBody.position;
                endPosition = new Vector2(startPosition.x + input.x * map.gridSize, startPosition.y + input.y * map.gridSize);
                if (map.getTile(actualPos.x,actualPos.y) == MapLogic.HOLE)
                {
                    moving = false;
                    map.holeGetsFilled(false);
                } else
                {
                    switch (map.getTile(targetPos.x, targetPos.y))
                    {
                        case MapLogic.ICE:
                        case MapLogic.HOLE:
                            actualPos.x = targetPos.x;
                            actualPos.y = targetPos.y;
                            moving = true;
                            break;

                        case MapLogic.BARRIER:
                        case MapLogic.GROUND:
                        case MapLogic.WALL:
                            moving = false;
                            break;
                    }
                }
            }
        }
    }
}

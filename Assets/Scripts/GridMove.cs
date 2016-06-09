using System.Collections;
using UnityEngine;

class GridMove : MonoBehaviour
{

    public float gridSize;
    public Camera cam;

    private enum Orientation
    {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Horizontal;

    private int x, y, targetX, targetY;
    private bool moving;
    private bool sliding;
    private Rigidbody2D rBody;
    private GameObject mapLogic;
    private MapLogic map;

    private Vector2 input;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private float t = 0;
    private float factor;
    Animator anim;

    public void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        factor = 0.2f;
        x = 14;
        y = 10;
        anim = GetComponent<Animator>();
        mapLogic = GameObject.Find("MapLogic");
        map = mapLogic.GetComponent<MapLogic>();
       

        initPlayer();
    }

    private void initPlayer()
    {
        rBody.position = new Vector2((x + 0.5f) * gridSize, -(y + 0.2f) * gridSize);
        cam.transform.position = rBody.position;
    }

    public void Update()
    {
        anim.SetBool("is_walking", moving);
        anim.SetFloat("input_x", input.x);
        anim.SetFloat("input_y", input.y);
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);


        if (moving)
        {
            if (t < factor)
            {
                t += Time.deltaTime;
                rBody.MovePosition(Vector3.Lerp(startPosition, endPosition, t * 1/factor));
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
            if(!sliding) input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                input.x = System.Math.Sign(input.x);
                input.y = 0;
                targetX = x + (int)input.x;
                targetY = y;
                gridOrientation = Orientation.Horizontal;
            }
            else if (Mathf.Abs(input.x) < Mathf.Abs(input.y))
            {
                input.x = 0;
                input.y = System.Math.Sign(input.y);
                targetX = x;
                targetY = y  + (int)input.y * -1;
                gridOrientation = Orientation.Vertical;
            }
            else
            {
                input = Vector2.zero;
            }


            if (input != Vector2.zero)
            {
                startPosition = rBody.position;
                endPosition = new Vector3(startPosition.x + input.x * gridSize, startPosition.y + input.y * gridSize, startPosition.z);
                Rock targetRock = map.rockAt(targetX, targetY);
                switch (map.getTile(targetX, targetY))
                {
                    case MapLogic.GROUND:
                        x = targetX;
                        y = targetY;
                        moving = true;
                        sliding = false;
                        break;

                    case MapLogic.ICE:
                        if (targetRock != null)
                        {
                            endPosition = startPosition;
                            sliding = false;
                            moving = false;
                            targetRock.moveTo((int)input.x, (int)input.y);
                        }
                        else
                        {
                            x = targetX;
                            y = targetY;
                            moving = true;
                            sliding = true;
                        }
                        break;

                    case MapLogic.WALL:
                        sliding = false;
                        break;
                    case MapLogic.HOLE:
                        sliding = false;
                        break;
                }

            }
        }
    }
}
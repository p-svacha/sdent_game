using System.Collections;
using UnityEngine;

class GridMove : MonoBehaviour
{

    private Camera cam;
    private Position actualPos, targetPos;
    private bool moving;
    private bool sliding;
    private Rigidbody2D rBody;
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
        actualPos = new Position(13,12);
        targetPos = new Position();
        input = new Vector2();
        anim = GetComponent<Animator>();
        map = GameObject.Find("MapLogic").GetComponent<MapLogic>();
        cam = Camera.FindObjectOfType<Camera>();


        initPlayer();
    }

    public void initPlayer()
    {
        rBody.position = new Vector2((actualPos.x + 0.5f) * map.gridSize, -(actualPos.y + 0.2f) * map.gridSize);
        cam.transform.position = rBody.position;
    }

    public void Update()
    {
        anim.SetBool("is_walking", moving);
        anim.SetFloat("input_x", input.x);
        anim.SetFloat("input_y", input.y);
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        move();
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
            if (!sliding) input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                input.x = System.Math.Sign(input.x);
                input.y = 0;
                targetPos.x = actualPos.x + (int)input.x;
                targetPos.y = actualPos.y;
            }
            else if (Mathf.Abs(input.x) < Mathf.Abs(input.y))
            {
                input.x = 0;
                input.y = System.Math.Sign(input.y);
                targetPos.x = actualPos.x;
                targetPos.y = actualPos.y + (int)input.y * -1;
            }
            else
            {
                input = Vector2.zero;
            }


            if (input != Vector2.zero)
            {
                startPosition = rBody.position;
                endPosition = new Vector3(startPosition.x + input.x * map.gridSize, startPosition.y + input.y * map.gridSize, startPosition.z);
                Rock targetRock = map.rockAt(targetPos.x, targetPos.y);
                switch (map.getTile(targetPos.x, targetPos.y))
                {
                    case MapLogic.GROUND:
                        actualPos.x = targetPos.x;
                        actualPos.y = targetPos.y;
                        moving = true;
                        sliding = false;
                        break;

                    case MapLogic.ICE:
                        if (targetRock != null)
                        {
                            endPosition = startPosition;
                            sliding = false;
                            targetRock.startRockMove(input);
                        }
                        else
                        {
                            actualPos.x = targetPos.x;
                            actualPos.y = targetPos.y;
                            moving = true;
                            sliding = true;
                        }
                        break;

                    case MapLogic.WALL:
                    case MapLogic.BARRIER:
                        sliding = false;
                        break;

                    case MapLogic.HOLE:
                        actualPos.x = targetPos.x;
                        actualPos.y = targetPos.y;
                        moving = true;
                        sliding = false;

                        map.holeGetsFilled(true);
                        break;
                }

            }
        }
    }
}
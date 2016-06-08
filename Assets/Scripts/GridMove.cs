using System.Collections;
using UnityEngine;

class GridMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float gridSize = 1f;
    public Camera cam;

    private enum MovementState
    {
        Sliding,
        Moving,
        Collided,
        Standing
    }

    private enum Orientation
    {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Horizontal;
    private MovementState movementState = MovementState.Standing;

    private Vector2 input;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t = 0;
    private float factor = 1f;
    Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
        endPosition = transform.position;
    }

    public void Update()
    {
        Debug.Log(movementState);
        anim.SetBool("is_walking", movementState == MovementState.Moving);
        anim.SetFloat("input_x", input.x);
        anim.SetFloat("input_y", input.y);

        

        switch (movementState)
        {
            case MovementState.Standing:
                input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                {
                    input.y = 0;
                    gridOrientation = Orientation.Horizontal;
                }
                else
                {
                    input.x = 0;
                    gridOrientation = Orientation.Vertical;
                }


                if (input != Vector2.zero)
                {
                    if (t < 1f)
                    {
                        t += Time.deltaTime * (moveSpeed / gridSize) * factor;
                    }
                    else
                    {
                        movementState = MovementState.Moving;
                        move();
                    }
                }
                break;

            case MovementState.Moving:
                if (t < 1f)
                {
                    t += Time.deltaTime * (moveSpeed / gridSize) * factor;
                    transform.position = Vector3.Lerp(startPosition, endPosition, t);
                    cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                }
                else
                {
                    movementState = MovementState.Standing;
                }
                break;

            case MovementState.Sliding:
                input = new Vector2(input.x, input.y);
                if (t < 1f)
                {
                    t += Time.deltaTime * (moveSpeed / gridSize) * factor;
                    transform.position = Vector3.Lerp(startPosition, endPosition, t);
                    cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                }
                else
                {
                    transform.position = endPosition;
                    move();
                }
                break;

        }
    }

    public void move()
    {
        startPosition = endPosition;
        t = 0;

        if (gridOrientation == Orientation.Horizontal)
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize, startPosition.y, startPosition.z);

        }
        else
        {
            endPosition = new Vector3(startPosition.x, startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        }
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Ice")
        {
            movementState = MovementState.Sliding;
        }
        if (coll.gameObject.tag == "Wall")
        {
            if(movementState == MovementState.Moving)
            {
                movementState = MovementState.Standing;
            }
            else
            {
                movementState = MovementState.Collided;
            }
            transform.position = startPosition;
        }
        
    }
}
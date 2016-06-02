using System.Collections;
using UnityEngine;

class GridMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float gridSize = 1f;
    public Camera cam;

    private enum Orientation
    {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Horizontal;
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    private Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor = 1f;
    Animator anim;

    private GameObject rock;
    private PolygonCollider2D coll;

    public void Start()
    {
        anim = GetComponent<Animator>();
        rock = GameObject.Find("Collision");
        coll = rock.GetComponent<PolygonCollider2D>();
    }

    public void Update()
    {

        anim.SetBool("is_walking", isMoving);
        anim.SetFloat("input_x", input.x);
        anim.SetFloat("input_y", input.y);

        if (!isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (!allowDiagonals)
            {
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
            }

            if (input != Vector2.zero)
            {
                if (t < 1f)
                {
                    t += Time.deltaTime * (moveSpeed / gridSize) * factor;
                }
                else
                {
                    move();
                }
            }
        }

        if(isMoving)
        {
            if(t < 1f) { 
                t += Time.deltaTime * (moveSpeed / gridSize) * factor;
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
                cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            }
            else
            {
                isMoving = false;
            }
        }
    }

    public void move()
    {
        isMoving = true;

        startPosition = transform.position;
        t = 0;

        if (gridOrientation == Orientation.Horizontal)
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);

        }
        else
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        }
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Collision")
        {
            isMoving = false;
            transform.position = startPosition;
        }
    }
}
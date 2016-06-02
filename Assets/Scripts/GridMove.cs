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
    private bool isColliding = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;
    Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
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
                StartCoroutine(move(transform));
            }
        }
    }

    public IEnumerator move(Transform transform)
    {
        isMoving = true;

        anim.SetBool("is_walking", true);
        anim.SetFloat("input_x", input.x);
        anim.SetFloat("input_y", input.y);

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

        if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0)
        {
            factor = 0.7071f;
        }
        else
        {
            factor = 1f;
        }

        while (t < 1f)
        {
            t += Time.deltaTime * (moveSpeed / gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            yield return null;
            if (isColliding)
            {
                break;
            }
        }

        anim.SetBool("is_walking", false);
        isMoving = false;
        isColliding = false;
        yield return 0;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        isColliding = true;
    }
}
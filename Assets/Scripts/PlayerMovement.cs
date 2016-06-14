using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    
	Rigidbody2D rBody;
	Animator anim;
    Camera cam;
    Vector2 latestVector;
    public GameObject sword;
    bool swordSwinging = false;

	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        cam = Camera.FindObjectOfType<Camera>();
    }
	
	// Update is called once per frame
	void Update () {

		Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //on or off, not linear

        if(movementVector != Vector2.zero)
        {
            latestVector = movementVector;
            anim.SetBool("is_walking", true);
            anim.SetFloat("input_x", movementVector.x);
            anim.SetFloat("input_y", movementVector.y);
        }
        else
        {
            anim.SetBool("is_walking", false);
        }

        rBody.MovePosition(rBody.position + movementVector * Time.deltaTime);
        cam.transform.position = new Vector3(rBody.position.x, rBody.position.y, -10);

        if (Input.GetKeyDown("space") && !swordSwinging)
        {
            swordSwinging = true;
            var s = (GameObject)Instantiate(sword, rBody.position + new Vector2(0.4f,0), new Quaternion());
            s.transform.parent = transform;
        }
    }

    public void EndSwordSwing()
    {
        swordSwinging = false;
    }
}

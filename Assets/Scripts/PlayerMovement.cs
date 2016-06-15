using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    
	private Rigidbody2D rBody;
	private Animator anim;
    private Camera cam;
    private Vector3 camTarget;
    private bool camOnBarrier;
    private bool barrierDestroyed;
    private float targetTime;
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
            anim.SetBool("is_walking", true);
            anim.SetBool("facing_right", movementVector.x > 0);
        }
        else
        {
            anim.SetBool("is_walking", false);
        }

        rBody.MovePosition(rBody.position + movementVector * Time.deltaTime);

        // Smooth camera 

        float dampTime = 0.08f;
        Vector3 velocity = Vector3.zero;

        if(camOnBarrier && Time.time > targetTime - 0.5f && !barrierDestroyed)
        {
            Destroy(GameObject.Find("Barrier"));
            barrierDestroyed = true;
        }
        if(camOnBarrier && Time.time > targetTime)
        {
            camOnBarrier = false;
            camTarget = transform.position;
        }
        if(!camOnBarrier) camTarget = transform.position;

        Vector3 point = cam.WorldToViewportPoint(camTarget);
        Vector3 delta = camTarget - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = cam.transform.position + delta;
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, destination, ref velocity, dampTime);

        if (Input.GetKeyDown("space") && !swordSwinging)
        {
            swordSwinging = true;
            var s = (GameObject)Instantiate(sword, rBody.position + new Vector2(0.4f, 0), new Quaternion());
            s.transform.parent = transform;
        }

    }

    public void EndSwordSwing()
    {
        swordSwinging = false;
    }

    public void cameraOnBarrier()
    {
        camOnBarrier = true;
        targetTime = Time.time + 1f;
        this.camTarget = new Vector3(16 * 0.32f, -11 * 0.32f, 0);
    }
 }

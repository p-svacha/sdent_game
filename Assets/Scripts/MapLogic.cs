using UnityEngine;
using System.Collections;

public class MapLogic : MonoBehaviour {

    private Camera cam;
    private Vector3 camTarget;
    private GameObject player;
    private bool camOnBarrier;
    bool destroyed;
    private int camBarrier;
    private float targetTime;

    public const int GROUND = 0;
    public const int ICE = 1;
    public const int WALL = 2;
    public const int BARRIER = 3;
    public const int HOLE = 4;

    public float gridSize = 0.32f;
    private float dampTime = 0.08f;
    private Vector3 velocity = Vector3.zero;

    private int[,] map;

    private Rock[] rock;

    int[,] rockpos =
{
            {16,21,18,4 },
            {16,17,3,13 }
     };

    // Use this for initialization
    void Start () {
        map = new int[,] {
            { 2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2 },
            { 2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,2,2,1,1,1,2,2,2,2,2,2,2,2 },
            { 2,2,1,2,1,1,1,1,1,1,2,2,3,0,0,2,2,1,2,0,1,1,1,1,1,1,2,2 },
            { 2,2,1,1,1,2,1,1,1,1,1,1,3,0,0,2,1,0,1,1,2,1,1,1,1,1,2,2 },
            { 2,2,1,1,1,1,1,1,1,1,1,1,3,0,0,2,1,2,2,1,1,1,1,1,2,1,2,2 },
            { 2,2,1,0,0,1,1,0,1,1,2,2,2,2,2,2,1,2,4,1,1,1,1,1,1,1,2,2 },
            { 2,2,1,0,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,0,1,1,2,1,1,1,2,2 },
            { 2,2,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,2,1,2,2,1,1,1,1,1,2,2 },
            { 2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,3,3,3,3,2,2 },
            { 2,2,2,2,2,1,1,4,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2 },
            { 2,2,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2 },
            { 2,2,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,2,3,3,3,2,2,2 },
            { 2,2,1,1,0,1,1,1,1,2,1,1,0,0,0,0,1,1,2,1,1,1,1,1,1,1,2,2 },
            { 2,2,1,0,1,1,1,1,2,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,2,2 },
            { 2,2,1,0,0,1,1,1,1,1,2,3,3,3,3,3,3,2,1,1,1,1,1,1,1,2,2,2 },
            { 2,2,1,1,1,1,1,1,2,1,2,0,0,0,0,0,0,2,2,2,1,1,2,1,1,1,2,2 },
            { 2,2,1,1,1,1,2,1,1,1,2,0,0,0,0,0,1,1,4,2,1,1,1,1,1,1,2,2 },
            { 2,2,1,1,1,2,1,1,1,1,2,0,0,0,0,0,0,1,1,2,0,1,1,1,1,4,2,2 },
            { 2,2,2,2,2,2,2,2,2,2,1,1,1,0,0,0,1,1,2,1,0,2,2,2,2,2,2,2 },
            { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2 }
        };

        cam = Camera.FindObjectOfType<Camera>();
        player = GameObject.Find("Player");
        setUpRocks();
        
    }

    public void Update()
    {

        // Smooth camera 

        Vector3 point = cam.WorldToViewportPoint(camTarget);
        Vector3 delta = camTarget - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, cam.transform.position + delta, ref velocity, dampTime);

        // Camera to barrier
        if (camOnBarrier)
        {
            if(Time.time > targetTime - 0.5f && !destroyed)
            {
                destroyed = true;
                Destroy(GameObject.Find("Barrier " + camBarrier));

            }
            if(Time.time > targetTime)
            {
                camOnBarrier = false;
                camTarget = GameObject.Find("Player").transform.position;
                destroyed = false;
            }
        }
        else
        {
            camTarget = player.transform.position;
        }
    }


    private void setUpRocks()
    {
        rock = new Rock[4];



        for (int i = 0; i < 4; i++)
        {
            rock[i] = GameObject.Find("rock" + i).GetComponent<Rock>();

            rock[i].actualPos = new Position(rockpos[0,i], rockpos[1,i]);
        }

        for (int i = 0; i < 4; i++)
        {
            rock[i].rBody.position = new Vector2((rock[i].actualPos.x + 0.5f) * gridSize, -(rock[i].actualPos.y + 0.5f) * gridSize);
        }
    }
	
    public int getTile(int x, int y)
    {
        return map[y, x];
    }

    public Rock rockAt(int x, int y)
    {
        for(int i = 0; i < rock.Length; i++)
        {
            if (rock[i].actualPos.x == x && rock[i].actualPos.y == y) return rock[i];
        }
        return null;
    }

    public void holeGetsFilled(Position pos)
    {

        //replace hole with ground
        map[pos.y, pos.x] = MapLogic.GROUND;


        //replace right Barrier with ice
        switch (pos.y)
        {
            case 16:
                for (var i = 11; i <= 16; i++)
                {
                    map[14, i] = MapLogic.ICE;
                }
                camTarget = new Vector3(14 * 0.32f, -14 * 0.32f, 0);
                camOnBarrier = true;
                camBarrier = 1;
                targetTime = Time.time + 1f;
                break;
            case 17:
                for (var i = 22; i <= 24; i++)
                {
                    map[11, i] = MapLogic.ICE;
                }
                camTarget = new Vector3(23 * 0.32f, -11 * 0.32f, 0);
                camOnBarrier = true;
                camBarrier = 2;
                targetTime = Time.time + 1f;
                break;
            case 9:
                for (var i = 22; i <= 25; i++)
                {
                    map[8, i] = MapLogic.ICE;
                }
                camTarget = new Vector3(23 * 0.32f, -8 * 0.32f, 0);
                camOnBarrier = true;
                camBarrier = 3;
                targetTime = Time.time + 1f;
                break;
            case 5:
                for (var i = 2; i <= 4; i++)
                {
                    map[i, 12] = MapLogic.ICE;
                }
                camTarget = new Vector3(12 * 0.32f, -3 * 0.32f, 0);
                camOnBarrier = true;
                camBarrier = 4;
                targetTime = Time.time + 1f;
                break;
        }

    }
}


public enum Orientation
{
    Horizontal,
    Vertical
};

public class Position
{
    public int x { get; set; }
    public int y { get; set; }

    public Position() { }

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

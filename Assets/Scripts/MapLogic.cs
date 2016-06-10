using UnityEngine;
using System.Collections;

public class MapLogic : MonoBehaviour {

    public const int GROUND = 0;
    public const int ICE = 1;
    public const int WALL = 2;
    public const int BARRIER = 3;
    public const int HOLE = 4;

    public float gridSize = 0.32f;

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
            { 2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2 },
            { 2,2,1,2,1,1,1,1,1,1,2,2,3,0,0,2,2,1,2,1,1,1,1,1,1,1,2,2 },
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

        setUpRocks();
        
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

    public void holeGetsFilled(bool isPlayer)
    {
        if (isPlayer)
        {
            //reset player
        } else
        {
            //destroy rock
        }

        //replace hole with ground
        //replace right Barrier with ice
    }

    public void update()
    {

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

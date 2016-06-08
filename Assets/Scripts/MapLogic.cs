﻿using UnityEngine;
using System.Collections;

public class MapLogic : MonoBehaviour {

    public const int GROUND = 0;
    public const int ICE = 1;
    public const int WALL = 2;
    public const int BARRIER = 3;
    public const int HOLE = 4;

    private int[,] map;

    private Rock[] rock;

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

        rock = new Rock[4];

        for (int i = 0; i < 4; i++)
        {
            rock[i] = GameObject.Find("rock" + i).GetComponent<Rock>();
            Debug.Log("success");
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
            if (rock[i].x == x && rock[i].y == y) return rock[i];
        }
        return null;
    }

    public void moveRock(Rock r, int x, int y)
    {
        r.moveTo(x, y);
    }

    public void update()
    {

    }

}
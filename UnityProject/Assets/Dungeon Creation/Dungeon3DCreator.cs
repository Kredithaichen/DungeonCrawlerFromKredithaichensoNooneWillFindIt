using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon3DCreator : MonoBehaviour
{
    [SerializeField]
    private int width, height;

    [SerializeField]
    private GameObject cubeGameObject;
    [SerializeField]
    private GameObject floorGameObject;

    private int[,] map;

    // Use this for initialization
    void Start ()
    {
        map = new int[width, height];

        var floor = Instantiate(floorGameObject, new Vector3((width - 1) / 2f, 0, (height - 1) / 2f), new Quaternion());
        floor.transform.localScale = new Vector3(width, 1, height);

        AddHorizontalWall(0, width, 0);
        AddHorizontalWall(0, width, height - 1);

        AddVerticalWall(0, height, 0);
        AddVerticalWall(0, height, width - 1);

        AddHorizontalWall(2, 8, 4);

        GenerateMap();
    }

    private void GenerateMap()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                HandleMapElement(x, y);
    }

    private void HandleMapElement(int x, int y)
    {
        if (map[x, y] == 1)
            Instantiate(cubeGameObject, new Vector3(x, 1, y), new Quaternion());
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    private void AddHorizontalWall(int x1, int x2, int y)
    {
        for (int x = x1; x < x2; x++)
            map[x, y] = 1;
    }

    private void AddVerticalWall(int y1, int y2, int x)
    {
        for (int y = y1; y < y2; y++)
            map[x, y] = 1;
    }
}

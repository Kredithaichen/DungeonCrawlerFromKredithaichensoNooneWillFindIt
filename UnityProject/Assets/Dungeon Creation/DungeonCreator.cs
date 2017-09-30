using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    [SerializeField]
    private int width, height;

    [SerializeField]
    private GameObject drawingTarget;

    [SerializeField]
    private int seed;

    private System.Random randomGenerator;
    private Renderer renderer;

    private int[,] map;

	// Use this for initialization
	void Start()
	{
        randomGenerator = new System.Random(seed);

        map = new int[width, height];
        GenerateMap();

	    renderer = drawingTarget.GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    private void GenerateMap()
    {
        AddHorizontalWall(1, 78, 10);
        AddVerticalWall(23, 67, 4);
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

    private void AddDiagonalWall(int x1, int y1, int x2, int y2)
    {
        
    }

    private Texture2D GenerateTexture()
    {
        var texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                texture.SetPixel(x, y, GenerateColor(x, y));
            }
        }

        texture.Apply();
        return texture;
    }

    private Color GenerateColor(int x, int y)
    {
        //var c = (float)randomGenerator.NextDouble();

        var c = 0f;

        if (map[x, y] == 1)
            c = 1f;

        return new Color(c, c, c);
    }

    // Update is called once per frame
	void Update()
    {
        
    }
}

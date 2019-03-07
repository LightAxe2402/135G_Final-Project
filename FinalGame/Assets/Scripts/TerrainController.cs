using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public int width = 100;
    public int height = 100;

    //private Terrain terrain;
    public float depth;
    public float holeWidth = 0.75f;
    public float speed = 1;
    public float scale = 20.0f;

    private Terrain terrain;
    private float counter = 0;

    private void Start()
    {
        terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x,y);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;

        return Mathf.Sign(Mathf.Sin(xCoord + counter) + holeWidth)*depth;
    }

    private void Update()
    {
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        counter += speed;
    }
}

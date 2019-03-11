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
    public int period = 2;
    public int pitWidthMin;
    public int pitWidthMax;
    public int platformWidthMin;
    public int platformWidthMax;
    public bool generate = true;

    private float[,] heights;
    private int deltaX = 0;
    private Terrain terrain;
    public float counter = 0;
    //private Material mat;

    private int pitWidth = 0;
    private int pitWidthCounter = 0;
    private int platformWidth = 0;
    private int platformWidthCounter = 0;
    private bool makingPlatform = true;
    private bool generateBuffer = false;

    private void Start()
    {
        heights = new float[width, height];
        for(int w = 0; w < width; w++)
        {
            for(int h = 0; h < height; h++)
            {
                heights[w, h] = 1.0f;
            }
        }
        pitWidth = Random.Range(pitWidthMin, pitWidthMax);
        platformWidth = Random.Range(platformWidthMin, platformWidthMax);

        terrain = GetComponent<Terrain>();
        terrain.terrainData.heightmapResolution = width + 1;
        terrain.terrainData.size = new Vector3(width, depth, height);
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        for(int w = 0; w < width-1; w++)
        {
            for(int h = 0; h < height; h++)
            {
                heights[w, h] = heights[w + 1, h];
            }
        }

        float newHeight = GenerateHeights();
        for (int j = 0; j < height; j++)
        {
            heights[width - 1, j] = newHeight;
        }

        terrainData.SetHeights(0, 0, heights);
        return terrainData;
    }

    float GenerateHeights()
    {
        if(generateBuffer == true)
        {
            return 1 * depth;
        }
        else if(makingPlatform == true)
        {
            platformWidthCounter++;
            if(platformWidthCounter >= platformWidth)
            {
                makingPlatform = false;
                platformWidthCounter = 0;
                platformWidth = Random.Range(platformWidthMin, platformWidthMax);
            }
            return 1 * depth;
        }
        else
        {
            pitWidthCounter++;
            if(pitWidthCounter >= pitWidth)
            {
                makingPlatform = true;
                pitWidthCounter = 0;
                pitWidth = Random.Range(pitWidthMin, pitWidthMax);
            }
            return 0;
        }
    }

    TerrainData GenerateTerrainOld(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeightsOld());
        return terrainData;
    }

    float[,] GenerateHeightsOld()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeightOld(x,y);
                deltaX++;
            }
        }
        return heights;
    }

    float CalculateHeightOld(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;
        if(deltaX > Mathf.PI * period)
        {
            deltaX = 0;
            period = Random.Range(1, 4);
        }
        return Mathf.Sign(Mathf.Sin(xCoord + counter) + holeWidth*(Mathf.Sin(4*Mathf.PI*xCoord)+1))*depth;
    }

    private void Update()
    {
        counter += speed;
        if (generate)
        {
            terrain.terrainData = GenerateTerrain(terrain.terrainData);
        }
    }
    
    public void generateBufferTerrain()
    {
        generateBuffer = true;
    }

    public void stopBufferTerrainGen()
    {
        generateBuffer = false;
    }
}

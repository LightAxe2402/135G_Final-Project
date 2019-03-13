using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public int width = 100;
    public int height = 100;

    public float depth;
    public float speed = 1;
    public int pitWidthMin;
    public int pitWidthMax;
    public int platformWidthMin;
    public int platformWidthMax;
    public bool generate = true;

    private float[,] heights;
    private Terrain terrain;
    public float counter = 0;

    private int pitWidth = 0;
    private int pitWidthCounter = 0;
    private int platformWidth = 0;
    private int platformWidthCounter = 0;
    private bool makingPlatform = true;
    private bool generateBuffer = false;
    private float terrainTexOffset = 0;
    private float[] ones;
    private float[] zeroes;
    private float[][] newArrayHeights;

    private void Start()
    {
        ones = new float[256];
        zeroes = new float[256];
        newArrayHeights = new float[256][];
        for (int i = 0; i < 256; i++)
        {
            ones[i] = depth;
            zeroes[i] = 0.0f;
            newArrayHeights[i] = ones;
        }
        heights = new float[width, height];
        pitWidth = Random.Range(pitWidthMin, pitWidthMax);
        platformWidth = Random.Range(platformWidthMin, platformWidthMax);

        terrain = GetComponent<Terrain>();
        terrain.terrainData.heightmapResolution = width + 1;
        terrain.terrainData.size = new Vector3(width, depth, height);
        convertHeightsArray();
        terrain.terrainData.SetHeights(0, 0, heights);
    }

    private void generateHeights()
    {
        shiftHeightsArray();
        updateTerrainArray();
        convertHeightsArray();
    }

    private void convertHeightsArray()
    {
        for (int w = 0; w < width - 1; w++)
        {
            for (int h = 0; h < height; h++)
            {
                heights[w, h] = newArrayHeights[w][h];
            }
        }
    }

    private void shiftHeightsArray()
    {
        for (int i = 0; i < width-1; i++)
        {
            newArrayHeights[i] = newArrayHeights[i + 1];
        }
    }

    private void updateTerrainArray()
    {
        if (generateBuffer == true)
        {
            newArrayHeights[width-1] = ones;
        }
        else if (makingPlatform == true)
        {
            platformWidthCounter++;
            if (platformWidthCounter >= platformWidth)
            {
                makingPlatform = false;
                platformWidthCounter = 0;
                platformWidth = Random.Range(platformWidthMin, platformWidthMax);
            }
            newArrayHeights[width - 1] = ones;
        }
        else
        {
            pitWidthCounter++;
            if (pitWidthCounter >= pitWidth)
            {
                makingPlatform = true;
                pitWidthCounter = 0;
                pitWidth = Random.Range(pitWidthMin, pitWidthMax);
            }
            newArrayHeights[width - 1] = zeroes;
        }
    }

    private void Update()
    {
        counter += speed;
        terrainTexOffset += 0.0191f;
        if (generate)
        {
            terrain.materialTemplate.mainTextureOffset = new Vector2(0,terrainTexOffset);
            generateHeights();
            terrain.terrainData.SetHeights(0, 0, heights);
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

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject terrainObj;
    public GameObject player;
    public GameObject spawnPoint;
    public GameObject spawnObject;
    public GameObject destination;
    public float minSpawnTime = 1.0f;
    public float maxSpawnTime = 3.0f;
    public float spawnRangeY = 3.0f;
    public float score = 0.0f;
    public float distanceToObjective = 10f;
    public float bufferDistance = 1f;
    public Text winText;
    public Text scoreText;
    public Text continueText;
    public string nextLevelName;

    private playerController pc;
    private TerrainController terrainScript;
    private float counter = 0f;
    private float spawnTime;
    private bool finished = false;
    private bool canContinue = false;

    private void Start()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        pc = player.GetComponent<playerController>();
        winText.text = "";
        continueText.text = "";
        terrainScript = terrainObj.GetComponent<TerrainController>();
    }

    public void stopTerrain()
    {
        terrainScript.generate = false;
    }

    private void Update()
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if(Input.GetKey("return") && canContinue == true)
        {
            Debug.Log("pressed enter");
            SceneManager.LoadScene(nextLevelName);
        }
        counter += Time.deltaTime;
        if(counter >= spawnTime && finished == false)
        {
            spawnPickup();
            counter = 0;
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }

        if (terrainScript.counter >= distanceToObjective)
        {
            winText.text = "Destination Reached!";
            continueText.text = "Press Enter To Continue";
            stopTerrain();
            pc.disableEffects();
            destination.GetComponent<Animator>().SetBool("destinationReached", true);
            canContinue = true;
            Debug.Log("canContinue: " + canContinue);
        }
        else if (terrainScript.counter >= distanceToObjective-bufferDistance)
        {
            terrainScript.generateBufferTerrain();
            finished = true;
            //code for scene completions
        }

        scoreText.text = "Score: " + score;
        
    }
    void spawnPickup()
    {
        Vector3 spawnPosition = new Vector3(spawnPoint.transform.position.x,spawnPoint.transform.position.y + Random.Range(-spawnRangeY, spawnRangeY),spawnPoint.transform.position.z);
        Instantiate(spawnObject,spawnPosition,Quaternion.identity);
    }
}

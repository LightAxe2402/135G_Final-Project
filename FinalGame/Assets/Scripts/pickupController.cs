using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupController : MonoBehaviour
{
    public ParticleSystem collectionEffect;
    public float speed = 2.0f;
    public float rotationSpeedMin = 1.0f;
    public float rotationSpeedMax = 15.0f;
    public float scoreValue = 10.0f;

    private Rigidbody rb;
    private GameObject[] gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectsWithTag("GameController");
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0,0,-speed));
        rb.AddRelativeTorque(new Vector3(Random.Range(rotationSpeedMin, rotationSpeedMax), Random.Range(rotationSpeedMin, rotationSpeedMax), Random.Range(rotationSpeedMin, rotationSpeedMax)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(gameManager[0] != null)
            {
                gameManager[0].GetComponent<GameManagerScript>().score += scoreValue;
                Instantiate(collectionEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                Destroy(gameObject);
                
            }
        }
    }

}

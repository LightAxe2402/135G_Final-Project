using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Controller3D : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float moveForce = 5f;
    [SerializeField] private float pushForce = 2f;
    [SerializeField] private string targetRoom = "";

    private Rigidbody rb;
    private Vector3 input = Vector3.zero;
    private bool pushing = false;

    /// <summary>
    /// Getting Private References
    /// </summary>
    private void Awake()
    {
        // Getting the RigidBody reference
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Resetting Player States
    /// </summary>
    private void Update()
    {
        if (input == Vector3.zero)
            pushing = false;
    }

    /// <summary>
    /// Getting Input and Adding the input force to the RigidBody
    /// </summary>
    private void FixedUpdate()
    {
        // Applying Force Based on Input Recieved from GetDirectionInput
        input = GetDirectionInput();
        rb.velocity = input * ((!pushing) ? moveForce : pushForce);
    }

    /// <summary>
    /// Checks for entering a door
    /// Checks for entering an enemy sight radius
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // Getting the Object
        GameObject obj = other.gameObject;

        // Determining the Object
        if (obj.tag == "Exit") {
            if (obj.GetComponent<Exit>().DoorOpened()) {
                Debug.Log("Level Completed");
                SceneManager.LoadScene(targetRoom);
            }
        }

        if (obj.tag == "pickup")
        {
            Debug.Log("Got a Key");
            obj.GetComponent<Key>().UnlockDoor();
        }

        if (obj.tag == "Enemy") {
            Debug.Log("Enemy Spotted You");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Spotted You");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.gameObject.tag == "PushBlock")
        {
            if (input != Vector3.zero)
            {
                Debug.Log("Pushing");
                pushing = true;

                collision.transform.position += (input * pushForce * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// Getting and Returning Input based on Horizontal and Vertical
    /// Axis Input
    /// </summary>
    /// <returns></returns>
    private Vector3 GetDirectionInput()
    {
        // Setting the Input tracker variable
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            return new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            return new Vector3(0f, 0f, Input.GetAxis("Vertical"));


        // Returning the input
        return Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Controller3D : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float moveForce = 5f;

    private Rigidbody rb;

    /// <summary>
    /// Getting Private References
    /// </summary>
    private void Awake()
    {
        // Getting the RigidBody reference
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Getting Input and Adding the input force to the RigidBody
    /// </summary>
    private void FixedUpdate()
    {
        // Applying Force Based on Input Recieved from GetDirectionInput
        Vector3 input = GetDirectionInput();
        rb.velocity = input * moveForce;
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

                /* Code for level transfer go here... */
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

    /// <summary>
    /// Getting and Returning Input based on Horizontal and Vertical
    /// Axis Input
    /// </summary>
    /// <returns></returns>
    private Vector3 GetDirectionInput()
    {
        // Setting the Input tracker variable
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        // Returning the input
        return dir;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float gravity;
    public float jumpSpeed;

    private float verticalSpeed;
    private Rigidbody rb;
    private bool touchingGround = false;
    private Quaternion baseRotation = Quaternion.Euler(0, 0, 0);
    GameObject parent;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        parent = transform.parent.gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("pressed space");
            if (touchingGround == true)
            {
                verticalSpeed += jumpSpeed;
                touchingGround = false;
            }
        }

        verticalSpeed += gravity;

        Vector3 currentPosition = new Vector3(parent.transform.position.x, parent.transform.position.y+0.01f, parent.transform.position.z + 4.5f);
        Vector3 desiredPosition = new Vector3(0, verticalSpeed, 0);
        Vector3 direction = Vector3.up*verticalSpeed;

        Ray ray = new Ray(currentPosition, desiredPosition);
        RaycastHit hit;
        Quaternion rotation = Quaternion.Euler(baseRotation.eulerAngles.x - verticalSpeed * 10, baseRotation.eulerAngles.y, baseRotation.eulerAngles.z);
        //Debug.DrawRay(currentPosition, desiredPosition, Color.green);

        if (!Physics.Raycast(ray, out hit, direction.magnitude) || hit.transform.tag != "ground")
        {
            if(touchingGround == true)
            {
                touchingGround = false;
            }
            parent.transform.position += desiredPosition;
            parent.transform.rotation = rotation;
        }
        else
        {
            parent.transform.position = new Vector3(parent.transform.position.x, hit.point.y+0.01f, parent.transform.position.z);
            parent.transform.rotation = baseRotation;
            verticalSpeed = 0;
            touchingGround = true;
        }  
    }

    public void disableEffects()
    {
        GetComponent<LineRenderer>().enabled = false;
        var emission = GetComponent<ParticleSystem>().emission;
        emission.enabled = false;
    }
}

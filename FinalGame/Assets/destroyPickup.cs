using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "pickup")
        {
            Destroy(other.gameObject);
        }
    }
}

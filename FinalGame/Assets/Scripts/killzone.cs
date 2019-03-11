using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class killzone : MonoBehaviour
{
    public GameObject myParticleSystem;
    public GameObject gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.GetComponent<GameManagerScript>().stopTerrain();
            Instantiate(myParticleSystem, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), Quaternion.identity);
        }
    }
}

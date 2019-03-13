using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class behavior : MonoBehaviour
{
    private void Update()
    {
        ParticleSystem particles = GetComponent<ParticleSystem>();

        if(!particles.IsAlive())
        {
            //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

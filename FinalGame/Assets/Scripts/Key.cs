using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [Header("Door Reference")]
    [SerializeField] private Exit exitReference = null;

    /// <summary>
    /// Unlocks the Referenced Door
    /// </summary>
    public void UnlockDoor()
    {
        exitReference.UnlockDoor();
        GameObject.Destroy(gameObject);
    }

}

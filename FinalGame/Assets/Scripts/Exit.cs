using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [Header("Door State Variables")]
    [SerializeField] private bool doorLocked = true;

    public bool DoorOpened() { return !doorLocked; }

    public void UnlockDoor() {
        doorLocked = false;
    }

}

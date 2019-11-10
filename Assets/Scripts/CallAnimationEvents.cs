using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAnimationEvents : MonoBehaviour
{
    PlayerThrow playerThrow;
    // Start is called before the first frame update
    void Start()
    {
        playerThrow = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerThrow>();
    }

    void Throw()
    {
        playerThrow.ThrowPickup();
    }

    void Pickup()
    {
        playerThrow.GrabPickup();
    }

    void Drop()
    {
        playerThrow.DropPickup();
    }
}

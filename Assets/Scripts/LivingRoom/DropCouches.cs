using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCouches : MonoBehaviour
{
    public GameObject falseFloor;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(falseFloor);
        }
    }


}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningRoomController : MonoBehaviour
{
    public GameObject[] plates;
    public int platesLeft;
    // Start is called before the first frame update
    void Start()
    {
        plates = GameObject.FindGameObjectsWithTag("Plate");
        platesLeft = plates.Length;
        Debug.Log(platesLeft);
    }

    private void Update()
    {
        if(platesLeft == 0)
        {
            Debug.Log("DONE");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Plate")
        {
            platesLeft--;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Plate")
        {
            platesLeft++;
        }
    }
}
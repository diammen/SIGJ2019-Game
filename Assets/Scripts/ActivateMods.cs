﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateMods : MonoBehaviour
{
    public AudioSource source;
    public AudioClip eatModuleSound;
    public AudioClip memoryModulePickup;
    public Text dialogue;

    //Timer Scripts for the text.
    public float lastingTime = 10.0f;
    private float countTime = 0.0f;
    private bool timerOn = false;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Module")
        {
            timerOn = true;
            source.PlayOneShot(eatModuleSound);
            if (collision.gameObject.name == "ActivateRotation")
            {
                GetComponent<PlayerMove>().rotationModuleOn = true;
                dialogue.text = "ROTATION MODULE ACTIVATED (Left Stick to rotate)";
                if(countTime >= lastingTime /2)
                {
                    dialogue.text = "Why is my rotation module on the floor... never mind. I wonder what my humans are up to in the living room?";
                }
            }
            else if(collision.gameObject.name == "ActivateTranslation")
            {
                GetComponent<PlayerMove>().translationModuleOn = true;
            }
            else if(collision.gameObject.name == "ActivateThrow")
            {
                GetComponent<PlayerThrow>().canThrow = true;
                dialogue.text = "THROW MODULE ACTIVATED (A to throw after picking up objects)";
                if (countTime >= lastingTime / 2)
                {
                    dialogue.text = "Strange... I really hope my humans are OK. I hope Alex is ok. I'll go check up on them.";
                }
            }
            else if (collision.gameObject.name == "ActivatePickUp")
            {
                GetComponent<PlayerThrow>().canPickUp = true;

                dialogue.text = "PICK UP MODULE ACTIVATED (B to pick up/drop)";
                if (countTime >= lastingTime / 2)
                {
                    dialogue.text = "What's my pick up module doing here? Something's not right, but I should still cook dinner for my humans.";
                }
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("MemoryModule"))
        {
            source.PlayOneShot(memoryModulePickup);
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        if(timerOn)
        {
            countTime += Time.deltaTime;
            if(countTime >= lastingTime)
            {
                dialogue.text = " ";
            }
        }
    }
}

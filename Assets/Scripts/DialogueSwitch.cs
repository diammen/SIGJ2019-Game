using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSwitch : MonoBehaviour
{
    public GameObject textManager;
    public string[] dialogue;
    public bool active = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!active && other.gameObject.CompareTag("Player"))
        {
            textManager.GetComponent<DialogueManager>().setTextToShow(dialogue[0], dialogue[1]);
            active = true;
        }
    }
}

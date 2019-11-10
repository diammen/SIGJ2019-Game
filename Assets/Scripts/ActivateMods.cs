using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ActivateMods : MonoBehaviour
{
    public AudioSource source;
    public AudioClip eatModuleSound;
    public AudioClip memoryModulePickup;
    public PlayableDirector director;

    public Text dialogue;
    public string[] displayText;

    //Timer Scripts for the text.
    public float lastingTime = 10.0f;
    private float countTime = 0.0f;
    private bool timerOn = false;

    Animator anim;
    WaitForSeconds waitForJingle;

    private void Start()
    {
        waitForJingle = new WaitForSeconds(memoryModulePickup.length);
        anim = GetComponentInChildren<Animator>();
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Module")
        {
            timerOn = true;
            source.PlayOneShot(eatModuleSound);
            anim.SetTrigger("EatModule");
            if (collision.gameObject.name == "ActivateRotation")
            {
                GetComponent<PlayerMove>().rotationModuleOn = true;
                //displayText[0] = "ROTATION MODULE ACTIVATED (Left Stick to rotate)";

                //displayText[1] = "Why is my rotation module on the floor... \n never mind. I wonder what my humans are up to in the living room?";


            }
            else if (collision.gameObject.name == "ActivateTranslation")
            {
                GetComponent<PlayerMove>().translationModuleOn = true;
            }
            else if (collision.gameObject.name == "ActivateThrow")
            {
                GetComponent<PlayerThrow>().canThrow = true;
                displayText[0] = "THROW MODULE ACTIVATED (A to throw after picking up objects)";

                displayText[1] = "Strange... I really hope my humans are OK. \n I hope Alex is ok. I'll go check up on them.";

            }
            else if (collision.gameObject.name == "ActivatePickUp")
            {
                GetComponent<PlayerThrow>().canPickUp = true;

                displayText[0] = "PICK UP MODULE ACTIVATED (B to pick up/drop)";

                displayText[1] = "What's my pick up module doing here? \n Something's not right, but I should still cook dinner for my humans.";

            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("MemoryModule"))
        {
            source.PlayOneShot(memoryModulePickup);
            anim.SetTrigger("EatModule");
            StartCoroutine(WaitForJingle());
            collision.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
    private void Update()
    {
        if (timerOn)
        {
            countTime += Time.deltaTime;

            displayDialogue();

            if (countTime >= lastingTime)
            {


                dialogue.text = " ";
                countTime = 0.0f;
                timerOn = false;
            }
        }
    }


    void displayDialogue()
    {
        //dialogue.text = displayText[0];
        //if (countTime >= lastingTime / 2)
        //{
        //    dialogue.text = displayText[1];
        //}


    }
    IEnumerator WaitForJingle()
    {
        yield return waitForJingle;
        director.Play();
        Time.timeScale = 0;

    }
}

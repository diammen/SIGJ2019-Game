using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMods : MonoBehaviour
{
    public AudioSource source;
    public AudioClip eatModuleSound;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Module")
        {
            source.PlayOneShot(eatModuleSound);
            if (collision.gameObject.name == "ActivateRotation")
            {
                GetComponent<PlayerMove>().rotationModuleOn = true;

            }
            else if(collision.gameObject.name == "ActivateTranslation")
            {
                GetComponent<PlayerMove>().translationModuleOn = true;
            }
            else if(collision.gameObject.name == "ActivateThrow")
            {
                GetComponent<PlayerThrow>().canThrow = true;
            }
            else if (collision.gameObject.name == "ActivatePickUp")
            {
                GetComponent<PlayerThrow>().canPickUp = true;
            }
            Destroy(collision.gameObject);
        }
    }
}

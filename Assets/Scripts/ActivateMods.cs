using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivateMods : MonoBehaviour
{
    public AudioSource source;
    public AudioClip eatModuleSound;
    public AudioClip memoryModulePickup;
    public PlayableDirector director;

    WaitForSeconds waitForJingle;

    private void Start()
    {
        waitForJingle = new WaitForSeconds(memoryModulePickup.length);
    }
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
        if (collision.gameObject.CompareTag("MemoryModule"))
        {
            source.PlayOneShot(memoryModulePickup);
            StartCoroutine(WaitForJingle());
            collision.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    private IEnumerator WaitForJingle()
    {
        yield return waitForJingle;
        director.Play();
        Time.timeScale = 0;
    }
}

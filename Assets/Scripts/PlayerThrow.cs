using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public GameObject pickup;
    public Transform spawner;
    public AudioSource source;
    public AudioClip pickupSound, throwSound, dropSound, errorSound;
    public float throwforce;
    public bool canThrow = true;
    public bool canPickUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Hold down the button and charge up your shot.
        if (canThrow)
        {
            //Release the button and fire
            if (pickup != null && Input.GetAxis("Throw") > 0.5)
            {
                source.PlayOneShot(throwSound);
                pickup.gameObject.SetActive(true);
                pickup.GetComponent<Rigidbody>().AddForce(transform.forward * throwforce, ForceMode.Impulse);
                pickup.transform.parent = null;
                pickup = null;
            }
        }
        else if (!source.isPlaying && !canThrow && Input.GetAxis("Throw") != 0)
        {
            source.PlayOneShot(errorSound);
        }
        if (canPickUp)
        {
            if (pickup != null && Input.GetButtonDown("Drop"))
            {
                source.PlayOneShot(dropSound);
                pickup.gameObject.SetActive(true);
                pickup.transform.parent = null;
                pickup = null;
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (canPickUp)
        {
            if (!pickup && (collision.gameObject.tag == "PickUp" || collision.gameObject.tag == "Plate"))
            {
                source.PlayOneShot(pickupSound);
                pickup = collision.gameObject;
                pickup.GetComponent<Rigidbody>().velocity = Vector3.zero;
                pickup.transform.parent = transform;
                pickup.transform.position = spawner.position;
                pickup.transform.rotation = spawner.rotation;

                collision.gameObject.SetActive(false);

            }
        }
        else if (!source.isPlaying)
        {
            source.PlayOneShot(errorSound);
        }
    }
}
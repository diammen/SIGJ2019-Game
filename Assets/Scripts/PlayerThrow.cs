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

    GameObject cachedPickup;
    bool carryingPickup;

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
                pickup.GetComponent<Rigidbody>().AddForce(transform.up + transform.forward * throwforce, ForceMode.Impulse);
                pickup.transform.parent = null;
                pickup = null;
            }
        }
        else if (!source.isPlaying && !canThrow && carryingPickup && Input.GetAxis("Throw") != 0)
        {
            source.PlayOneShot(errorSound);
        }
        if (canPickUp)
        {
            if (!carryingPickup && cachedPickup != null && pickup == null && Input.GetButtonDown("Drop"))
            {
                GrabPickup();
            }
            else if (Input.GetButtonDown("Drop") && pickup != null)
            {
                source.PlayOneShot(dropSound);
                pickup.gameObject.SetActive(true);
                pickup.transform.parent = null;
                pickup = null;
                cachedPickup = null;
            }
            else if (pickup == null && carryingPickup)
            {
                carryingPickup = false;
            }
        }
        else if (Input.GetButtonDown("Drop") && !source.isPlaying)
        {
            source.PlayOneShot(errorSound);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (canPickUp)
        {
            if (collision.gameObject.tag == "PickUp" || collision.gameObject.tag == "Plate" )
            {
                cachedPickup = collision.gameObject;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        cachedPickup = null;
    }

    private void GrabPickup()
    {
        source.PlayOneShot(pickupSound);
        pickup = cachedPickup;
        pickup.GetComponent<Rigidbody>().velocity = Vector3.zero;
        pickup.transform.parent = transform;
        pickup.transform.position = spawner.position;
        pickup.transform.rotation = spawner.rotation;
        pickup.SetActive(false);

        carryingPickup = true;
    }
}
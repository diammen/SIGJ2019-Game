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
    Animator anim;
    bool carryingPickup;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Hold down the button and charge up your shot.
        if (canThrow)
        {
            //Release the button and fire
            if (pickup != null && Input.GetAxis("Throw") == 1)
            {
                print("throw");
                anim.SetTrigger("Throw");
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
                anim.SetBool("Pickup", true);
            }
            else if (Input.GetButtonDown("Drop") && pickup != null)
            {
                anim.SetTrigger("Drop");
            }
            else if (pickup == null && carryingPickup)
            {
                anim.SetBool("Pickup", false);
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

    public void GrabPickup()
    {
        source.PlayOneShot(pickupSound);
        pickup = cachedPickup;
        if (pickup != null)
        {
            pickup.GetComponent<Rigidbody>().velocity = Vector3.zero;
            pickup.GetComponent<Rigidbody>().isKinematic = true;
            pickup.GetComponent<Collider>().enabled = false;
            pickup.transform.parent = spawner.transform;
            pickup.transform.position = spawner.position;
            pickup.transform.rotation = spawner.rotation;
        }
        carryingPickup = true;
    }

    public void ThrowPickup()
    {
        source.PlayOneShot(throwSound);
        if (pickup != null)
        {
            pickup.GetComponent<Rigidbody>().isKinematic = false;
            pickup.GetComponent<Collider>().enabled = true;
            pickup.GetComponent<Rigidbody>().AddForce(transform.up * 3 + transform.forward * throwforce, ForceMode.Impulse);
            pickup.transform.parent = null;
            pickup = null;
        }
    }

    public void DropPickup()
    {
        source.PlayOneShot(dropSound);
        if (pickup != null)
        {
            pickup.GetComponent<Rigidbody>().isKinematic = false;
            pickup.GetComponent<Collider>().enabled = true;
            pickup.transform.parent = null;
            pickup = null;
            cachedPickup = null;
        }
    }
}
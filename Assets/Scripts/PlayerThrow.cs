using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawner;
    public float throwforce;
    public bool canThrow = true;
    public bool canPickUp = false;
  //  private bool canThrow = true;

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
            if (bullet != null && Input.GetAxis("Throw") > 0.5)
            {
                bullet.gameObject.SetActive(true);
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * throwforce, ForceMode.Impulse);
                bullet.transform.parent = null;
                bullet = null;
            }
        }
        if (canPickUp)
        {
            if (bullet != null && Input.GetButtonDown("Drop"))
            {
                bullet.gameObject.SetActive(true);
                bullet.transform.parent = null;
                bullet = null;
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (canPickUp)
        {
            if (!bullet && collision.gameObject.tag == "PickUp")
            {
                bullet = collision.gameObject;
                bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
                bullet.transform.parent = transform;
                bullet.transform.position = spawner.position;
                bullet.transform.rotation = spawner.rotation;

                collision.gameObject.SetActive(false);

            }
        }
    }
}
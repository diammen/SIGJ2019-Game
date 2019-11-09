using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBullet : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawner;
    public float bulletSpeed;
    public bool canThrow = true;
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
                GameObject spawnedBullet = Instantiate(bullet, spawner.position, spawner.rotation);

                spawnedBullet.GetComponent<Rigidbody>().velocity = spawner.transform.forward * bulletSpeed;
                Destroy(bullet);
                bullet = null;
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (canThrow)
        {
            if (!bullet && collision.gameObject.tag == "PickUp")
            {
                bullet = collision.gameObject;

                collision.gameObject.SetActive(false);

            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBullet : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawner;
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Hold down the button and charge up your shot.

        //Release the button and fire
        if (bullet != null && (Input.GetButtonDown("Fire1")))
        {
            GameObject spawnedBullet = Instantiate(bullet, spawner.position, spawner.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawner.transform.forward * bulletSpeed;
        }

    }
}

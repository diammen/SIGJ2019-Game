using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningRoomController : MonoBehaviour
{
    public GameObject[] plates;
    public int platesLeft;
    public GameObject floatingPlatform;
    // Start is called before the first frame update
    void Start()
    {
        plates = GameObject.FindGameObjectsWithTag("Plate");
        platesLeft = plates.Length;
    }

    private void Update()
    {
        if(platesLeft == 0)
        {
            Destroy(floatingPlatform);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Plate")
        {
            platesLeft--;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Plate")
        {
            platesLeft++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageOverlays : MonoBehaviour
{
    public Image overlaySpace;

    public Sprite[] images;

    public float lastingTime = 24f;
    private float countTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        overlaySpace.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;

        if (countTime >= 2f && countTime < 9f)
        {
            overlaySpace.enabled = true;
            overlaySpace.sprite = images[0];
        }
        else if (countTime >= 9f && countTime < 19f)
        {
            overlaySpace.sprite = images[1];

        }
        else if(countTime < lastingTime)
        {
            overlaySpace.sprite = images[2];
        }
        else
        {
            overlaySpace.enabled = false;
        }
    }
}

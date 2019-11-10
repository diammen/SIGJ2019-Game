using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSong : MonoBehaviour
{

    public AudioSource speaker;
    public float toLoad = 27f;
    private float countTime = 0.0f;
    public AudioClip barfSound;
    bool active = false;
    // Update is called once per frame
    void Update()
    {


        countTime += Time.deltaTime;
        if (!active  && countTime > toLoad)
        {
            speaker.PlayOneShot(barfSound);
            active = true;
        }
    }
}

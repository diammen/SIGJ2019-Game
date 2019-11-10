using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip memoryMusic;
    public AudioClip endingMusic;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

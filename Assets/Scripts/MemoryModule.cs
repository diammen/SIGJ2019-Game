using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MemoryModule : MonoBehaviour
{
    public AudioSource source;
    public AudioClip memoryModulePickup;
    public PlayableDirector director;
    //This is basically a win condition
    //Upon collecting this, go to menu
    public string mainMenu;

    WaitForSeconds waitForJingle;

    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

        waitForJingle = new WaitForSeconds(memoryModulePickup.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(WaitForJingle());
        }
    }

    private IEnumerator WaitForJingle()
    {
        director.Play();
        yield return null;
        Time.timeScale = 0;
    }
}

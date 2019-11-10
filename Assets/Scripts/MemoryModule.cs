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
            SceneManager.LoadScene(mainMenu);
        }
    }

    private IEnumerator WaitForJingle()
    {
        yield return waitForJingle;
        director.Play();
        Time.timeScale = 0;
    }
}

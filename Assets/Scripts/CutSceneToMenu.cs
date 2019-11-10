using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CutSceneToMenu : MonoBehaviour
{
    public float toLoad = 45f;
    private float countTime = 0.0f;
    public string mainMenu;
    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if (countTime > toLoad)
        {
            SceneManager.LoadScene(mainMenu);
        }
    }
}

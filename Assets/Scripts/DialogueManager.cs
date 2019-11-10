using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public float lastingTime = 10f;
    private float countTime = 0.0f;
    public string[] displayText;
    public bool timer = false;
    public Text dialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timer)
        {
            countTime += Time.deltaTime;
            dialogue.text = displayText[0];
            if (countTime >= lastingTime / 2)
            {
                dialogue.text = displayText[1];
            }
            if(countTime >= lastingTime)
            {
                countTime = 0.0f;
                dialogue.text = " ";
                timer = false;
            }
        }

    }

    public void setTextToShow(string one, string two)
    {
        displayText[0] = one;
        displayText[1] = two;
        timer = true;
    }

    void displayDialogue()
    {
        


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMods : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Module")
        {
            Debug.Log("Hit");
            if (collision.gameObject.name == "ActivateRotation")
            {
                GetComponent<PlayerMove>().rotationModuleOn = true;

            }
            else if(collision.gameObject.name == "ActivateTranslation")
            {
                GetComponent<PlayerMove>().translationModuleOn = true;
            }
            else if(collision.gameObject.name == "ActivateThrow")
            {
                GetComponent<PlayerThrow>().canThrow = true;
            }
            Destroy(collision.gameObject);
        }
    }
}

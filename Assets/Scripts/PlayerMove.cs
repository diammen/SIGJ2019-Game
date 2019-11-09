using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public bool translationModuleOn;
    public bool rotationModuleOn;

    Rigidbody rb;
    float x, y;
    float drive;
    bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        x = rotationModuleOn ? Input.GetAxis("Horizontal") : 0;
        y = rotationModuleOn ? Input.GetAxis("Vertical") : 0;
        drive = translationModuleOn ? Input.GetAxis("Drive") : 0;

        if (drive != 0)
            isMoving = true;
        else
            isMoving = false;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 lookVec = new Vector3(x, 0, y);
            Quaternion lookRot = rb.rotation;

            if (lookVec != Vector3.zero)
                lookRot = Quaternion.LookRotation(lookVec, rb.transform.up);

            rb.AddForce(Camera.main.transform.forward * drive * moveSpeed, ForceMode.Impulse);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, lookRot, Time.deltaTime * rotationSpeed));
        }
    }
}

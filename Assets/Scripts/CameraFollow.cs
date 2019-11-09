using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 offset;
    public float smoothTime;
    public float rotationSpeed;

    Vector3 velocity;
    float x;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Camera Horizontal");

        offset = Quaternion.AngleAxis(x * rotationSpeed, Vector3.up) * offset;

        transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + offset, ref velocity, smoothTime);
        transform.rotation = Quaternion.LookRotation(followTarget.position - transform.position, Vector3.up);
    }
}

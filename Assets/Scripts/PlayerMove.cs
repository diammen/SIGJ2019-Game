using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public AudioSource moveSoundSource, errorSoundSource;
    public AudioClip errorSound;
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
        rb = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        x = rotationModuleOn ? Input.GetAxis("Horizontal") : 0;
        y = rotationModuleOn ? Input.GetAxis("Vertical") : 0;
        drive = translationModuleOn ? Input.GetAxis("Drive") : 0;

        if (!errorSoundSource.isPlaying && !rotationModuleOn && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            errorSoundSource.Play();
        }

        if (!errorSoundSource.isPlaying && Input.GetAxis("Drive") != 0 && !translationModuleOn)
        {
            errorSoundSource.Play();
        }

        if (drive != 0)
        {
            if (!moveSoundSource.isPlaying)
                StartCoroutine(moveSoundSequence());
            isMoving = true;
        }
        else
        {
            //if (!soundFading && moveSoundSource.isPlaying)
            //{
            //    StartCoroutine(soundFadeout());
            //}
            if (moveSoundSource.isPlaying)
            {
                moveSoundSource.volume = 0;
                moveSoundSource.Stop();
            }
            isMoving = false;
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDir = cameraForward * y + cameraRight * x;
            Quaternion lookRot = rb.rotation;

            if (moveDir != Vector3.zero)
                lookRot = Quaternion.LookRotation(moveDir, rb.transform.up);

            rb.AddForce(rb.transform.forward * drive * moveSpeed, ForceMode.Impulse);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, lookRot, Time.deltaTime * rotationSpeed));
        }
    }

    IEnumerator moveSoundSequence()
    {
        moveSoundSource.Play();
        while (moveSoundSource.volume < 1)
        {
            moveSoundSource.volume += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator soundFadeout()
    {
        Debug.Log("fading sound");
        float startVolume = moveSoundSource.volume;
        while (moveSoundSource.volume > 0)
        {
            moveSoundSource.volume -= startVolume * Time.deltaTime;
            yield return null;
        }

        moveSoundSource.volume = 0;
        moveSoundSource.Stop();
    }
}

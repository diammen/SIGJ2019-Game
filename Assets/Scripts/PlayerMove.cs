using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public AudioSource moveSoundSource;
    public AudioClip startMoveSound;
    public AudioClip loopMoveSound;
    public float moveSpeed;
    public float rotationSpeed;
    public bool translationModuleOn;
    public bool rotationModuleOn;

    Rigidbody rb;
    float x, y;
    float drive;
    float startSoundVolume;
    bool isMoving;
    bool soundFading = false; 

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

        if (drive != 0)
        {
            if (!moveSoundSource.isPlaying)
                StartCoroutine(moveSoundSequence());
            startSoundVolume = moveSoundSource.volume;
            isMoving = true;
        }
        else
        {
            if (!soundFading && moveSoundSource.isPlaying)
            {
                StartCoroutine(soundFadeout());
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
        moveSoundSource.volume = 1;
        moveSoundSource.PlayOneShot(startMoveSound);
        yield return new WaitForSeconds(startMoveSound.length);
        moveSoundSource.Play();
        moveSoundSource.loop = true;
    }

    IEnumerator soundFadeout()
    {
        soundFading = true;
        Debug.Log("fading sound");
        float startVolume = moveSoundSource.volume;
        while (moveSoundSource.volume > 0)
        {
            moveSoundSource.volume -= startVolume * Time.deltaTime;
            yield return null;
        }

        print("stopping sound");

        moveSoundSource.volume = 0;
        moveSoundSource.Stop();
        soundFading = false;
    }
}

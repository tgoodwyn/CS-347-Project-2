using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{

    Rigidbody rb;
    Transform camTransform;

    // values for storing amt of change to transform each frame
    Quaternion deltaRotation;
    Vector3 deltaPosition;

    // the mouse x and y sensitivty, project settings multiply this value by 3 currently
    [Range(10f, 360f)]
    public float mouseSensitivity = 120;
    [Range(10f, 360f)]
    public float mouseYSensitivity = 120f;
    [Range(3f, 20f)]
    public float moveSpeed = 5f;

    // variables to store input
    float horizontal;
    float vertical;
    float mouseX;
    float mouseY;

    // for testing - user can press m to stop/resume input control
    public bool managed = false;
    public Manager manager;

    // variables related to audio
    bool IsMoving = true;
    AudioSource audioSource;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camTransform = Camera.main.transform;
        Cursor.visible = false;
        audioSource = gameObject.GetComponent<AudioSource>();

    }



    void Update()
    {
        if (managed)
        {
            if (manager.hasBegun()) GetInputs();
        }
        else
        {
            GetInputs();
        }


        // player controller checks for vertical movement - strafe script checks for horizontal
        if (Math.Abs(vertical) > 0) IsMoving = true;
        else IsMoving = false;
        playAudio();

        // rotate the amt based on mouse movement
        camTransform.Rotate(-Vector3.right * mouseY * mouseYSensitivity * Time.deltaTime);
    }


    void playAudio()
    {
        if (IsMoving && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (!IsMoving) audioSource.Stop();
    }


    private void FixedUpdate()
    {

        // get the delta for rotation and movement, apply it to transform
        deltaRotation = Quaternion.Euler(Vector3.up * mouseX * Time.fixedDeltaTime * mouseSensitivity);
        rb.MoveRotation(rb.rotation * deltaRotation);
        deltaPosition = ((transform.forward * vertical) + (transform.right * horizontal)) * Time.fixedDeltaTime * moveSpeed;
        rb.MovePosition(rb.position + deltaPosition);
        Debug.Log(deltaPosition);
    }


    void GetInputs()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
}

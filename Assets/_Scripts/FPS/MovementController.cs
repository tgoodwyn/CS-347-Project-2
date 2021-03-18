using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    Rigidbody rb;
    Transform camTransform;
    Quaternion deltaRotation;
    Vector3 deltaPosition;
    [Range(10f, 360f)]
    public float mouseSensitivity = 120;
    [Range(10f, 360f)]
    public float mouseYSensitivity = 120f;
    [Range(3f, 20f)]
    public float moveSpeed = 5f;
    float horizontal;
    float vertical;
    float mouseX;
    float mouseY;

    public bool managed = false;
    public Manager manager;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camTransform = Camera.main.transform;
        Cursor.visible = false;

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


        camTransform.Rotate(-Vector3.right * mouseY * mouseYSensitivity * Time.deltaTime);
    }





    private void FixedUpdate()
    {

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

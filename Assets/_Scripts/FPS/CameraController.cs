using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Rigidbody rb;
    Transform camTransform;
    Quaternion deltaRotation;

    [Range(10f, 360f)]
    public float mouseSensitivity = 120;
    [Range(10f, 360f)]
    public float mouseYSensitivity = 120f;

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
        if (!manager.isPaused()) GetInputs();

        camTransform.Rotate(-Vector3.right * mouseY * mouseYSensitivity * Time.deltaTime);
    }





    private void FixedUpdate()
    {
        deltaRotation = Quaternion.Euler(Vector3.up * mouseX * Time.fixedDeltaTime * mouseSensitivity);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }


    void GetInputs()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }
}

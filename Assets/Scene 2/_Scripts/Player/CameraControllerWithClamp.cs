using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerWithClamp : MonoBehaviour
{

    [Range(10f, 360f)]
    public float mouseXSensitivity = 120;
    [Range(10f, 360f)]
    public float mouseYSensitivity = 120f;

    float mouseX;
    float mouseY;

    public bool managed = false;
    public GameStateManager manager;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.visible = false;
    }



    void Update()
    {
        if (!manager.isPaused()) GetInputs();
        //GetInputs();
    }


    private void FixedUpdate()
    {
        xRotation -= mouseY * Time.fixedDeltaTime * mouseXSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 45f); 

        yRotation += mouseX * Time.fixedDeltaTime * mouseYSensitivity;
        yRotation = Mathf.Clamp(yRotation, -70f, 70f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }


    void GetInputs()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }
}

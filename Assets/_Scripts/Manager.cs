using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    bool begun = true;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) begun = !begun;
        //Debug.Log(begun);
    }

    public bool hasBegun()
    {
        return begun;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    bool begun = true;

    public static int targetsHit = 0;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            begun = !begun;
            Debug.Log(targetsHit);
        }
    }

    public bool hasBegun()
    {
        return begun;
    }
}

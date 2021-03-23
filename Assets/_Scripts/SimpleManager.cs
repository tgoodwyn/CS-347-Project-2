using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SimpleManager : MonoBehaviour
{

    bool paused = false;

    void Update()
    {

        // you can pause//unpause camera movement while the game is playing
        if (Input.GetKeyDown(KeyCode.M))
        {
            paused = !paused;
            //Debug.Log(targetsHit);
        }

    }

    public bool isPaused()
    {
        return paused;
    }

}

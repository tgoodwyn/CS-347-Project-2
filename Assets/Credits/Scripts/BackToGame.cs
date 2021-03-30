using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToGame : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        // Scene switch code
        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("Scene 2/__Scene/Scene 2");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Manager : MonoBehaviour
{

    bool begun = true;

    public static int targetsHit = 0;
    public Text scoreTextValue;
    public Text shotsTextValue;
    public Text timeTextValue;

    public static int scoreValue;
    public static int shotsValue;
    public static float timeValue;




    void Start()
    {
        scoreValue = 0;
        timeValue = 30.0f;
        shotsValue = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            begun = !begun;
            Debug.Log(targetsHit);
        }
        if (timeValue <= 0f)
        {
            Debug.Log("Game over");
        }
        else 
        {
            timeValue = timeValue - Time.deltaTime;
        }
        scoreTextValue.text = scoreValue.ToString();
        shotsTextValue.text = shotsValue.ToString();
        timeTextValue.text = timeValue.ToString();
    }

    public bool hasBegun()
    {
        return begun;
    }
    
}

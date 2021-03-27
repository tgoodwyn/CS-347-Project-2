﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Manager : MonoBehaviour
{

    bool paused = true;
    bool gameFinished = false;
    public static int targetsHit = 0;
    public Text scoreTextValue;
    public Text shotsTextValue;
    public Text timeTextValue;
    public Text tDestroyedTextValue;
    public Text accuracyTextValue;
    public Text minTBetweenTargetsTextValue;
    public Text maxTBetweenTargetsTextValue;
    public Text avgTBetweenTargetsTextValue;
    public Text sdTBetweenTargetsTextValue;

    public static int scoreValue;
    public static int shotsValue;
    public static float timeValue;
    public static int tDestroyedValue;
    public static float accuracyValue;
    public static float minTBetweenTargetsValue;
    public static float maxTBetweenTargetsValue;
    public static float avgTBetweenTargetsValue;
    public static float sdTBetweenTargetsValue;

    public GameObject gameOverText;
    public GameObject gameBeatenText;


    void Start()
    {
        scoreValue = 0;
        timeValue = 30.0f;
        shotsValue = 0;
        paused = false;
    }

    void Update()
    {

        // you can pause//unpause camera movement while the game is playing
        if (!gameFinished)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                paused = !paused;
                //Debug.Log(targetsHit);
            }
        }

        if (timeValue <= 0f)
        {
            //Debug.Log("Game over");
            gameFinished = true;
            paused = true;
            gameOverText.SetActive(true);
        }
        else
        {
            timeValue = timeValue - Time.deltaTime;
        }
        scoreTextValue.text = scoreValue.ToString();
        shotsTextValue.text = shotsValue.ToString();
        timeTextValue.text = timeValue.ToString();
    }

    public void victoryAchieved()
    {
        paused = true;
        gameFinished = true;
        gameBeatenText.SetActive(true);
    }

    public void resetSubLevel()
    {
        paused = false;
        gameFinished = false;
        gameBeatenText.SetActive(false);
        gameOverText.SetActive(false);
        timeValue = 30.0f;
        shotsValue = 0;
    }

    public void resetGame()
    {
        paused = false;
        gameFinished = false;
        gameBeatenText.SetActive(false);
        gameOverText.SetActive(false);
        scoreValue = 0;
        timeValue = 30.0f;
        shotsValue = 0;

    }


    // getters
    public bool getGameStatus()
    {
        // so that the level controller can check if the game has been beaten or lost
        // only allow reset if it has
        return gameFinished;
    }
    public bool isPaused()
    {
        return paused;
    }

}

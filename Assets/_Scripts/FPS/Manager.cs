using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Level1Controller l1c;
    bool paused = true;
    bool gameFinished = false;
    bool plScreenActive = false;
    public bool ready = true;
    public static int targetsHit = 0;
    public Text scoreTextValue;
    public Text shotsTextValue;
    public Text timeTextValue;
    public Text tHitTextValue;
    public Text tRemainingTextValue;
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
    public GameObject plScreen;
    public GameObject reticle;

    void Start()
    {
        scoreValue = 0;
        timeValue = 30.0f;
        shotsValue = 0;
        paused = false;
        l1c = GetComponent<Level1Controller>();
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
        if (plScreenActive)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                plScreen.SetActive(false);
                plScreenActive = false;
                ready = true;
                l1c.nextLevel();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                plScreen.SetActive(false);
                plScreenActive = false;
                ready = true;
                l1c.repeatLevel();
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
        if (ready == true)
        {
            timeTextValue.text = timeValue.ToString();
        }
        tHitTextValue.text = Level1Controller.tDestructionTimes.Count.ToString();
        tRemainingTextValue.text = (l1c.numTargets - Level1Controller.tDestructionTimes.Count).ToString();


    }

    // loads post level screen
    public void lplScreen()
    {
        Manager.scoreValue += Manager.targetsHit * (int)Mathf.Log(Manager.timeValue,2) - (Manager.shotsValue - l1c.numTargets);
        computeStats();
        accuracyTextValue.text = accuracyValue.ToString("#.00")+"%";
        minTBetweenTargetsTextValue.text = minTBetweenTargetsValue.ToString("#.000")+" s";
        maxTBetweenTargetsTextValue.text = maxTBetweenTargetsValue.ToString("#.000")+" s";
        avgTBetweenTargetsTextValue.text = avgTBetweenTargetsValue.ToString("#.000")+" s";
        sdTBetweenTargetsTextValue.text = sdTBetweenTargetsValue.ToString("#.000")+" s";
        plScreen.SetActive(true);
        plScreenActive = true;
        ready = false;
    } 

    public void nlHUDSet()
    {
        Manager.targetsHit = 0;
        Manager.shotsValue = 0;
        Manager.timeValue = 30.0f;
    }
    
    public void rlHUDSet()
    {
        Manager.targetsHit = 0;
        Manager.shotsValue = 0;
        Manager.timeValue = 30.0f;
    }

    public void victoryAchieved()
    {
        paused = true;
        gameFinished = true;
        gameBeatenText.SetActive(true);
    }



    public void resetLevel()
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

    private void computeStats()
    {

        // Computes intervals between target hits
        List<float> betweenTargetIntervals = new List<float>();
        for (int i = 0; i < Level1Controller.tDestructionTimes.Count - 1; i++)
        {
            betweenTargetIntervals.Add(Level1Controller.tDestructionTimes[i + 1] - Level1Controller.tDestructionTimes[i]);
        }

        // Finds min, max, and avg between target intervals
        float sumOfBTIs = 0;
        minTBetweenTargetsValue = 10;
        maxTBetweenTargetsValue = 0;
        for (int i = 0; i < betweenTargetIntervals.Count; i++)
        {
            if (betweenTargetIntervals[i] < minTBetweenTargetsValue)
            {
                minTBetweenTargetsValue = betweenTargetIntervals[i];
            }
            if (betweenTargetIntervals[i] > maxTBetweenTargetsValue)
            {
                maxTBetweenTargetsValue = betweenTargetIntervals[i];
            }
            sumOfBTIs += betweenTargetIntervals[i];
        }
        avgTBetweenTargetsValue = sumOfBTIs / betweenTargetIntervals.Count;

        // Computers SD
        float sqDiffSum = 0;
        for (int i = 0; i < betweenTargetIntervals.Count; i++)
        {
            sqDiffSum += Mathf.Pow(betweenTargetIntervals[i] - avgTBetweenTargetsValue, 2);
        }
        sdTBetweenTargetsValue = Mathf.Sqrt(sqDiffSum / (betweenTargetIntervals.Count - 1));

        // Computes accuracy
        accuracyValue = 100 * targetsHit / shotsValue;
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

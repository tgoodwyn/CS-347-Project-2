using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Level1Controller l1c;
    bool paused = true;
    bool gameFinished = false;
    bool plScreenActive = false;
    bool gbScreenActive = true;
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
    public Text clScoreTextValue;
    public Text nextLevelTextValue;

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
    public GameObject gbScreen;

    private string[] levelDescriptions = new string[33];

    void Start()
    {
        scoreValue = 0;
        timeValue = 30.0f;
        shotsValue = 0;
        l1c = GetComponent<Level1Controller>();
        addLevelDescriptions();
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
		else
		{
            if (Input.GetKeyDown(KeyCode.P))
            {
                SceneManager.LoadScene("Scene 2");
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                SceneManager.LoadScene("Credits/Credits");
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
				resetGame();
            }
		}
        if (gbScreenActive)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                gbScreen.SetActive(false);
                gbScreenActive = false;
                paused = false;
                scoreValue = 0;
                timeValue = 30.0f;
                shotsValue = 0;
                targetsHit = 0;
            }
        }
        else if (plScreenActive)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                plScreen.SetActive(false);
                plScreenActive = false;
                ready = true;
                reticle.SetActive(true);
                l1c.nextLevel();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                plScreen.SetActive(false);
                plScreenActive = false;
                ready = true;
                reticle.SetActive(true);
                l1c.repeatLevel();
            }
        }
        // for presentation
        else
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                l1c.nextLevel();
            }
        }
        if (timeValue <= 0f)
        {
            //Debug.Log("Game over");
            gameFinished = true;
            paused = true;
            gameOverText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                paused = false;
                gameBeatenText.SetActive(false);
                gameOverText.SetActive(false);
                l1c.repeatLevel();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                l1c.gameBeaten();
            }

        } 
        else if (ready == false || gbScreenActive==true)
        {

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
        clScoreTextValue.text = (Manager.targetsHit * (int)Mathf.Log(Manager.timeValue, 2) - (Manager.shotsValue - l1c.numTargets)).ToString();
        accuracyTextValue.text = accuracyValue.ToString("#.00")+"%";
        minTBetweenTargetsTextValue.text = minTBetweenTargetsValue.ToString("#.000")+" s";
        maxTBetweenTargetsTextValue.text = maxTBetweenTargetsValue.ToString("#.000")+" s";
        avgTBetweenTargetsTextValue.text = avgTBetweenTargetsValue.ToString("#.000")+" s";
        sdTBetweenTargetsTextValue.text = sdTBetweenTargetsValue.ToString("#.000")+" s";
        if (l1c.eIncrementor < 32) {
            nextLevelTextValue.text = levelDescriptions[l1c.eIncrementor + 1];
        }
        else
        {
            nextLevelTextValue.text = "";
        }
        plScreen.SetActive(true);
        plScreenActive = true;
        ready = false;
        reticle.SetActive(false);
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
        Manager.targetsHit = 0;
    }

    public void resetGame()
    {
        paused = false;
        gameFinished = false;
        gameBeatenText.SetActive(false);
        gameOverText.SetActive(false);
		l1c.gameBeaten();
        timeValue = 30.0f;
        shotsValue = 0;
        scoreValue = 0;
        Manager.targetsHit = 0;
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

        // Computes SD
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

    private void addLevelDescriptions()
    {
        levelDescriptions[0] = "2 rows, even spacing \nTargets: 10]";
        levelDescriptions[1] = "2 rows, random spacing \nTargets: 14";
        levelDescriptions[2] = "2 columns, even spacing \nTargets: 10";
        levelDescriptions[3] = "2 columns, random spacing \nTargets: 14";
        levelDescriptions[4] = "2 diagonals, even spacing \nTargets: 17";
        levelDescriptions[5] = "2 diagonals, random spacing \nTargets: 13";
        levelDescriptions[6] = "Circle, even spacing \nTargets: 12";
        levelDescriptions[7] = "Circle, random spacing \nTargets: 8";
        levelDescriptions[8] = "Random arrangement, large area \nTargets :10";
        levelDescriptions[9] = "Random arrangement, medium area \nTargets: 6";
        levelDescriptions[10] = "Repeat. Random arrangement, medium area \nTargets: 6";
        levelDescriptions[11] = "Random arrangement, small area \nTargets: 4";
        levelDescriptions[12] = "Repeat. Random arrangement, small area \nTargets: 4";
        levelDescriptions[13] = "Repeat. Random arrangement, small area \nTargets: 4";
        levelDescriptions[14] = "Spheres now spawn 1 at a time as the previous is hit. \nHorizontal motion, even spacing, switches directions \nTargets: 15";
        levelDescriptions[15] = "Horizontal motion, random spacing, switches directions \nTargets: 16";
        levelDescriptions[16] ="Slower horizontal motion, even spacing, switches directions \nTargets: 15";
        levelDescriptions[17] ="Slower horizontal motion, random spacing, switches directions \nTargets: 16";
        levelDescriptions[18] ="Vertical motion, even spacing, switches directions \nTargets: 11";
        levelDescriptions[19] ="Vertical motion, random spacing, switches directions \nTargets: 14";
        levelDescriptions[20] ="Slower vertical motion, even spacing, switches directions \nTargets: 11";
        levelDescriptions[21] ="Slower vertical motion, random spacing, switches directions \nTargets: 14";
        levelDescriptions[22] ="Zig - zag pattern, switches directions horizontally \nTargets: 20";
        levelDescriptions[23] ="Random motion, switches directions horizontally \nTargets: 20";
        levelDescriptions[24] ="Circle, even spacing \nTargets: 12";
        levelDescriptions[25] ="Circle, random spacing \nTargets: 8";
        levelDescriptions[26] ="Random spawns in a large spawn area  \nTargets: 15";
        levelDescriptions[27] ="Random spawns in a medium spawn area \nTargets: 15";
        levelDescriptions[28] ="Random spawns in a small spawn area \nTargets: 15";
        levelDescriptions[29] ="Random spawns in a smaller spawn area \nTargets: 15";
        levelDescriptions[30] ="Random spawns of various sizes in small - medium spawn area \nTargets: 15";
        levelDescriptions[31] ="Sphere - in-random - motion. \nTargets: 15";
        levelDescriptions[32] ="Sphere - in-random - motion, varying size \nTargets: 15";

    }

}

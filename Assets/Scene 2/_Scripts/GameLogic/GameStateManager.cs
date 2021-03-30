using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{

    public GameObject player;

    [System.NonSerialized]
    public Level currentLevel;
    [System.NonSerialized]
    public int currentLevelIndex = 0;
    public Level[] gameLevels = new Level[4];


    bool paused = false;

    [System.NonSerialized]
    public int remainingTargets;
    //public List<GameObject> remainingTargets = new List<GameObject>();


    private int levelScore = 0;
    private int totalScore = 0;
    [System.NonSerialized]
    public int shotsFiredCount;
    [System.NonSerialized]
    public int targetHitCount;
    [System.NonSerialized]
    public int targetEscapeCount;

    public Text shotsFiredCountText;
    public Text targetHitCountText;
    public Text targetEscapeCountText;
    public Text currentLevelText;
    public Text remainingTargetText;

    private int hundredPercentBonus;
    private int shotsFiredPenalty;

    private LevelGenerator levelGenerator;

    public Text totalScoreTextValue;
    public Text levelScoreTextValue;
    //    public Text currentLevelTextValue;

    public GameObject levelCompleteText;

    public GameObject hundredPercentTextLabel;
    public GameObject hundredPercentTextValue;
    public GameObject shotPenaltyTextLabel;
    public GameObject shotPenaltyTextValue;

    public GameObject gameStartText;
    public GameObject gameCompleteText;
    public GameObject gameFinalScoreText;

    [System.NonSerialized]
    public bool gameRunning;
    private bool levelRunning;

    public float timeBetweenLevels = 3f;

    private void Start()
    {
        levelGenerator = GetComponent<LevelGenerator>();
    }
    void gameFinish()
    {
        gameRunning = false;
        displayEndOfGameText();
    }

    void gameStart()
    {
        currentLevelIndex = 0;
        levelStart();
        resetTrackedValuesForGame();
        gameRunning = true;
    }

    void levelStart()
    {
        loadLevel();
        resetTrackedValuesForLevel();
        levelRunning = true;

    }
    private void loadLevel()
    {
        currentLevel = gameLevels[currentLevelIndex];
        remainingTargets = getTotalTargetsForLevel();
        levelGenerator.setCurrentLevel(currentLevel);
        StartCoroutine(levelGenerator.spawnLevel());

    }
    IEnumerator levelFinish()
    {
        levelRunning = false;
        calculateScoreAtEndOfLevel();
        yield return StartCoroutine(displayLevelTransitionText());
        currentLevelIndex++;
        if (currentLevelIndex == gameLevels.Length)
        {
            gameFinish();
        }
        else
        {
            levelStart();
        }
    }
    void resetTrackedValuesForGame()
    {
        totalScore = 0;
        gameStartText.SetActive(false);
        gameCompleteText.SetActive(false);
        gameFinalScoreText.SetActive(false);

    }
    void resetTrackedValuesForLevel()
    {
        shotsFiredCount = 0;
        targetHitCount = 0;
        targetEscapeCount = 0;
        levelScore = 0;
    }

    private IEnumerator displayLevelTransitionText()
    {
        levelCompleteText.SetActive(true);
        hundredPercentTextLabel.SetActive(true);
        hundredPercentTextValue.SetActive(true);
        shotPenaltyTextLabel.SetActive(true);
        shotPenaltyTextValue.SetActive(true);
        Text bonusText = hundredPercentTextValue.GetComponent<Text>();
        Text penaltyText = shotPenaltyTextValue.GetComponent<Text>();
        bonusText.text = hundredPercentBonus.ToString();
        penaltyText.text = shotsFiredPenalty.ToString();
        yield return new WaitForSeconds(timeBetweenLevels);
        levelCompleteText.SetActive(false);
        hundredPercentTextLabel.SetActive(false);
        hundredPercentTextValue.SetActive(false);
        shotPenaltyTextLabel.SetActive(false);
        shotPenaltyTextValue.SetActive(false);
    }

    private void calculateScoreAtEndOfLevel()
    {
        int totalLevelTargets = getTotalTargetsForLevel();
        if (targetEscapeCount == 0) hundredPercentBonus = 100 * totalLevelTargets;
        else hundredPercentBonus = 0;
        int shotsInExcess = shotsFiredCount - totalLevelTargets;
        shotsInExcess = Math.Max(0, shotsInExcess);
        shotsFiredPenalty = shotsInExcess / totalLevelTargets * 313;
        totalScore = totalScore + levelScore + hundredPercentBonus - shotsFiredPenalty;
    }


    public void updateScoreForLevel()
    {

        levelScore = targetHitCount * 100;
    }


    void Update()
    {
        if (gameRunning)
        {

            if (levelRunning)
            {
                if (remainingTargets == 0)
                {
                    StartCoroutine(levelFinish());
                }

                updateScoreForLevel();
            }

        } else
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                SceneManager.LoadScene("Credits/Credits");
            }

        }


        // start the game by pressing b
        if (Input.GetKeyDown(KeyCode.B) && !gameRunning)
        {
            gameStart();
        }

        // you can pause/unpause camera movement while the game is playing
        if (Input.GetKeyDown(KeyCode.M))
        {
            paused = !paused;
        }

        totalScoreTextValue.text = totalScore.ToString();
        shotsFiredCountText.text = shotsFiredCount.ToString();
        targetHitCountText.text = targetHitCount.ToString();
        targetEscapeCountText.text = targetEscapeCount.ToString();
        currentLevelText.text = (currentLevelIndex + 1).ToString();
        levelScoreTextValue.text = levelScore.ToString();
        remainingTargetText.text = remainingTargets.ToString();
    }



    private void displayEndOfGameText()
    {
        Text restartText = gameStartText.GetComponent<Text>();
        restartText.text = "Press 'b' to restart game\n Press 'c' to view credits";
        gameStartText.SetActive(true);
        gameCompleteText.SetActive(true);
        Text scoreText = gameFinalScoreText.GetComponent<Text>();
        scoreText.text = totalScore.ToString();
        gameFinalScoreText.SetActive(true);
    }



    public bool isPaused()
    {
        return paused;
    }



    public int getTotalTargetsForLevel()
    {
        int targetsPerRow = (int)Mathf.Floor(currentLevel.width / currentLevel.targetSpacing);
        int numberOfRows = (int)Mathf.Floor(currentLevel.height / currentLevel.targetSpacing);
        return targetsPerRow * numberOfRows;
    }

}

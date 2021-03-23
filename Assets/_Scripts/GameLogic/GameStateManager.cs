using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{

    bool paused = false;

    private static int scoreValue = 0;
    private static int shotsValue;
    private static float eventTimerValue;
    private static float totalTimeValue;

    private LevelController level;

    public GameObject player;
    public Text eventTimeTextValue;
    public Text totalTimeTextValue;
    public Text scoreTextValue;
    public Text currentLevelTextValue;

    public GameObject gameStartText;
    public GameObject newLevelText;
    public GameObject gameCompleteText;
    public GameObject gameFinalScoreText;

    private static bool eventBegunForLevel = false;
    private bool levelResetBegun = false;
    private bool levelBegun;
    private bool gameBegun;

    public float timeBetweenLevels = 3f;

    private void Start()
    {
        level = GetComponent<LevelController>();
        totalTimeValue = 0;
    }
    void Update()
    {
        if (gameBegun)
        {
            totalTimeValue += Time.deltaTime;
        }

        // start the game by pressing b
        if (Input.GetKeyDown(KeyCode.B) && !gameBegun)
        {
            startGame();
        }

        // you can pause/unpause camera movement while the game is playing
        if (Input.GetKeyDown(KeyCode.M))
        {
            paused = !paused;
        }


        if (eventTimerValue > 0f && levelBegun)
        {
            eventTimerValue -= Time.deltaTime;
        }

        // level finishes when there are no targets left
        if (LevelController.getRemainingTargetCount() <= 0 && !levelResetBegun && gameBegun)
        {
            if (level.currentLevelNumber == LevelController.totalLevels) {
                gameCompleted();
            }
            else
            {

                StartCoroutine(nextLevel());
            }

        }

        eventTimeTextValue.text = eventTimerValue.ToString();
        totalTimeTextValue.text = totalTimeValue.ToString();
        scoreTextValue.text = scoreValue.ToString();
    }

    private void startGame()
    {
        totalTimeValue = 0;
        scoreValue = 0;
        gameBegun = true;
        levelBegun = true;
        level.currentLevelNumber = 1;
        level.LoadLevel();
        loadLevel();
        level.beginLevel();
        gameStartText.SetActive(false);
        gameCompleteText.SetActive(false);
        gameFinalScoreText.SetActive(false);
    }

    private void gameCompleted()
    {
        levelResetBegun = true;
        gameBegun = false;
        Text restartText = gameStartText.GetComponent<Text>();
        restartText.text = "Press 'b' to restart game";
        gameStartText.SetActive(true);
        gameCompleteText.SetActive(true);
        Text scoreText = gameFinalScoreText.GetComponent<Text>();
        scoreText.text = scoreValue.ToString();
        gameFinalScoreText.SetActive(true);
    }

    IEnumerator nextLevel()
    {
        levelResetBegun = true;

        levelBegun = false;
        level.nextLevel();
        yield return StartCoroutine(displayStartOfLevelText());
        level.beginLevel();
        loadLevel();
    }
    private void loadLevel()
    {
        level.LoadLevel();
        eventTimerValue = LevelController.timeTilEvent;
        levelBegun = true;
        currentLevelTextValue.text = level.currentLevelNumber.ToString();
        resetLevelMotion();
        levelResetBegun = false;
    }

    private void FixedUpdate()
    {
        if (levelBegun && eventTimerValue <= 0 && !eventBegunForLevel)
        {
            eventBegunForLevel = true;
            level.triggerLevelEvent();
        }
    }

    public static void updateScore()
    {
        int multiplier = 100;
        if (eventBegunForLevel)
        {
            multiplier = 200;
        }

        scoreValue += (int)(multiplier / totalTimeValue);
    }
    public void resetLevelMotion()
    {
        eventBegunForLevel = false;
    }
    public bool isPaused()
    {
        return paused;
    }

    IEnumerator displayStartOfLevelText()
    {
        newLevelText.SetActive(true);
        yield return new WaitForSeconds(timeBetweenLevels);
        newLevelText.SetActive(false);
    }

}

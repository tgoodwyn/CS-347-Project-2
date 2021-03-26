//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class LevelController : MonoBehaviour
//{

//    public GameObject targetPrefab;
//    private GameStateManager gameStateManager;
//    private Transform playerTransform;

//    public Level[] gameLevels = new Level[3];

//    public int currentLevelNumber; // not the index
//    private Level currentLevel;


//    public static float startingXOffsetFromPlayer;
//    public static float startingYOffsetFromPlayer;
//    public static float startingZOffsetFromPlayer;
//    public static float scaleTargetSizeBy;
//    public static float horizontalTargetSpacing;
//    public static float verticalTargetSpacing;
//    public static int targetsPerRow;
//    public static int numberOfRows;

//    public static float timeTilEvent;
//    public static float targetMoveSpeed;
//    public static float timeDelayBetweenMovementEvents;
//    public static int randomOrSequential;
//    public static int sinkOrFloat;

//    public static int totalLevels;
//    public delegate IEnumerator EventFunc();
//    public EventFunc levelEventFunc;

//    private static List<GameObject> remainingTargets = new List<GameObject>();
//    public static int totalLevelTargets;
//    // Start is called before the first frame update
//    void Start()
//    {
//        gameStateManager = GetComponent<GameStateManager>();
//        playerTransform = gameStateManager.player.transform;
//        totalLevels = gameLevels.Length;


//    }

//    //======================================================================================//
//    // start of level functions
//    //======================================================================================//

//    public void nextLevel()
//    {
//        currentLevelNumber++;
//        LoadLevel();
//    }

//    public void LoadLevel()
//    {
//        int currentLevelIndex = currentLevelNumber - 1;
//        currentLevel = gameLevels[currentLevelIndex];
//        initializeLevel();
//        totalLevelTargets = numberOfRows * targetsPerRow;
//    }

//    public void beginLevel()
//    {
//        spawnTargets();
//    }
//    void spawnTargets()
//    {
//        switch (randomOrSequential)
//        {
//            case 0:
//                //StartCoroutine(spawnTargetsRandom());
//                spawnTargetsRandom();
//                break;
//            case 1:
//                spawnTargetsSequential();
//                break;
//        }
//    }

//    //======================================================================================//
//    // specific spawn functions 
//    //======================================================================================//
//    void spawnTargetsSequential()
//    {
//        Vector3 spawnOffset = new Vector3(startingXOffsetFromPlayer, startingYOffsetFromPlayer, startingZOffsetFromPlayer);
//        Vector3 startingSpawnPosition = playerTransform.position + spawnOffset;
//        Vector3 spawnLoc = startingSpawnPosition;
//        for (int i = 0; i < numberOfRows; i++)
//        {
//            for (int j = 0; j < targetsPerRow; j++)
//            {
//                GameObject spawned = Instantiate(targetPrefab, spawnLoc, Quaternion.identity);
//                remainingTargets.Add(spawned);
//                setMovementStrategies(spawned);
//                spawned.transform.localScale *= scaleTargetSizeBy; // scale the prefab up
//                spawnLoc.x += horizontalTargetSpacing;
//            }

//            spawnLoc.x = startingSpawnPosition.x;
//            spawnLoc.y += verticalTargetSpacing;
//        }
//    }

//    void spawnTargetsRandom()
//    {
//        float spawnFieldWidth = horizontalTargetSpacing * targetsPerRow;
//        float spawnFieldEnd = startingXOffsetFromPlayer + spawnFieldWidth;
//        float xRNG = Random.Range(startingXOffsetFromPlayer, spawnFieldEnd);
//        Vector3 spawnLoc = new Vector3(xRNG, startingYOffsetFromPlayer, startingZOffsetFromPlayer);

        
//        for (int i = 0; i < totalLevelTargets; i++)
//        {
//            GameObject spawned = Instantiate(targetPrefab, spawnLoc, Quaternion.identity);
//            remainingTargets.Add(spawned);
//            setMovementStrategies(spawned);
//            spawned.transform.localScale *= scaleTargetSizeBy; // scale the prefab up
//            spawned.SetActive(false);
//            xRNG = Random.Range(startingXOffsetFromPlayer, spawnFieldEnd);
//            spawnLoc.x = xRNG;
//        }

//    }

//    private void setMovementStrategies(GameObject newTarget)
//    {
//        int horizontalMovementStrategy;
//        int verticalMovementStrategy;
//        switch (sinkOrFloat)
//        {
//            case 0: // sink
//                horizontalMovementStrategy = 0;
//                verticalMovementStrategy = -1;
//                break;
//            case 1: // float
//                horizontalMovementStrategy = 0;
//                verticalMovementStrategy = 1;
//                break;
//            default: // none
//                horizontalMovementStrategy = 0;
//                verticalMovementStrategy = 0;
//                break;

//        }

//        BalloonController bc = newTarget.GetComponent<BalloonController>();
//        bc.setMotionParameters(horizontalMovementStrategy, verticalMovementStrategy);

//    }

//    //======================================================================================//
//    // Level "event" functions 
//    //======================================================================================//

//    public void triggerLevelEvent()
//    {
//        StartCoroutine(levelEventFunc());
//    }
//    public IEnumerator beginTargetsExiting()
//    {

//        int i = 0;
//        while (remainingTargets.Count > 0)
//        {
//            try
//            {
//                GameObject balloon = remainingTargets.ElementAt(i);
//                balloon.SetActive(true);
//                BalloonController bc = balloon.GetComponent<BalloonController>();
//                if (!bc.isInMotion())
//                {
//                    bc.setInMotion();
//                    i = 0;
//                }
//                else i++;
//            }
//            catch
//            {
//                i = 0;
//                continue;
//            }

//            yield return new WaitForSeconds(timeDelayBetweenMovementEvents);
//            timeDelayBetweenMovementEvents *= .98f; // speed up a little each time
//        }
//    }


//    //======================================================================================//
//    // Functions acccessed by other classes     
//    //======================================================================================//


//    //public List<GameObject> getRemainingTargets()

//    public static int getRemainingTargetCount()
//    {
//        return remainingTargets.Count();
//    }

//    public static void removeBalloonFromLevel(GameObject balloon)
//    {
//        remainingTargets.Remove(balloon);
//    }

//    //======================================================================================//
//    // Initializer function 
//    //======================================================================================//
//    void initializeLevel()
//    {
//        //
//        // basically just set all the staic variables equal to their corresponding variables per Level
//        //
//        startingXOffsetFromPlayer = currentLevel.startingXOffsetFromPlayer;
//        startingYOffsetFromPlayer = currentLevel.startingYOffsetFromPlayer;
//        startingZOffsetFromPlayer = currentLevel.startingZOffsetFromPlayer;
//        scaleTargetSizeBy = currentLevel.scaleTargetSizeBy;
//        horizontalTargetSpacing = currentLevel.horizontalTargetSpacing;
//        verticalTargetSpacing = currentLevel.verticalTargetSpacing;
//        targetsPerRow = currentLevel.targetsPerRow;
//        numberOfRows = currentLevel.numberOfRows;
//        timeTilEvent = currentLevel.timeTilEvent;
//        targetMoveSpeed = currentLevel.targetMoveSpeed;
//        timeDelayBetweenMovementEvents = currentLevel.timeDelayBetweenMovementEvents;
//        randomOrSequential = currentLevel.randomOrSequential;
//        sinkOrFloat = currentLevel.sinkOrFloat;

//        switch (currentLevel.timedLevelEvent)
//        {
//            case 0:
//                levelEventFunc = beginTargetsExiting;
//                break;
//            default:
//                break;
//        }
//    }
//}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    private Level currentLevel;
    private GameStateManager gameManager;
    private Transform player;

    float motionStartDelay = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameStateManager>();
        player = gameManager.player.transform;
        //StartCoroutine(spawnLevel());

    }


    public void setCurrentLevel(Level currentLevel)
    {
        this.currentLevel = currentLevel;
    }

    public IEnumerator spawnLevel()
    {
        yield return new WaitForSeconds(2f);
        int targetsPerRow = (int)Mathf.Floor(currentLevel.width / currentLevel.targetSpacing);
        int numberOfRows = (int)Mathf.Floor(currentLevel.height / currentLevel.targetSpacing);

        float startingX = player.position.x - currentLevel.width / 2;
        float startingY = currentLevel.heightFromGround;

        float spawnX;
        float spawnY;
        float spawnZ = currentLevel.zDepth;

        Vector3 spawnLoc;

        float accelerationOfSpawnDelay = 1f - currentLevel.accelerationOfSpawnDelay * .005f;
        float spawnDelay = currentLevel.spawnDelay;
        motionStartDelay = 0;
        for (int y = 0; y < numberOfRows; y++)
        {
            for (int x = 0; x < targetsPerRow; x++)
            {
                // set the x and y coordinates, either fixed by spacing or randomized
                if (currentLevel.randomizeXSpawn) spawnX = UnityEngine.Random.Range(startingX, startingX + currentLevel.width);
                else spawnX = startingX + currentLevel.targetSpacing * x;
                if (currentLevel.randomizeYSpawn) spawnY = UnityEngine.Random.Range(startingY, startingY + currentLevel.height);
                else spawnY = startingY + currentLevel.targetSpacing * y;
                spawnX = Mathf.Clamp(spawnX, player.position.x - currentLevel.maxWidth / 2, player.position.x + currentLevel.maxWidth / 2);
                spawnY = Mathf.Clamp(spawnY, startingY, startingY + currentLevel.maxHeight);
                spawnLoc = new Vector3(spawnX, spawnY, spawnZ); // the Vector3 where the target will be spawned


                // next, spawn the target, change its scale, and intialize its movement variables
                GameObject spawned = Instantiate(currentLevel.targetPrefab, spawnLoc, Quaternion.identity); 
                spawned.transform.localScale *= currentLevel.targetScale;
                initializeTarget(spawned, gameManager, x, y);

                //gameManager.remainingTargets.Add(spawned); // might remove
                // next, delay for however long you want for the level
                yield return new WaitForSeconds(spawnDelay);
                spawnDelay *= accelerationOfSpawnDelay; // also possible to shorten this time of delay each spawn
                spawnDelay = Math.Max(spawnDelay, currentLevel.minSpawnDelay); // capping at a certain minimum (so that you don't spawn instantaneously unless you want to)
            }
        }
    }

    void initializeTarget(GameObject spawned, GameStateManager gameManager, int x, int y)
    {
        motionStartDelay += currentLevel.motionDelay;
        BalloonController bc = spawned.GetComponent<BalloonController>();
        bc.initializeMovementStrategy(currentLevel, player, x, y, motionStartDelay);
        bc.setGameManager(gameManager);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level 0", menuName = "New Level")]
public class Level : ScriptableObject
{

    //Properties of volume containing targets
    [Header("Target Data")]
    public GameObject targetPrefab;
    public float targetScale;
    public float targetSpacing;
    //public int numberOfTargets;
    [Header("Volume Data")]
    public float zDepth;
    [Range(6, 20)]
    public float heightFromGround;
    public float width;
    public float height;
    public float maxWidth;
    public float maxHeight;

    [Header("Spawn Data")]
    public bool randomizeXSpawn;
    public bool randomizeYSpawn;
    [Range(0,3.0f)]
    public float spawnDelay;
    [Range(0, 10)]
    public int accelerationOfSpawnDelay;
    [Range(0,3.0f)]
    public float minSpawnDelay;

    [Header("Per Target Motion Data")]
    [Range(0, 3.0f)]
    public float timeToMove; 
    [Range(0, 0.25f)]
    public float moveSpeed;
    [Range(0, 10)]
    public int randomizationOfSpeed;

    [Header("Pack Movement Strategy")]
    [Range(1, 6)]
    public int packMovementStrategy;
    [Range(0, 3)]
    public int numberOfBounces;
    [Range(0, 2f)]
    public float motionDelay;
    // 1 - exit stage left together
    // 2 - exit stage right together
    // 3 - exit above together
    // 4 - exit below together
    // 5 - exit horizontally in opposite ways
    // 6 - exit vertically in opposite ways
}

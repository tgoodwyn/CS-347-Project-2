using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Level 0", menuName = "New Level")]
public class Level : ScriptableObject
{
    [Range(0,1)]
    public int randomOrSequential;
    [Range(0,1)]
    public int sinkOrFloat;
    [Range(0, 0)]
    public int timedLevelEvent;

    public float startingXOffsetFromPlayer;
    public float startingYOffsetFromPlayer;
    public float startingZOffsetFromPlayer;
    public float scaleTargetSizeBy;
    public float horizontalTargetSpacing;
    public float verticalTargetSpacing;
    public int targetsPerRow;
    public int numberOfRows;


    public float timeTilEvent;
    //public int horizontalMovementStrategy = 0;
    //public int verticalMovementStrategy = 0;
    public float targetMoveSpeed = 2;
    public float timeDelayBetweenMovementEvents;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    private GameStateManager gameManager;

    private float timeToMove;
    private float timeSinceSpawned;

    private float moveSpeed;
    private int moveDirection; // 1 = left, 2 = right, 3 = above, 4 = below
    private bool isMoving = false;

    private float boundaryOffset = 3;
    private float minBoundary;
    private float maxBoundary;

    private int maxNumberOfBounces;
    private int numberOfBouncesTaken = 0;

    private delegate void MovementFunction();
    MovementFunction movementFunction;

    private void Update()
    {
        if (timeSinceSpawned < timeToMove)
        {
            timeSinceSpawned += Time.deltaTime;
        }
        else
        {
            isMoving = true;
        }

    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            movementFunction();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Dart")
        {
            Destroy(gameObject);
            gameManager.remainingTargets--;
            if (other.tag == "Dart") gameManager.targetHitCount++;
            if (other.tag == "Boundary") gameManager.targetEscapeCount++;
        }
    }


    // 
    // initialization functions
    //

    public void setGameManager(GameStateManager manager)
    {
        gameManager = manager;
    }

    public void initializeMovementStrategy(Level currentLevel, Transform player, int horizontalSpawnOrder, int verticalSpawnOrder, float motionStartDelay)
    {
        switch (currentLevel.packMovementStrategy)
        {
            // mapping a pack strategy to an individual target strategy
            // individual target strategies: 1 = left, 2 = right, 3 = above, 4 = below
            case 1:
                moveDirection = 1;
                break;
            case 2:
                moveDirection = 2;
                break;
            case 3:
                moveDirection = 3;
                break;
            case 4:
                moveDirection = 4;
                break;
            case 5:
                moveDirection = (horizontalSpawnOrder % 2 == 0) ? 3 : 4; // if even, move left; if odd, move right
                break;
            case 6:
                moveDirection = (verticalSpawnOrder % 2 == 0) ? 1 : 2; // if even, move up; if odd, move down
                break;
        }

        float randomizationConstant = currentLevel.randomizationOfSpeed * .005f;
        float finalSpeed = UnityEngine.Random.Range(currentLevel.moveSpeed - randomizationConstant, currentLevel.moveSpeed + randomizationConstant);
        maxNumberOfBounces = currentLevel.numberOfBounces;
        timeToMove = currentLevel.timeToMove + motionStartDelay;        
        moveSpeed = finalSpeed;
        initializeMovementFunction(currentLevel, player);
    }
    private void initializeMovementFunction(Level currentLevel, Transform player)
    {
        float boundaryLeft =  player.position.x - currentLevel.width / 2 - boundaryOffset;
        float boundaryRight = player.position.x + currentLevel.width / 2 + boundaryOffset;
        float boundaryUp = currentLevel.heightFromGround + currentLevel.height + boundaryOffset;
        float boundaryDown = currentLevel.heightFromGround - boundaryOffset;
        if (moveDirection == 1 || moveDirection == 2)
        {
            movementFunction = lateralMovement;
            minBoundary = boundaryLeft;
            maxBoundary = boundaryRight;
            if (moveDirection == 1) moveSpeed *= -1;
        }
        if (moveDirection == 3 || moveDirection == 4)
        {
            minBoundary = boundaryDown;
            maxBoundary = boundaryUp;
            movementFunction = verticalMovement;
            if (moveDirection == 4) moveSpeed *= -1;
        }
    }

    private void lateralMovement()
    {
        transform.position += new Vector3(moveSpeed, 0, 0);
        if (numberOfBouncesTaken < maxNumberOfBounces)
        {
            if (transform.position.x >= maxBoundary || transform.position.x <= minBoundary)
            {
                numberOfBouncesTaken++;
                moveSpeed *= -1;
            }
        }
    }

    private void verticalMovement()
    {
        transform.position += new Vector3(0, moveSpeed, 0);
        if (numberOfBouncesTaken < maxNumberOfBounces)
        {
            if (transform.position.y >= maxBoundary || transform.position.y <= minBoundary)
            {
                numberOfBouncesTaken++;
                moveSpeed *= -1;
            }
        }
    }





}

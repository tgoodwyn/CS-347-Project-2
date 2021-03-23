using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    private bool isMoving = false;
    private int RightOrLeft = 0; // 1 if right, -1 if left, 0 if no horizontal movement
    private int UpOrDown = 0; // 1 if up, -1 if down, 0 if no vertical movement

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary") Destroy(gameObject);
        LevelController.removeBalloonFromLevel(gameObject);
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            float horizontalMovement = LevelController.targetMoveSpeed * RightOrLeft;
            float verticalMovement = LevelController.targetMoveSpeed * UpOrDown;

            transform.position += new Vector3(horizontalMovement, verticalMovement, 0);
        }
    }

    public void setInMotion()
    {
        isMoving = true;
    }

    public bool isInMotion()
    {
        return isMoving;
    }

    public void setMotionParameters(int horizontal, int vertical)
    {
        RightOrLeft = horizontal;
        UpOrDown = vertical;
    }


}

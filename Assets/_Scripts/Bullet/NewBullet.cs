using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBullet : MonoBehaviour
{
    public float TimeToLive = 5f;
    //public Manager manager;
    private void Start()
    {
        Destroy(gameObject, TimeToLive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            Destroy(other.gameObject);
            LevelController.removeBalloonFromLevel(other.gameObject);
            GameStateManager.updateScore();
        }
        else if (other.tag == "Obstacle")
        {
            Destroy(other.gameObject);
        }
    }
}

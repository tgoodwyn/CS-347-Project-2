using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartController : MonoBehaviour
{
    public float TimeToLive = 5f;
    //public GameStateManager gameManager;
    //public Manager manager;
    private void Start()
    {
        //gameManager = GameObject.FindObjectOfType<GameStateManager>();
        Destroy(gameObject, TimeToLive);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Target")
    //    {
    //        Destroy(other.gameObject);
    //        gameManager.remainingTargets.Remove(other.gameObject);
    //    }
    //}
}

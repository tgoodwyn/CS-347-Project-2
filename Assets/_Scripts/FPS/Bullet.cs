using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float TimeToLive = 5f;
    GameObject referenceObject;
    Level1Controller referenceScript;
    List<GameObject> spawnedTargets;
    private void Start()
    {
        referenceObject = GameObject.FindGameObjectWithTag("Manager");
        referenceScript = referenceObject.GetComponent<Level1Controller>();
        spawnedTargets=referenceScript.spawnedTargets;
        Destroy(gameObject, TimeToLive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            Manager.targetsHit++;
            Destroy(other.gameObject);
            foreach (GameObject obj in spawnedTargets)
            {
                
            }
        }
        else if (other.tag == "Obstacle")
        {
            Manager.timeValue = Manager.timeValue - 5;
            Destroy(other.gameObject);
        }
    }
}

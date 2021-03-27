using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float TimeToLive = 5f;
    GameObject referenceObject;
    Level1Controller referenceScript;
    List<GameObject> spawnedTargets;
    int eIncrementor;
    private void Start()
    {
        referenceObject = GameObject.FindGameObjectWithTag("Manager"); // reference to Manager object
        referenceScript = referenceObject.GetComponent<Level1Controller>(); // reference to Level1Controller attached to Manager
        spawnedTargets = referenceScript.spawnedTargets; // reference to spawnedTargets of Level1Controller
        eIncrementor = referenceScript.eIncrementor; // reference to eIncrementor of Level1Controller
        Destroy(gameObject, TimeToLive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            if ( 9 == 9)   // temporarily set to always, press t to enter this level
            {
                Manager.targetsHit++;
                float otherXCoord = other.gameObject.transform.position[0]; // save X and Z coordinates of hit target
                float otherZCoord = other.gameObject.transform.position[2];

                foreach (GameObject obj in spawnedTargets) {   
                    if (obj.transform.position[0] == otherXCoord && obj.transform.position[2] == otherZCoord) { // if both x and z coordinates match, they belong to the same target sets
                        Destroy(obj); // always at least 1 target set remains, not sure why
                        // spawnedTargets.Remove(obj); - when this line is added, only the hit target is detonated 
                    }
                }
            }
            else
            {
                Manager.targetsHit++;
                Destroy(other.gameObject);
            }
            
        }
        else if (other.tag == "Obstacle")
        {
            Manager.timeValue = Manager.timeValue - 5;
            Destroy(other.gameObject);
        }
    }
}

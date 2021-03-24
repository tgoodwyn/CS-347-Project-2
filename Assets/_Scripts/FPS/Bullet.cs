using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float TimeToLive = 5f;
    
    private void Start()
    {
        Destroy(gameObject, TimeToLive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            Manager.targetsHit++;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Obstacle")
        {
            Manager.timeValue = Manager.timeValue - 5;
            Destroy(other.gameObject);
        }
    }
}

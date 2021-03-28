using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : MonoBehaviour
{
    // the bullet prefab and spawn point will be set in the editor 
    public GameObject bullet;
    public Transform BulletSpawnPoint;

    // user view
    public Camera cam;

    // how fast is the bullet
    public float bulletSpeed;

    // automatic? semiautomatic if not
    public bool automatic;

    // fire speed (if automatic)
    public float fireRate;


    // conditions for shooting
    bool triggerPulled, gunReady;

    // For triggering the progressive spawn function in Level1Controller.cs
    GameObject referenceObject;
    Level1Controller referenceScript;
    List<GameObject> spawnedTargets;
    int eIncrementor;

    void Start()
    {
        gunReady = true;


        referenceObject = GameObject.FindGameObjectWithTag("Manager"); // reference to Manager object
        referenceScript = referenceObject.GetComponent<Level1Controller>(); // reference to Level1Controller attached to Manager
        spawnedTargets = referenceScript.spawnedTargets; // reference to spawnedTargets of Level1Controller
    }

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        //Check if semi-automatic or automatic
        if (automatic)
        {
            triggerPulled = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            triggerPulled = Input.GetKeyDown(KeyCode.Mouse0);
        }
        //Debug.Log(triggerPulled);
        // based on fire rate and user input
        if (gunReady && triggerPulled)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        eIncrementor = referenceScript.eIncrementor; // reference to eIncrementor of Level1Controller


        gunReady = false;

        // use the middle of the screen to cast a ray, 
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); 
        RaycastHit hit;

        // whatever it hits will be used to calculate the launch trajectory for the bullet
        Vector3 aimPoint;
        if (Physics.Raycast(ray, out hit))
        {
            aimPoint = hit.point;
            if (eIncrementor > 13)
            {
                Manager.targetsHit++;
                Destroy(hit.transform.gameObject,.07f);
                Level1Controller.tDestructionTimes.Add(Time.time);
                referenceScript.psNextTarget();
            }
            else
            {
                Manager.targetsHit++;
                Destroy(hit.transform.gameObject,.07f);
                Level1Controller.tDestructionTimes.Add(Time.time);
            }
        }
        else
        { // if it doesn't hit anything, just set a default point
            aimPoint = ray.GetPoint(50);
        }
        //Calculate direction from spawn point to ray's hit.point
        Vector3 trajectory = aimPoint - BulletSpawnPoint.position;

        //Instantiate bullet
        GameObject launchedBullet = Instantiate(bullet, BulletSpawnPoint.position, Quaternion.identity); 
        // set the forward direction of the bullet equal to its trajectory 
        launchedBullet.transform.forward = trajectory.normalized;

        // normalize the bullet direction and multiply it by its speed
        launchedBullet.GetComponent<Rigidbody>().AddForce(trajectory.normalized * bulletSpeed, ForceMode.Impulse);
        Manager.shotsValue += 1;
        Invoke("ReadyAgain", fireRate);

    }


    private void ReadyAgain()
    {
        // called after set duration - fireRate
        gunReady = true;
    }

}

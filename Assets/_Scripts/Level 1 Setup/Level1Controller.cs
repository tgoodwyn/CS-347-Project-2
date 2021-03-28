using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Level 1 warmup portion script, n to advance to the next event of the warmup, r to repeat an event, and c to clear targets
public class Level1Controller : MonoBehaviour
{
    public GameObject target;  // Spherical target prefab array
    public GameObject bTarget;
    public Level1Controller l1c;
    public static GameObject targetR;
    private float tRadius = 1.75f;  // Spherical target radii, in the order they are to be spawned
    public int tSpacing = 2; // Inter-target spacing distances, in the order they are to be used
    public int tHeight1 = 25; // Heights at which bottom-most rows of targets are to be spawned, in the order they are to be used
    public int tHeight2 = 45; // Heights at which top-most rows of targets are to be spawned, in the order they are to be used
    public int z1 = -8;  // z coordinates of leftmost target columns
    public int z2 = 8 ;  // z coordinates of rightmost target columns
    public int tcAngle = 30;
    public int tcRadius = 20;

    public static int tn = 0;

    public static List<Vector3> psEvent1Positions = new List<Vector3>(); // standard h
    public static List<Vector3> psEvent2Positions = new List<Vector3>(); // random h
    public static List<Vector3> psEvent3Positions = new List<Vector3>(); // standard v 
    public static List<Vector3> psEvent4Positions = new List<Vector3>(); // random v
    public static List<Vector3> psEvent5Positions = new List<Vector3>(); // standard d
    public static List<Vector3> psEvent6Positions = new List<Vector3>(); // random d
    public static List<Vector3> psEvent7Positions = new List<Vector3>(); // standard c
    public static List<Vector3> psEvent8Positions = new List<Vector3>(); // random c
    public static List<Vector3> psEvent9Positions = new List<Vector3>(); // 10 in random area of size rp1
    public static List<Vector3> psEvent10Positions = new List<Vector3>(); // 10 in random area of size rp2
    public static List<Vector3> psEvent11Positions = new List<Vector3>(); // 10 in random area of size rp3
    public static List<Vector3> psEvent12Positions = new List<Vector3>(); // 10 in small random area of radius 5
    public static List<Vector3> psEvent13Positions = new List<Vector3>(); // 10 spawned randomly such that next point is random distance from previous



    private Manager Manager;
    public List<GameObject> spawnedTargets = new List<GameObject>();
    public int eIncrementor = 0;  // event incrementor

    public static List<float> tDestructionTimes = new List<float>();

    // for checking when the level's been beat
    private int numTargets = 20;

    
    private void spawnTargets()
    {
        /*
        int functionIndex = 0;
        else
        {
            functionIndex = -1;
        }
        */
        // sets projectile speed according to target size
        /*if (index == 0)
        {
            referenceScript.bulletSpeed = 300f;
        }
        else {
            referenceScript.bulletSpeed = 150f;
        }*/

        if (eIncrementor == 0)
        {
            spawnTargetsH1();
        }
        else if (eIncrementor == 1)
        {
            spawnTargetsH2();
        }
        else if (eIncrementor == 2)
        {
            spawnTargetsV1();
        }
        else if (eIncrementor == 3)
        {
            spawnTargetsV2();
        }
        else if (eIncrementor == 4)
        {
            spawnTargetsD1();
        }
        else if (eIncrementor == 5)
        {
            spawnTargetsD2();
        }
        else if (eIncrementor == 6)
        {
            spawnTargetsC1();
        }
        else if (eIncrementor == 7)
        {
            spawnTargetsC2();
        }
        else if (eIncrementor == 8)
        {
            spawnTargetsRPU1();
        }
        else if (eIncrementor == 9 || eIncrementor == 10)
        {
            spawnTargetsRPU2();
        }
        else if (eIncrementor == 11 || eIncrementor == 12 || eIncrementor==13)
        {
            spawnTargetsRPU3();
        }
        else if (eIncrementor > 13)
        {
            psNextTarget();
        }
    } 

    private void genPSPos()
    {
        Level1PSPosGen l1psposgen = GetComponent<Level1PSPosGen>();
        l1psposgen.psEvent1PosGen();
        l1psposgen.psEvent2PosGen();
        l1psposgen.psEvent3PosGen();
        l1psposgen.psEvent4PosGen();
    }

    public void psNextTarget() {
        if (eIncrementor == 14)
        {
            numTargets = 9;
            psNextTargeti(psEvent1Positions);
        }
        else if (eIncrementor == 15)
        {
            numTargets = 14;
            psNextTargeti(psEvent2Positions);
        }
        else if (eIncrementor == 16)
        {
            numTargets = 9;
            psNextTargeti(psEvent3Positions);
        }
        else if (eIncrementor == 17)
        {
            numTargets = 14;
            psNextTargeti(psEvent4Positions);
        }
        
    }
    private void psNextTargeti(List<Vector3> psEventPositions)
    {
        if (tn < psEventPositions.Count)
        {
            GameObject instantiated = Instantiate(target, psEventPositions[tn], Quaternion.identity);
        }
        tn++;
    }

    // Spawns spherical targets in 2 lines extending horizontally, constant spacing between targets
    private void spawnTargetsH1()
    {
        float tandsDistance = tRadius * 2 * 5 + tSpacing * 9; // target and spacing combined distance covered
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;  // where first sphere is to be placed, computed such that rows are horizonatally centered
        Vector3 pos = new Vector3(65, tHeight1, startingPosition);
        for (int i = 0; i < 5; i++)
        {
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            pos.y = tHeight2;
            GameObject instantiated2 = Instantiate(target, pos, Quaternion.identity);
            pos.y = tHeight1;
            spawnedTargets.Add(instantiated);
            spawnedTargets.Add(instantiated2);
            pos.z += 2 * tRadius + tSpacing;
        }
        numTargets = 10;
    }

    // Spawns spherical targets in 2 lines extending horizontally, random spacing between targets
    private void spawnTargetsH2()
    {
        int tQuantity = 14;
        int rngMTSpacing = 10;
        int[] rngTSpacings = new int[tQuantity-2];
        int tSpacingDistance = 0;
        for (int i = 0; i < tQuantity / 2 - 1; i++)
        {
            rngTSpacings[i] = Random.Range(1, rngMTSpacing);
            tSpacingDistance += rngTSpacings[i];
        }
        for (int i = tQuantity/2 -1; i < tQuantity-2; i++)
        {
            rngTSpacings[i] = Random.Range(1, rngMTSpacing);
        }
        float tandsDistance = tRadius * 2 * 7 + tSpacingDistance;
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;
        Vector3 pos = new Vector3(65, tHeight1, startingPosition);
        Vector3 pos2 = new Vector3(65, tHeight2, startingPosition);
        for (int i = 0; i < tQuantity/2; i++)
        {
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            GameObject instantiated2 = Instantiate(target, pos2, Quaternion.identity);
            spawnedTargets.Add(instantiated);
            spawnedTargets.Add(instantiated2);
            if (i < tQuantity/2-1)
            {
                pos.z += 2 * tRadius + rngTSpacings[i];
                pos2.z += 2 * tRadius + rngTSpacings[i + tQuantity / 2 - 1];
            }

        }
        numTargets = tQuantity;
    }

    // Spawns spherical targets in 2 lines extending vertically, constant spacing between targets
    private void spawnTargetsV1()
    {
        Vector3 pos = new Vector3(65, 20, z1);
        for (int i = 0; i < 5; i++)
        {
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            pos.z = z2;
            GameObject instantiated2 = Instantiate(target, pos, Quaternion.identity);
            pos.z = z1;
            spawnedTargets.Add(instantiated);
            spawnedTargets.Add(instantiated2);
            pos.y += 2 * tRadius + tSpacing;
        }
        numTargets = 10;
    }

    // Spawns spherical targets in 2 lines extending vertically, random spacing between targets
    private void spawnTargetsV2()
    {
        int rngMTSpacing = 14;
        Vector3 pos = new Vector3(65, 5, z1);
        Vector3 pos2 = new Vector3(65, 5, z2);
        for (int i = 0; i < 7; i++)
        {
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            GameObject instantiated2 = Instantiate(target, pos2, Quaternion.identity);
            spawnedTargets.Add(instantiated);
            spawnedTargets.Add(instantiated2);
            if (i < 6)
            {
                pos.y += 2 * tRadius + Random.Range(1, rngMTSpacing);
                pos2.y += 2 * tRadius + Random.Range(1, rngMTSpacing);
            }

        }
        numTargets = 14;
    }

    // Spawns spherical targets diagonally in a squished X pattern, constant spacing between targets
    private void spawnTargetsD1()
    {
        float zIncrement = .866f;
        float yIncrement = .5f;
        Vector3 pos = new Vector3(65, 32, 0);
        GameObject centerTarget = Instantiate(target, pos, Quaternion.identity);
        spawnedTargets.Add(centerTarget);
        for (int i = 0; i < 4; i++)
        {
            pos.z += zIncrement * (2 * tRadius + tSpacing);
            pos.y += yIncrement * (2 * tRadius + tSpacing);
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < 4; i++)
        {
            pos.z += zIncrement * -(2 * tRadius + tSpacing);
            pos.y += yIncrement * (2 * tRadius + tSpacing);
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < 4; i++)
        {
            pos.z += zIncrement * (2 * tRadius + tSpacing);
            pos.y += yIncrement * -(2 * tRadius + tSpacing);
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < 4; i++)
        {
            pos.z += zIncrement * -(2 * tRadius + tSpacing);
            pos.y += yIncrement * -(2 * tRadius + tSpacing);
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        numTargets = 17;
    }

    // Spawns spherical targets diagonally in a squished X pattern, random spacing between targets
    private void spawnTargetsD2()
    {
        int lTCount = 3; 
        float zIncrement = .866f;
        float yIncrement = .5f;
        Vector3 pos = new Vector3(65, 32, 0);
        GameObject centerTarget = Instantiate(target, pos, Quaternion.identity);
        spawnedTargets.Add(centerTarget);


        for (int i = 0; i < lTCount; i++)
        {
            int rngTSpacing = Random.Range(1, 6);
            pos.z += zIncrement * (2 * tRadius + tSpacing + rngTSpacing);
            pos.y += yIncrement * (2 * tRadius + tSpacing + rngTSpacing);
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < lTCount; i++)
        {
            int rngTSpacing = Random.Range(1, 6);
            pos.z += zIncrement * -(2 * tRadius + tSpacing + rngTSpacing);
            pos.y += yIncrement * (2 * tRadius + tSpacing + rngTSpacing);
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < lTCount; i++)
        {
            int rngTSpacing = Random.Range(1, 6);
            pos.z += zIncrement * (2 * tRadius + tSpacing + rngTSpacing);
            pos.y += yIncrement * -(2 * tRadius + tSpacing + rngTSpacing);
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < lTCount; i++)
        {
            int rngTSpacing = Random.Range(1, 6);
            pos.z += zIncrement * -(2 * tRadius + tSpacing + rngTSpacing);
            pos.y += yIncrement * -(2 * tRadius + tSpacing + rngTSpacing);
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        numTargets = 1 + lTCount * 4;
    }


    // Spawns spherical targets in a circular pattern, constant spacing between targets
    private void spawnTargetsC1()
    {
        Vector3 circleCenter = new Vector3(65, 32, 0);
        Vector3 pos = new Vector3(65, 0, 0);
        for (int i = 0; i < 360 / tcAngle; i++)
        {
            pos.z = tcRadius * Mathf.Cos(tcAngle * Mathf.Deg2Rad * i);
            pos.y = 32 + tcRadius * Mathf.Sin(tcAngle * Mathf.Deg2Rad * i);
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        numTargets = 360 / tcAngle;
    }

    // Spawns spherical targets in a circular pattern, random spacing between targets
    private void spawnTargetsC2()
    {
        Vector3 cirleCenter = new Vector3(65, 32, 0);
        Vector3 pos = new Vector3(65, 0, 0);
        int tQuantity =  8;
        int rngTCAngle = 7;
        int angle = 0;
        for (int i = 0; i < tQuantity; i++)
        {
            int rf = Random.Range(1, 6);
            int rAngle = rngTCAngle * rf;
            pos.z = tcRadius * Mathf.Cos((angle + rAngle) * Mathf.Deg2Rad);
            pos.y = 32 + tcRadius * Mathf.Sin((angle + rAngle) * Mathf.Deg2Rad);
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
            angle += tcAngle + rAngle;
        }
        numTargets = tQuantity;

    }


    // Spawns spherical targets in a random arrangement, with y and z coordinates generated randomly but such that no 2 targets are within a set distance of one another
    private void spawnTargetsRPU1()
    {
        int tQuantity = 10;
        int distanceThreshold = 7;
        Vector3 pos = new Vector3(65, 0, 0);
        int[] rgPosZs = new int[tQuantity - 1];
        int[] rgPosYs = new int[tQuantity - 1];
        for (int i = 0; i < tQuantity; i++)
        {
            rgUniqueCoords();
            if (i != tQuantity - 1)
            {
                rgPosZs[i] = (int)pos.z;
                rgPosYs[i] = (int)pos.y;
            }

            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        void rgUniqueCoords()
        {
            pos.z = Random.Range(-60, 60);
            pos.y = 32 + Random.Range(-20, 45);
            for (int i = 0; i < tQuantity - 1; i++)
            {
                float distance = Mathf.Sqrt(Mathf.Pow(pos.z - rgPosZs[i], 2) + Mathf.Pow(pos.y - rgPosYs[i], 2));
                if (distance < distanceThreshold)
                {
                    rgUniqueCoords();
                }

            }
        }
        numTargets = tQuantity;
    }
    private void spawnTargetsRPU2()
    {
        int tQuantity = 6;
        int distanceThreshold = 7;
        Vector3 pos = new Vector3(65, 0, 0);
        int[] rgPosZs = new int[tQuantity - 1];
        int[] rgPosYs = new int[tQuantity - 1];
        for (int i = 0; i < tQuantity; i++)
        {
            rgUniqueCoords();
            if (i != tQuantity - 1)
            {
                rgPosZs[i] = (int)pos.z;
                rgPosYs[i] = (int)pos.y;
            }

            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        void rgUniqueCoords()
        {
            pos.z = Random.Range(-30, 30);
            pos.y = 32 + Random.Range(-20, 25);
            for (int i = 0; i < tQuantity - 1; i++)
            {
                float distance = Mathf.Sqrt(Mathf.Pow(pos.z - rgPosZs[i], 2) + Mathf.Pow(pos.y - rgPosYs[i], 2));
                if (distance < distanceThreshold)
                {
                    rgUniqueCoords();
                }

            }
        }
        numTargets = tQuantity;
    }

    private void spawnTargetsRPU3()
    {
        int tQuantity = 4;
        int distanceThreshold = 7;
        Vector3 pos = new Vector3(65, 0, 0);
        int[] rgPosZs = new int[tQuantity - 1];
        int[] rgPosYs = new int[tQuantity - 1];
        for (int i = 0; i < tQuantity; i++)
        {
            rgUniqueCoords();
            if (i != tQuantity - 1)
            {
                rgPosZs[i] = (int)pos.z;
                rgPosYs[i] = (int)pos.y;
            }

            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        void rgUniqueCoords()
        {
            pos.z = Random.Range(-15, 15);
            pos.y = 32 + Random.Range(-15, 15);
            for (int i = 0; i < tQuantity - 1; i++)
            {
                float distance = Mathf.Sqrt(Mathf.Pow(pos.z - rgPosZs[i], 2) + Mathf.Pow(pos.y - rgPosYs[i], 2));
                if (distance < distanceThreshold)
                {
                    rgUniqueCoords();
                }

            }
        }
        numTargets = tQuantity;
    }

    // Spawns mixed spherical/cubical targets in a random arrangement, with y and z coordinates generated randomly but such that no 2 targets are within a set distance of one another
    /*
    private void spawnTargetsRP2(int index)
    {
        int tQuantity = 16;
        int[] distanceThresholds = { 15, 10, 7 };
        Vector3 pos = new Vector3(65, 0, 0);
        int[] rgPosZs = new int[tQuantity - 1];
        int[] rgPosYs = new int[tQuantity - 1];
        for (int i = 0; i < tQuantity; i++)
        {
            rgUniqueCoords();
            if (i != tQuantity - 1)
            {
                rgPosZs[i] = (int)pos.z;
                rgPosYs[i] = (int)pos.y;
            }

            if (i % 2 == 0)
            {
                GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }
            else
            {
                GameObject instantiated = Instantiate(cTargets[index], pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }
        }
        void rgUniqueCoords()
        {
            pos.z = Random.Range(-100, 100);
            pos.y = 32 + Random.Range(-25, 65);
            for (int i = 0; i < tQuantity - 1; i++)
            {
                float distance = Mathf.Sqrt(Mathf.Pow(pos.z - rgPosZs[i], 2) + Mathf.Pow(pos.y - rgPosYs[i], 2));
                if (distance < distanceThresholds[index])
                {
                    rgUniqueCoords();
                }

            }
        }
        if (tQuantity % 2 == 0)
        {
            numTargets = tQuantity / 2;
        }
        else
        {
            numTargets = (tQuantity - 1) / 2;
        }
    }
    */
    // Test level, spawns spherical targets at random x,y,z coordinates
    private void spawnTargetsRP3()
    {
        int tQuantity = 10;
        int distanceThreshold= 7;
        Vector3 pos = new Vector3(65, 0, 0);
        int[] rgPosZs = new int[tQuantity - 1];
        int[] rgPosYs = new int[tQuantity - 1];
        int[] rgPosXs = new int[tQuantity - 1];
        for (int i = 0; i < tQuantity; i++)
        {
            rgUniqueCoords();
            if (i != tQuantity - 1)
            {
                rgPosZs[i] = (int)pos.z;
                rgPosYs[i] = (int)pos.y;
                rgPosXs[i] = (int)pos.x;
            }

            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        void rgUniqueCoords()
        {
            pos.x = Random.Range(20, 65);
            pos.z = Random.Range(-60, 60);
            pos.y = 32 + Random.Range(-25, 20);
            for (int i = 0; i < tQuantity - 1; i++)
            {
                float distance = Mathf.Sqrt(Mathf.Pow(pos.z - rgPosZs[i], 2) + Mathf.Pow(pos.y - rgPosYs[i], 2) + Mathf.Pow(pos.x - rgPosXs[i], 2));
                if (distance < distanceThreshold)
                {
                    rgUniqueCoords();
                }

            }
        }
        numTargets = tQuantity;
    }

    private void spawnTargetsHuman1() {
        int tQuantity = 5;
        int distanceThreshold = 20;
        Vector3 pos = new Vector3(65, 0, 0);
        int[] rgPosZs = new int[tQuantity - 1];
        int[] rgPosXs = new int[tQuantity - 1];
        for (int i = 0; i < tQuantity; i++)
        {
            rgUniqueCoords();
            if (i != tQuantity - 1)
            {
                rgPosZs[i] = (int)pos.z;
                rgPosXs[i] = (int)pos.x;
            }

            pos.y = 14.25f;
            GameObject instantiated = Instantiate(target, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
            pos.y = 6.5f;
            GameObject instantiated2 = Instantiate(bTarget, pos, Quaternion.identity);
            spawnedTargets.Add(instantiated2);

            

        }
        void rgUniqueCoords()
        {
            pos.x = Random.Range(20, 65);
            pos.z = Random.Range(-70, 70);
            for (int i = 0; i < tQuantity - 1; i++)
            {
                float distance = Mathf.Sqrt(Mathf.Pow(pos.z - rgPosZs[i], 2) + Mathf.Pow(pos.x - rgPosXs[i], 2));
                if (distance < distanceThreshold)
                {
                    rgUniqueCoords();
                }

            }
        }
        numTargets = tQuantity;
    }
    


    // Start is called before the first frame update
    void Start()
    {
        genPSPos();
        spawnTargets();
        Manager = GetComponent<Manager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            // extracted into its own method
            nextLevel();
        }
        // Repeats level
        if (Input.GetKeyDown(KeyCode.R))
        {
            repeatSubLevel();
        }
        // Clears targets
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (GameObject obj in spawnedTargets)
            {
                Destroy(obj);
            }
            spawnedTargets.Clear();
        }
        // Enters test level
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (GameObject obj in spawnedTargets)
            {
                Destroy(obj);
            }
            spawnedTargets.Clear();
            spawnTargetsHuman1();
        }

        // returns true if game has been beaten or lost
        if (Manager.getGameStatus())
        {
            if (Input.GetKeyDown(KeyCode.R)) {
                Manager.resetSubLevel();
                repeatSubLevel();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // resets the whole thing
                gameBeaten();
            }

        }

        // Level beaten condition
        if (Manager.targetsHit >= numTargets)
        {
            nextLevel();
        }
    }


    private void repeatSubLevel() {
        Manager.targetsHit = 0;
        foreach (GameObject obj in spawnedTargets)
        {
            Destroy(obj);
        }
        spawnedTargets.Clear();
        spawnTargets();
    }
    private void nextLevel()
    {

        // reset the count of targets hit

        //targetsHit = 20
        //timeLeft = 20
        //shotsTaken = 40
        //20*20 - 20
        Manager.scoreValue += Manager.targetsHit * (int)Manager.timeValue - Manager.shotsValue;
        Manager.targetsHit = 0;
        Manager.shotsValue = 0;
        Manager.timeValue = 30.0f;
        foreach (GameObject obj in spawnedTargets)
        {
            Destroy(obj);
        }
        spawnedTargets.Clear();
        eIncrementor++;
        tn = 0;
        spawnTargets();
    }

    private void gameBeaten()
    {
        // resets the whole thing
        eIncrementor = 0;
        foreach (GameObject obj in spawnedTargets)
        {
            Destroy(obj);
        }
        spawnedTargets.Clear();
        Manager.resetGame();
        spawnTargets();
    }
}



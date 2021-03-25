using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Level 1 warmup portion script, n to advance to the next event of the warmup, r to repeat an event, and c to clear targets
public class Level1Controller : MonoBehaviour
{
    public GameObject[] sTargets = new GameObject[3];  // Spherical target prefab array
    public GameObject[] cTargets = new GameObject[3];  // Cubical target prefab array
    private float[] stRadii = { 4, 2.5f, 1.5f };  // Spherical target radii, in the order they are to be spawned
    private float[] ctSizes = { 7, 4, 2 };  // Cubical target sizes, in the order they are to be spawned
    public int[] tSpacings = { 3, 2, 1 }; // Inter-target spacing distances, in the order they are to be used
    public int[] tHeight1s = { 15, 20, 25 }; // Heights at which bottom-most rows of targets are to be spawned, in the order they are to be used
    public int[] tHeight2s = { 45, 40, 40 }; // Heights at which top-most rows of targets are to be spawned, in the order they are to be used
    public int[] z1s = { -15, -10, -5 };  // z coordinates of leftmost target columns
    public int[] z2s = { 15, 10, 5 };  // z coordinates of rightmost target columns
    public int[] tsAngles = { 30, 24, 18 };
    public int[] tsRadii = { 25, 20, 15 };


    private Manager Manager;
    private List<GameObject> spawnedTargets = new List<GameObject>();
    private int eIncrementor = 0;  // event incrementor

    // for changing bullet speeds
    GameObject referenceObject;
    ProjectileGun referenceScript;


    // for checking when the level's been beat
    private int numTargets = 20;
    private void spawnTargets()
    {
        int index = 0;
        int functionIndex = 0;
        if (eIncrementor < 9)
        {
            index = (int)Mathf.Floor(eIncrementor / 3);
        }
        else if (eIncrementor < 18)
        {
            index = (int)Mathf.Floor(eIncrementor / 3) - 3;
        }
        else if (eIncrementor < 27)
        {
            index = (int)Mathf.Floor(eIncrementor / 3) - 6;
        }
        else if (eIncrementor < 36)
        {
            index = (int)Mathf.Floor(eIncrementor / 3) - 9;
        }
        else if (eIncrementor < 42)
        {
            index = (int)Mathf.Floor((eIncrementor - 36) / 2);
        }
        if (eIncrementor < 9)
        {
            functionIndex = eIncrementor % 3;
        }
        else if (eIncrementor < 18)
        {
            functionIndex = eIncrementor % 3 + 3;
        }
        else if (eIncrementor < 27)
        {
            functionIndex = eIncrementor % 3 + 6;
        }
        else if (eIncrementor < 36)
        {
            functionIndex = eIncrementor % 3 + 9;
        }
        else if (eIncrementor < 42)
        {
            functionIndex = (eIncrementor - 36) % 2 + 12;
        }
        else
        {
            functionIndex = -1;
        }
        // sets projectile speed according to target size
        if (index == 0)
        {
            referenceScript.bulletSpeed = 300f;
        }
        else {
            referenceScript.bulletSpeed = 150f;
        }

        if (functionIndex == 0)
        {
            spawnTargetsH1(index);
        }
        else if (functionIndex == 1)
        {
            spawnTargetsH2(index);
        }
        else if (functionIndex == 2)
        {
            spawnTargetsH3(index);
        }
        else if (functionIndex == 3)
        {
            spawnTargetsV1(index);
        }
        else if (functionIndex == 4)
        {
            spawnTargetsV2(index);
        }
        else if (functionIndex == 5)
        {
            spawnTargetsV3(index);
        }
        else if (functionIndex == 6)
        {
            spawnTargetsD1(index);
        }
        else if (functionIndex == 7)
        {
            spawnTargetsD2(index);
        }
        else if (functionIndex == 8)
        {
            spawnTargetsD3(index);
        }
        else if (functionIndex == 9)
        {
            spawnTargetsC1(index);
        }
        else if (functionIndex == 10)
        {
            spawnTargetsC2(index);
        }
        else if (functionIndex == 11)
        {
            spawnTargetsC3(index);
        }
        else if (functionIndex == 12)
        {
            spawnTargetsRP1(index);
        }
        else if (functionIndex == 13)
        {
            spawnTargetsRP2(index);
        }
    }

    // Spawns spherical targets in 2 lines extending horizontally, constant spacing between targets
    private void spawnTargetsH1(int index)
    {
        float tandsDistance = stRadii[index] * 2 * 10 + tSpacings[index] * 9; // target and spacing combined distance covered
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;  // where first sphere is to be placed, computed such that rows are horizonatally centered
        Vector3 pos = new Vector3(65, tHeight1s[index], startingPosition);
        for (int i = 0; i < 10; i++)
        {
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            pos.y = tHeight2s[index];
            GameObject instantiated2 = Instantiate(sTargets[index], pos, Quaternion.identity);
            pos.y = tHeight1s[index];
            spawnedTargets.Add(instantiated);
            spawnedTargets.Add(instantiated2);
            pos.z += 2 * stRadii[index] + tSpacings[index];
        }
        numTargets = 20;
    }

    // Spawns spherical targets in 2 lines extending horizontally, random spacing between targets
    private void spawnTargetsH2(int index)
    {
        int[] rngMTSpacings = { 10, 10, 10 };
        int[] rngTSpacings = new int[18];
        int tSpacingDistance = 0;
        for (int i = 0; i < 9; i++)
        {
            rngTSpacings[i] = Random.Range(1, rngMTSpacings[index]);
            tSpacingDistance += rngTSpacings[i];
        }
        for (int i = 9; i < 18; i++)
        {
            rngTSpacings[i] = Random.Range(1, rngMTSpacings[index]);
        }
        float tandsDistance = stRadii[index] * 2 * 10 + tSpacingDistance;
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;
        Vector3 pos = new Vector3(65, tHeight1s[index], startingPosition);
        Vector3 pos2 = new Vector3(65, tHeight2s[index], startingPosition);
        for (int i = 0; i < 10; i++)
        {
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            GameObject instantiated2 = Instantiate(sTargets[index], pos2, Quaternion.identity);
            spawnedTargets.Add(instantiated);
            spawnedTargets.Add(instantiated2);
            if (i < 9)
            {
                pos.z += 2 * stRadii[index] + rngTSpacings[i];
                pos2.z += 2 * stRadii[index] + rngTSpacings[i + 9];
            }

        }
        numTargets = 20;
    }

    // Spawns mixed spherical/cubical targets in 2 lines extending horizontally, constant spacing between targets
    private void spawnTargetsH3(int index)
    {
        float tandsDistance = stRadii[index] * 2 * 5 + ctSizes[index] * 4 + tSpacings[index] * 8;
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;
        Vector3 pos = new Vector3(65, tHeight1s[index], startingPosition);
        for (int i = 0; i < 9; i++)
        {
            if (i % 2 == 0)
            {
                GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
                pos.y = tHeight2s[index];
                GameObject instantiated2 = Instantiate(sTargets[index], pos, Quaternion.identity);
                pos.y = tHeight1s[index];
                spawnedTargets.Add(instantiated);
                spawnedTargets.Add(instantiated2);
            }
            else
            {
                GameObject instantiated = Instantiate(cTargets[index], pos, Quaternion.identity);
                pos.y = tHeight2s[index];
                GameObject instantiated2 = Instantiate(cTargets[index], pos, Quaternion.identity);
                pos.y = tHeight1s[index];
                spawnedTargets.Add(instantiated);
                spawnedTargets.Add(instantiated2);
            }
            pos.z += stRadii[index] + ctSizes[index] / 2 + tSpacings[index];
        }
        numTargets = 10;
    }
    // Spawns spherical targets in 2 lines extending vertically, constant spacing between targets
    private void spawnTargetsV1(int index)
    {
        Vector3 pos = new Vector3(65, 5, z1s[index]);
        for (int i = 0; i < 10; i++)
        {
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            pos.z = z2s[index];
            GameObject instantiated2 = Instantiate(sTargets[index], pos, Quaternion.identity);
            pos.z = z1s[index];
            spawnedTargets.Add(instantiated);
            spawnedTargets.Add(instantiated2);
            pos.y += 2 * stRadii[index] + tSpacings[index];
        }
        numTargets = 20;
    }

    // Spawns spherical targets in 2 lines extending vertically, random spacing between targets
    private void spawnTargetsV2(int index)
    {
        int[] rngMTSpacings = { 10, 10, 10 };
        Vector3 pos = new Vector3(65, 5, z1s[index]);
        Vector3 pos2 = new Vector3(65, 5, z2s[index]);
        for (int i = 0; i < 10; i++)
        {
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            GameObject instantiated2 = Instantiate(sTargets[index], pos2, Quaternion.identity);
            spawnedTargets.Add(instantiated);
            spawnedTargets.Add(instantiated2);
            if (i < 9)
            {
                pos.y += 2 * stRadii[index] + Random.Range(1, 4 + index);
                pos2.y += 2 * stRadii[index] + Random.Range(1, 4 + index);
            }

        }
        numTargets = 20;
    }

    // Spawns mixed spherical/cubical targets in 2 lines extending vertically, constant spacing between targets
    private void spawnTargetsV3(int index)
    {
        Vector3 pos = new Vector3(65, 5, z1s[index]);
        for (int i = 0; i < 9; i++)
        {
            if (i % 2 == 0)
            {
                GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
                pos.z = z2s[index];
                GameObject instantiated2 = Instantiate(sTargets[index], pos, Quaternion.identity);
                pos.z = z1s[index];
                spawnedTargets.Add(instantiated);
                spawnedTargets.Add(instantiated2);
            }
            else
            {
                GameObject instantiated = Instantiate(cTargets[index], pos, Quaternion.identity);
                pos.z = z2s[index];
                GameObject instantiated2 = Instantiate(cTargets[index], pos, Quaternion.identity);
                pos.z = z1s[index];
                spawnedTargets.Add(instantiated);
                spawnedTargets.Add(instantiated2);
            }
            pos.y += stRadii[index] + ctSizes[index] / 2 + tSpacings[index];
        }
        numTargets = 10;
    }
    // Spawns spherical targets diagonally in a squished X pattern, constant spacing between targets
    private void spawnTargetsD1(int index)
    {
        float zIncrement = .9285f;
        float yIncrement = .3714f;
        Vector3 pos = new Vector3(65, 32, 0);
        GameObject centerTarget = Instantiate(sTargets[index], pos, Quaternion.identity);
        spawnedTargets.Add(centerTarget);
        for (int i = 0; i < 5; i++)
        {
            pos.z += zIncrement * (2 * stRadii[index] + tSpacings[index]);
            pos.y += yIncrement * (2 * stRadii[index] + tSpacings[index]);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < 5; i++)
        {
            pos.z += zIncrement * -(2 * stRadii[index] + tSpacings[index]);
            pos.y += yIncrement * (2 * stRadii[index] + tSpacings[index]);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < 5; i++)
        {
            pos.z += zIncrement * (2 * stRadii[index] + tSpacings[index]);
            pos.y += yIncrement * -(2 * stRadii[index] + tSpacings[index]);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < 5; i++)
        {
            pos.z += zIncrement * -(2 * stRadii[index] + tSpacings[index]);
            pos.y += yIncrement * -(2 * stRadii[index] + tSpacings[index]);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        numTargets = 21;
    }

    // Spawns spherical targets diagonally in a squished X pattern, random spacing between targets
    private void spawnTargetsD2(int index)
    {
        int slTCount = 3;
        if (index != 0)
        {
            slTCount = 5;
        }
        float zIncrement = .9285f;
        float yIncrement = .3714f;
        Vector3 pos = new Vector3(65, 32, 0);
        GameObject centerTarget = Instantiate(sTargets[index], pos, Quaternion.identity);
        spawnedTargets.Add(centerTarget);


        for (int i = 0; i < slTCount; i++)
        {
            int rngTSpacing = Random.Range(1, 4 + index);
            pos.z += zIncrement * (2 * stRadii[index] + tSpacings[index] + rngTSpacing);
            pos.y += yIncrement * (2 * stRadii[index] + tSpacings[index] + rngTSpacing);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < slTCount; i++)
        {
            int rngTSpacing = Random.Range(1, 4 + index);
            pos.z += zIncrement * -(2 * stRadii[index] + tSpacings[index] + rngTSpacing);
            pos.y += yIncrement * (2 * stRadii[index] + tSpacings[index] + rngTSpacing);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < slTCount; i++)
        {
            int rngTSpacing = Random.Range(1, 4 + index);
            pos.z += zIncrement * (2 * stRadii[index] + tSpacings[index] + rngTSpacing);
            pos.y += yIncrement * -(2 * stRadii[index] + tSpacings[index] + rngTSpacing);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < slTCount; i++)
        {
            int rngTSpacing = Random.Range(1, 4 + index);
            pos.z += zIncrement * -(2 * stRadii[index] + tSpacings[index] + rngTSpacing);
            pos.y += yIncrement * -(2 * stRadii[index] + tSpacings[index] + rngTSpacing);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        numTargets = 1 + slTCount * 4;
    }

    // Spawns mixed spherical/cubical targets diagonally in a squished X pattern, constant spacing between targets
    private void spawnTargetsD3(int index)
    {
        float zIncrement = .9285f;
        float yIncrement = .3714f;
        Vector3 pos = new Vector3(65, 32, 0);
        GameObject centerTarget = Instantiate(sTargets[index], pos, Quaternion.identity);
        spawnedTargets.Add(centerTarget);
        for (int i = 0; i < 5; i++)
        {
            pos.z += zIncrement * (2 * stRadii[index] + tSpacings[index]);
            pos.y += yIncrement * (2 * stRadii[index] + tSpacings[index]);
            if (i % 2 == 0)
            {
                GameObject instantiated = Instantiate(cTargets[index], pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }
            else
            {
                GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }

        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < 5; i++)
        {
            pos.z += zIncrement * -(2 * stRadii[index] + tSpacings[index]);
            pos.y += yIncrement * (2 * stRadii[index] + tSpacings[index]);
            if (i % 2 == 0)
            {
                GameObject instantiated = Instantiate(cTargets[index], pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }
            else
            {
                GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < 5; i++)
        {
            pos.z += zIncrement * (2 * stRadii[index] + tSpacings[index]);
            pos.y += yIncrement * -(2 * stRadii[index] + tSpacings[index]);
            if (i % 2 == 0)
            {
                GameObject instantiated = Instantiate(cTargets[index], pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }
            else
            {
                GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < 5; i++)
        {
            pos.z += zIncrement * -(2 * stRadii[index] + tSpacings[index]);
            pos.y += yIncrement * -(2 * stRadii[index] + tSpacings[index]);
            if (i % 2 == 0)
            {
                GameObject instantiated = Instantiate(cTargets[index], pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }
            else
            {
                GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }
        }
        numTargets = 9;
    }

    // Spawns spherical targets in a circular pattern, constant spacing between targets
    private void spawnTargetsC1(int index)
    {
        Vector3 circleCenter = new Vector3(65, 32, 0);
        Vector3 pos = new Vector3(65, 0, 0);
        for (int i = 0; i < 360 / tsAngles[index]; i++)
        {
            pos.z = tsRadii[index] * Mathf.Cos(tsAngles[index] * Mathf.Deg2Rad * i);
            pos.y = 32 + tsRadii[index] * Mathf.Sin(tsAngles[index] * Mathf.Deg2Rad * i);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        numTargets = 360 / tsAngles[index];
    }

    // Spawns spherical targets in a circular pattern, random spacing between targets
    private void spawnTargetsC2(int index)
    {
        Vector3 cirleCenter = new Vector3(65, 32, 0);
        Vector3 pos = new Vector3(65, 0, 0);
        int[] tQuantities = { 8, 8, 9 };
        int[] rngTSAngles = { 7, 7, 7 };
        int angle = 0;
        for (int i = 0; i < tQuantities[index]; i++)
        {
            int rf = Random.Range(1, 4 + index);
            int rAngle = rngTSAngles[index] * rf;
            pos.z = tsRadii[index] * Mathf.Cos((angle + rAngle) * Mathf.Deg2Rad);
            pos.y = 32 + tsRadii[index] * Mathf.Sin((angle + rAngle) * Mathf.Deg2Rad);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
            angle += tsAngles[index] + rAngle;
        }
        numTargets = tQuantities[index];

    }

    // Spawns mixed spherical/cubical targets in a circular pattern, constant spacing between targets
    private void spawnTargetsC3(int index)
    {
        Vector3 circleCenter = new Vector3(65, 32, 0);
        Vector3 pos = new Vector3(65, 0, 0);
        for (int i = 0; i < 360 / tsAngles[index]; i++)
        {
            pos.z = tsRadii[index] * Mathf.Cos(tsAngles[index] * Mathf.Deg2Rad * i);
            pos.y = 32 + tsRadii[index] * Mathf.Sin(tsAngles[index] * Mathf.Deg2Rad * i);
            if (i % 2 == 0)
            {
                GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }
            else
            {
                GameObject instantiated = Instantiate(cTargets[index], pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }
        }
        if (360 / tsAngles[index] % 2 == 0)
        {
            numTargets = 360 / tsAngles[index] / 2;
        }
        else
        {
            numTargets = (360 / tsAngles[index] - 1) / 2;

        }
    }

    // Spawns spherical targets in a random arrangement, with y and z coordinates generated randomly but such that no 2 targets are within a set distance of one another
    private void spawnTargetsRP1(int index)
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

            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
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
        numTargets = tQuantity;
    }

    // Spawns mixed spherical/cubical targets in a random arrangement, with y and z coordinates generated randomly but such that no 2 targets are within a set distance of one another
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
                GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
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

    // Test level, spawns spherical targets at random x,y,z coordinates
    private void spawnTargetsRP3(int index)
    {
        int tQuantity = 16;
        int[] distanceThresholds = { 15, 10, 7 };
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

            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        void rgUniqueCoords()
        {
            pos.x = Random.Range(65, -20);
            pos.z = Random.Range(-80, 80);
            pos.y = 32 + Random.Range(-25, 20);
            for (int i = 0; i < tQuantity - 1; i++)
            {
                float distance = Mathf.Sqrt(Mathf.Pow(pos.z - rgPosZs[i], 2) + Mathf.Pow(pos.y - rgPosYs[i], 2) + Mathf.Pow(pos.x - rgPosXs[i], 2));
                if (distance < distanceThresholds[index])
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
        referenceObject = GameObject.FindGameObjectWithTag("Player");
        referenceScript = referenceObject.GetComponent<ProjectileGun>();
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
        if (Input.GetKeyDown(KeyCode.O))
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
            spawnTargetsRP3(1);
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



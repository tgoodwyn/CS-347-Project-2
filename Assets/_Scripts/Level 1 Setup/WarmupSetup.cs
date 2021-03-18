using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Level 1 warmup portion script, n to advance to the next event of the warmup, r to repeat an event, and c to clear targets
public class WarmupSetup : MonoBehaviour
{
    public GameObject[] sTargets = new GameObject[3];  // Spherical target prefab array
    public GameObject[] cTargets = new GameObject[3];  // Cubical target prefab array
    public int[] stRadii = { 5, 3, 2 };  // Spherical target radii, in the order they are to be spawned
    public int[] ctSizes = { 9, 5, 3 };  // Cubical target sizes, in the order they are to be spawned
    public int[] tSpacings = { 3, 2, 2 }; // Inter-target spacing distances, in the order in which they are to be used
    public int[] tHeight1s = { 15, 15, 15 }; // Heights at which bottom-most rows of targets are to be spawned, in the order in which they are to be used
    public int[] tHeight2s = { 45, 35, 25 }; // Heights at which top-most rows of targets are to be spawned, in the order in which they are to be used
    public int[] z1s = { -15, -10, -5 };  // z coordinates of leftmost target columns
    public int[] z2s = { 15, 10, 5 };  // z coordinates of rightmost target columns


    public List<GameObject> spawnedTargets = new List<GameObject>();
    private int eIncrementor = 0;  // event incrementor

    public void spawnTargets() {
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
        else if (eIncrementor < 27) {
            index = (int)Mathf.Floor(eIncrementor / 3) - 6;
        }
        if (eIncrementor < 9)
        {
            functionIndex = eIncrementor % 3;
        }
        else if (eIncrementor < 18)
        {
            functionIndex = eIncrementor % 3 + 3;
        }
        else if (eIncrementor < 27) {
            functionIndex = eIncrementor % 3 + 6;
        }
        else
        {
            functionIndex = -1;
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
    }

    // Spawns spherical targets in 2 lines extending horizontally, constant spacing between targets
    public void spawnTargetsH1(int index) {
        int tandsDistance = stRadii[index] * 2 * 10 + tSpacings[index] * 9; // target and spacing combined distance covered
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;  // where first sphere is to be placed, computed such that rows are horizonatally centered
        Vector3 pos = new Vector3(65, tHeight1s[index], startingPosition);
        for (int i = 0; i < 10; i++)
        {
            GameObject instantiated=Instantiate(sTargets[index], pos, Quaternion.identity);
            pos.y = tHeight2s[index];
            GameObject instantiated2 = Instantiate(sTargets[index], pos, Quaternion.identity);
            pos.y = tHeight1s[index];
            spawnedTargets.Add(instantiated);
            spawnedTargets.Add(instantiated2);
            pos.z += 2 * stRadii[index] + tSpacings[index];
        }
    }
    
    // Spawns spherical targets in 2 lines extending horizontally, random spacing between targets
    public void spawnTargetsH2(int index) 
    {
        int[] rngTSpacings = new int[18];
        int tSpacingDistance = 0;
        for (int i = 0; i < 9; i++)
        {
            rngTSpacings[i] = tSpacings[index] * Random.Range(1, 4+index);           
            tSpacingDistance += rngTSpacings[i];
        }
        for (int i = 9; i < 18; i++) {
            rngTSpacings[i] = tSpacings[index] * Random.Range(1, 4 + index);
        }
        int tandsDistance = stRadii[index] * 2 * 10 + tSpacingDistance;
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;
        Vector3 pos = new Vector3(65, tHeight1s[index], startingPosition);
        Vector3 pos2 = new Vector3(65, tHeight2s[index], startingPosition);
        for (int i = 0; i < 10; i++)
        {
            GameObject instantiated=Instantiate(sTargets[index], pos, Quaternion.identity);
            GameObject instantiated2 = Instantiate(sTargets[index], pos2, Quaternion.identity);
            spawnedTargets.Add(instantiated);
            spawnedTargets.Add(instantiated2);
            if (i < 9)
            {
                pos.z += 2 * stRadii[index] + rngTSpacings[i];
                pos2.z += 2 * stRadii[index] + rngTSpacings[i + 9];
            }

        }
    }

    // Spawns mixed spherical/cubical targets in 2 lines extending horizontally, constant spacing between targets
    public void spawnTargetsH3(int index) {
        int tandsDistance = stRadii[index] * 2 * 5 + ctSizes[index] * 4 + tSpacings[index] * 8;
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;
        Vector3 pos = new Vector3(65, tHeight1s[index], startingPosition);
        for (int i = 0; i < 9; i++)
        {
            if (i % 2 == 0)
            {
                GameObject instantiated=Instantiate(sTargets[index], pos, Quaternion.identity);
                pos.y = tHeight2s[index];
                GameObject instantiated2 = Instantiate(sTargets[index], pos, Quaternion.identity);
                pos.y = tHeight1s[index];
                spawnedTargets.Add(instantiated);
                spawnedTargets.Add(instantiated2);
            }
            else
            {
                GameObject instantiated=Instantiate(cTargets[index], pos, Quaternion.identity);
                pos.y = tHeight2s[index];
                GameObject instantiated2 = Instantiate(cTargets[index], pos, Quaternion.identity);
                pos.y = tHeight1s[index];
                spawnedTargets.Add(instantiated);
                spawnedTargets.Add(instantiated2);
            }
            pos.z += stRadii[index] + ctSizes[index] / 2 + tSpacings[index];
        }
    }
    // Spawns spherical targets in 2 lines extending vertically, constant spacing between targets
    public void spawnTargetsV1(int index)
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
    }

    // Spawns spherical targets in 2 lines extending vertically, random spacing between targets
    public void spawnTargetsV2(int index) {
        int[] rngTSpacings = new int[18];
        for (int i = 0; i < 9; i++)
        {
            rngTSpacings[i] = tSpacings[index] * Random.Range(1, 4 + index);
        }
        for (int i = 9; i < 18; i++) {
            rngTSpacings[i] = tSpacings[index] * Random.Range(1, 4 + index);
        }
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
                pos.y += 2 * stRadii[index] + rngTSpacings[i];
                pos2.y += 2 * stRadii[index] + rngTSpacings[i + 9];
            }

        }
    }

    // Spawns mixed spherical/cubical targets in 2 lines extending vertically, constant spacing between targets
    public void spawnTargetsV3(int index)
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
    }

    public void spawnTargetsD1(int index) {
        float zIncrement = .907f;
        float yIncrement = .421f;
        Vector3 pos = new Vector3(65, 32, 0);
        GameObject centerTarget = Instantiate(sTargets[index], pos, Quaternion.identity);
        spawnedTargets.Add(centerTarget);
        for (int i = 0; i < 5; i++) {
            pos.z += zIncrement * (2*stRadii[index] + tSpacings[index]);
            pos.y += yIncrement * (2*stRadii[index] + tSpacings[index]);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < 5; i++)
        {
            pos.z += zIncrement * -(2*stRadii[index] + tSpacings[index]);
            pos.y += yIncrement * (2*stRadii[index] + tSpacings[index]);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < 5; i++)
        {
            pos.z += zIncrement * (2*stRadii[index] + tSpacings[index]);
            pos.y += yIncrement * -(2*stRadii[index] + tSpacings[index]);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < 5; i++)
        {
            pos.z += zIncrement * -(2*stRadii[index] + tSpacings[index]);
            pos.y += yIncrement * -(2*stRadii[index] + tSpacings[index]);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
    }

    public void spawnTargetsD2(int index)
    {
        int ballCount = 3;
        if (index!= 0) {
            ballCount = 5;
        }
        float zIncrement = .907f;
        float yIncrement = .421f;
        int[] rngTSpacings = new int[4*ballCount];
        for (int i = 0; i < 4*ballCount; i++)
        {
            rngTSpacings[i] = tSpacings[index] * Random.Range(1, 4 + index);
        }
        Vector3 pos = new Vector3(65, 32, 0);
        GameObject centerTarget = Instantiate(sTargets[index], pos, Quaternion.identity);
        spawnedTargets.Add(centerTarget);

        
        for (int i = 0; i < ballCount; i++)
        {
            pos.z += zIncrement * (2 * stRadii[index] + tSpacings[index]+rngTSpacings[i]);
            pos.y += yIncrement * (2 * stRadii[index] + tSpacings[index]+rngTSpacings[i]);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < ballCount; i++)
        {
            pos.z += zIncrement * -(2 * stRadii[index] + tSpacings[index]+rngTSpacings[i+ballCount]);
            pos.y += yIncrement * (2 * stRadii[index] + tSpacings[index]+rngTSpacings[i+ballCount]);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < ballCount; i++)
        {
            pos.z += zIncrement * (2 * stRadii[index] + tSpacings[index]+rngTSpacings[i+ballCount*2]);
            pos.y += yIncrement * -(2 * stRadii[index] + tSpacings[index]+rngTSpacings[i+ballCount*2]);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
        pos.z = 0;
        pos.y = 32;
        for (int i = 0; i < ballCount; i++)
        {
            pos.z += zIncrement * -(2 * stRadii[index] + tSpacings[index]+rngTSpacings[i+ballCount*3]);
            pos.y += yIncrement * -(2 * stRadii[index] + tSpacings[index]+rngTSpacings[i+ballCount*3]);
            GameObject instantiated = Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
        }
    }

    public void spawnTargetsD3(int index)
    {
        float zIncrement = .907f;
        float yIncrement = .421f;
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
            else {
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
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnTargets();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) {
            foreach (GameObject obj in spawnedTargets)
            {
                Destroy(obj);
            }
            spawnedTargets.Clear();
            eIncrementor++;
            spawnTargets();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            foreach (GameObject obj in spawnedTargets) {
                Destroy(obj);
            }
            spawnedTargets.Clear();
            spawnTargets();
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            foreach (GameObject obj in spawnedTargets) {
                Destroy(obj);
            }
            spawnedTargets.Clear();
        }
    }
}

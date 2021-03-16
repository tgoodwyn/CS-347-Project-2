using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Level 1 warmup portion script, horizontal portion done so far, press c to clear existing targets and n to advance to the next stage of the warmup
public class WarmupSetup : MonoBehaviour
{
    public GameObject[] sTargets = new GameObject[3];
    public GameObject[] cTargets = new GameObject[3];
    public int[] stRadii = { 5, 3, 2 };
    public int[] ctSizes = { 10, 6, 4 };
    public int[] tSpacings = { 3, 2, 2 };
    public int tHeight = 20;

    public List<GameObject> spawnedTargets = new List<GameObject>();
    private int eIncrementor = 0;

    public void spawnTargets() {
        int index = 0;
        int functionIndex = 0;
        if (eIncrementor < 12)
        {
            index = (int)Mathf.Floor(eIncrementor / 3);
        }
        else if (eIncrementor < 24) {
            index = (int)Mathf.Floor(eIncrementor / 3) - 4;
        }
        if (eIncrementor < 12)
        {
            functionIndex = eIncrementor % 3;
        }
        else if (eIncrementor < 24) { 
        
        }
        if (functionIndex == 0)
        {
            spawnTargetsH1(index);
        }
        else if (functionIndex == 1)
        {
            spawnTargetsH2(index);
        }
        else if (functionIndex == 2) {
            SpawnTargetsH3(index);
        }

    }

    public void spawnTargetsH1(int index) {
        int tandsDistance = stRadii[index] * 2 * 10 + tSpacings[index] * 9;
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;
        Vector3 pos = new Vector3(65, tHeight, startingPosition);
        for (int i = 0; i < 10; i++)
        {
            GameObject instantiated=Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
            pos.z += 2 * stRadii[index] + tSpacings[index];
        }
    }
    
    public void spawnTargetsH2(int index)
    {
        int[] rngTSpacings = new int[9];
        int tSpacingDistance = 0;
        for (int i = 0; i < 9; i++)
        {
            rngTSpacings[i] = tSpacings[index] * Random.Range(1, 4+index);           
            tSpacingDistance += rngTSpacings[i];
        }
        
        int tandsDistance = stRadii[index] * 2 * 10 + tSpacingDistance;
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;
        Vector3 pos = new Vector3(65, tHeight, startingPosition);
        for (int i = 0; i < 10; i++)
        {
            GameObject instantiated=Instantiate(sTargets[index], pos, Quaternion.identity);
            spawnedTargets.Add(instantiated);
            if (i < 9)
            {
                pos.z += 2 * stRadii[index] + rngTSpacings[i];
            }

        }
    }


    public void SpawnTargetsH3(int index) {
        int tandsDistance = stRadii[index] * 2 * 5 + ctSizes[index] * 4 + tSpacings[index] * 8;
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;
        Vector3 pos = new Vector3(65, tHeight, startingPosition);
        for (int i = 0; i < 9; i++)
        {
            if (i % 2 == 0)
            {
                GameObject instantiated=Instantiate(sTargets[index], pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }
            else
            {
                GameObject instantiated=Instantiate(cTargets[index], pos, Quaternion.identity);
                spawnedTargets.Add(instantiated);
            }
            pos.z += stRadii[index] + ctSizes[index] / 2 + tSpacings[index];
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
            eIncrementor++;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1E2Setup : MonoBehaviour
{
    public GameObject sTarget;
    public int tRadius = 5;
    public int tSpacing = 3;

    private int tHeight = 20;
    // Start is called before the first frame update
    void Start()
    {
        spawnTargets();
    }

    public void spawnTargets()
    {
        int[] tSpacings = new int[10];
        int tSpacingDistance = 0;
        for (int i = 0; i < 10; i++) {
            int rgtSpacing= tSpacing * Random.Range(1, 5);
            tSpacings[i] = rgtSpacing;
            if (i != 9) {
                tSpacingDistance += rgtSpacing;
            }
        }
        int tandsDistance = tRadius * 2 * 10 + tSpacingDistance;
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;
        Vector3 pos = new Vector3(65, tHeight, startingPosition);
        for (int i = 0; i < 10; i++)
        {
            Instantiate(sTarget, pos, Quaternion.identity);
            pos.z += 2 * tRadius + tSpacings[i];
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

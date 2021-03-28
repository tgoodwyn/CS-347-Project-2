using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1PSPosGen : MonoBehaviour
{
    private float tRadius = 1.75f;  // Spherical target radii, in the order they are to be spawned
    public int tSpacing = 2; // Inter-target spacing distances, in the order they are to be used
    public int tHeight1 = 25; // Heights at which bottom-most rows of targets are to be spawned, in the order they are to be used
    public int tHeight2 = 45; // Heights at which top-most rows of targets are to be spawned, in the order they are to be used
    public int z1 = -8;  // z coordinates of leftmost target columns
    public int z2 = 8;  // z coordinates of rightmost target columns
    public int tcAngle = 30;
    public int tcRadius = 20;
    public void psEvent1PosGen() {
        int tQuantity = 9;
        float tandsDistance = tRadius * 2 * 5 + tSpacing * 9; // target and spacing combined distance covered
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;  // where first sphere is to be placed, computed such that rows are horizonatally centered
        Vector3 pos = new Vector3(65, tHeight1, startingPosition);
        for (int i = 0; i < 5; i++)
        {
            Level1Controller.psEvent1Positions.Add(pos);
            pos.z += 2 * tRadius + tSpacing;
        }
        pos.z -= 2*(2 * tRadius + tSpacing);
        for (int i = 0; i < 4; i++) {
            Level1Controller.psEvent1Positions.Add(pos);
            pos.z -= 2 * tRadius + tSpacing;
        }
    }
    public void psEvent2PosGen()
    {
        int tQuantity = 14;
        int rngMTSpacing = 10;
        int[] rngTSpacings = new int[tQuantity/2-1];
        int tSpacingDistance = 0;
        for (int i = 0; i < tQuantity / 2 - 1; i++)
        {
            rngTSpacings[i] = Random.Range(1, rngMTSpacing);
            tSpacingDistance += rngTSpacings[i];
        }
        float tandsDistance = tRadius * 2 * 7 + tSpacingDistance;
        float startingPosition = -180 + (360 - tandsDistance) / 2 + 5;
        Vector3 pos = new Vector3(65, tHeight1, startingPosition);
        for (int i = 0; i < tQuantity / 2; i++)
        {
            Level1Controller.psEvent2Positions.Add(pos);
            if (i < tQuantity / 2 - 1)
            {
                pos.z += 2 * tRadius + rngTSpacings[i];
            }

        }
        pos.z -= (2 * tRadius + Random.Range(1, rngMTSpacing));
        for (int i = 0; i < tQuantity / 2; i++)
        {
            Level1Controller.psEvent2Positions.Add(pos);
            if (i < tQuantity / 2 - 1)
            {
                pos.z -= 2 * tRadius + Random.Range(1,rngMTSpacing);
            }

        }
    }

    public void psEvent3PosGen()
    {
        int tQuantity = 9;
        Vector3 pos = new Vector3(65, 20, 0);
        for (int i = 0; i < 5; i++)
        {
            Level1Controller.psEvent3Positions.Add(pos);
            pos.y += 2 * tRadius + tSpacing;
        }
        pos.y -= 2 * (2 * tRadius + tSpacing);
        for (int i = 0; i < 4; i++)
        {
            Level1Controller.psEvent3Positions.Add(pos);
            pos.y -= 2 * tRadius + tSpacing;
        }
    }

    public void psEvent4PosGen()
    {
        int tQuantity = 14;
        int rngMTSpacing = 14;
        Vector3 pos = new Vector3(65, 5, 0);
        for (int i = 0; i < 7; i++)
        {
            Level1Controller.psEvent4Positions.Add(pos);
            if (i < 6)
            {
                pos.y += 2 * tRadius + Random.Range(1, rngMTSpacing);
            }
        }
        pos.y -= 2 * tRadius + Random.Range(1, rngMTSpacing);
        for (int i = 0; i < 7; i++)
        {
            Level1Controller.psEvent4Positions.Add(pos);
            pos.y -= 2 * tRadius + Random.Range(1, rngMTSpacing);
            if (pos.y < 5)
            {
                pos.y = 5;
            }
        }
    }

    public void psEvent5PosGen()
    {

    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

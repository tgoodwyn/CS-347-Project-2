using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1PSPosGen : MonoBehaviour
{
    private float tRadius = 1.75f;  // Spherical target radii, in the order they are to be spawned
    public int tSpacing = 1; // Standard inter-target spacing distances, in the order they are to be used
    public int tHeight1 = 25; // Heights at which bottom-most rows of targets are to be spawned, in the order they are to be used
    public int tHeight2 = 45; // Heights at which top-most rows of targets are to be spawned, in the order they are to be used
    public int z1 = -8;  // z coordinates of leftmost target columns
    public int z2 = 8;  // z coordinates of rightmost target columns
    public int tcAngle = 30;
    public int tcRadius = 20;
    public void psEvent1PosGen() {
        int tQuantity = 15;
        float tandsDistance = tRadius * 2 * 5 + tSpacing * 9; // target and spacing combined distance covered
        float startingPosition = -180 + (360 - tandsDistance) / 2 + tRadius;  // where first sphere is to be placed, computed such that rows are horizonatally centered
        Vector3 pos = new Vector3(65, tHeight1, startingPosition);
        for (int i = 0; i < 8; i++)
        {
            Level1Controller.psEvent1Positions.Add(pos);
            pos.z += 2 * tRadius + tSpacing;
        }
        pos.z -= 2*(2 * tRadius + tSpacing);
        for (int i = 0; i < 7; i++) {
            Level1Controller.psEvent1Positions.Add(pos);
            pos.z -= 2 * tRadius + tSpacing;
        }
    }
    public void psEvent2PosGen()
    {
        int tQuantity = 16;  // only evens
        int rngMTSpacing = 10;
        int[] rngTSpacings = new int[tQuantity/2-1];
        int tSpacingDistance = 0;
        for (int i = 0; i < tQuantity / 2 - 1; i++)
        {
            rngTSpacings[i] = Random.Range(1, rngMTSpacing);
            tSpacingDistance += rngTSpacings[i];
        }
        float tandsDistance = tRadius * 2 + tSpacingDistance;
        float startingPosition = -180 + (360 - tandsDistance) / 2 + tRadius;
        Vector3 pos = new Vector3(65, tHeight1, startingPosition);
        for (int i = 0; i < tQuantity / 2; i++)
        {
            Level1Controller.psEvent2Positions.Add(pos);
            if (i < tQuantity / 2 - 1)
            {
                pos.z += rngTSpacings[i];
            }

        }
        pos.z -= Random.Range(1, rngMTSpacing);
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
        int tQuantity = 15;
        int tSpacingM = 2;
        float tandsDistance = tRadius * 2 + tSpacingM * 7; // target and spacing combined distance covered
        float startingPosition = -180 + (360 - tandsDistance) / 2 + tRadius;  // where first sphere is to be placed, computed such that rows are horizonatally centered
        Vector3 pos = new Vector3(65, tHeight1, startingPosition);
        for (int i = 0; i < 8; i++)
        {
            Level1Controller.psEvent3Positions.Add(pos);
            pos.z += tSpacingM;
        }
        pos.z -= 2 * tSpacingM;
        for (int i = 0; i < 7; i++)
        {
            Level1Controller.psEvent3Positions.Add(pos);
            pos.z -= tSpacingM;
        }
    }
    public void psEvent4PosGen()
    {
        int tQuantity = 16;  // only evens
        float rngMTSpacing = 4.5f;
        float[] rngTSpacings = new float[tQuantity / 2 - 1];
        float tSpacingDistance = 0;
        for (int i = 0; i < tQuantity / 2 - 1; i++)
        {
            rngTSpacings[i] = Random.Range(0.5f, rngMTSpacing);
            tSpacingDistance += rngTSpacings[i];
        }
        float tandsDistance = tRadius * 2 + tSpacingDistance;
        float startingPosition = -180 + (360 - tandsDistance) / 2 + tRadius;
        Vector3 pos = new Vector3(65, tHeight1, startingPosition);
        for (int i = 0; i < tQuantity / 2; i++)
        {
            Level1Controller.psEvent4Positions.Add(pos);
            if (i < tQuantity / 2 - 1)
            {
                pos.z += rngTSpacings[i];
            }

        }
        pos.z -= Random.Range(0.5f, rngMTSpacing);
        for (int i = 0; i < tQuantity / 2; i++)
        {
            Level1Controller.psEvent4Positions.Add(pos);
            if (i < tQuantity / 2 - 1)
            {
                pos.z -= Random.Range(0.5f, rngMTSpacing);
            }

        }
    }

    public void psEvent5PosGen()
    {
        int tQuantity = 11;
        Vector3 pos = new Vector3(65, 15, 0);
        for (int i = 0; i < 6; i++)
        {
            Level1Controller.psEvent5Positions.Add(pos);
            pos.y += 2 * tRadius + tSpacing;
        }
        pos.y -= 2 * (2 * tRadius + tSpacing);
        for (int i = 0; i < 5; i++)
        {
            Level1Controller.psEvent5Positions.Add(pos);
            pos.y -= 2 * tRadius + tSpacing;
        }
    }

    public void psEvent6PosGen()
    {
        int tQuantity = 14;
        int rngMTSpacing = 10;
        Vector3 pos = new Vector3(65, 25, 0);
        for (int i = 0; i < 7; i++)
        {
            Level1Controller.psEvent6Positions.Add(pos);
            if (i < 6)
            {
                pos.y += Random.Range(1, rngMTSpacing);
            }
        }
        pos.y -= Random.Range(1, rngMTSpacing);
        for (int i = 0; i < 7; i++)
        {
            Level1Controller.psEvent6Positions.Add(pos);
            pos.y -= Random.Range(1, rngMTSpacing);
            if (pos.y < 5)
            {
                pos.y = 5;
            }
        }
    }

    public void psEvent7PosGen()
    {
        int tQuantity = 11;
        int tSpacingM = 3;
        Vector3 pos = new Vector3(65, 15, 0);
        for (int i = 0; i < 6; i++)
        {
            Level1Controller.psEvent7Positions.Add(pos);
            pos.y += tSpacingM;
        }
        pos.y -= 2 * tSpacingM;
        for (int i = 0; i < 5; i++)
        {
            Level1Controller.psEvent7Positions.Add(pos);
            pos.y -= tSpacingM;
        }
    }
    
    public void psEvent8PosGen()
    {
        int tQuantity = 14;
        float rngMTSpacing = 4.5f;
        Vector3 pos = new Vector3(65, 25, 0);
        for (int i = 0; i < 7; i++)
        {
            Level1Controller.psEvent8Positions.Add(pos);
            if (i < 6)
            {
                pos.y += Random.Range(0.5f, rngMTSpacing);
            }
        }
        pos.y -= Random.Range(0.5f, rngMTSpacing);
        for (int i = 0; i < 7; i++)
        {
            Level1Controller.psEvent8Positions.Add(pos);
            pos.y -= Random.Range(0.5f, rngMTSpacing);
            if (pos.y < 5)
            {
                pos.y = 5;
            }
        }
    }

    public void psEvent9PosGen()
    {
        int tQuantity = 20;
        int tSpacingM = 7;
        int yDelta = 7;
        Vector3 pos = new Vector3(65, 25-yDelta, -(10*tSpacingM+2*tRadius)/2+tRadius);
        for (int i = 0; i < 10; i++)
        {
            Level1Controller.psEvent9Positions.Add(pos);
            pos.z += tSpacingM;
            if (i % 2 == 0)
            {
                pos.y = 25 + yDelta;
            }
            else
            {
                pos.y = 25 - yDelta;
            }
        }
        pos.z -= 2*tSpacingM;
        for (int i = 0; i < 10; i++)
        {
            Level1Controller.psEvent9Positions.Add(pos);
            pos.z -= tSpacingM;
            if (i % 2 == 0)
            {
                pos.y = 25 + yDelta;
            }
            else
            {
                pos.y = 25 - yDelta;
            }
        }
    }

    public void psEvent10PosGen()
    {
        int tQuantity = 20;
        float rngMTSpacingZ = 8.0f;
        float rngMTSpacingY = 10.0f;
        Vector3 pos = new Vector3(65, 25, -(10 * rngMTSpacingZ/2 + 2 * tRadius) / 2 + tRadius);
        for (int i = 0; i < 10; i++)
        {
            Level1Controller.psEvent10Positions.Add(pos);
            pos.z += Random.Range(.5f, rngMTSpacingZ);
            pos.y = 25 + Random.Range(-rngMTSpacingY, rngMTSpacingY);
        }
        pos.z -= Random.Range(.5f,rngMTSpacingZ);
        for (int i = 0; i < 10; i++)
        {
            Level1Controller.psEvent10Positions.Add(pos);
            pos.z -= Random.Range(.5f, rngMTSpacingZ);
            pos.y = 25 + Random.Range(-rngMTSpacingY, rngMTSpacingY);
        }
    }

    public void psEvent11PosGen()
    {
        Vector3 circleCenter = new Vector3(65, 32, 0);
        Vector3 pos = new Vector3(65, 0, 0);
        for (int i = 0; i < 360 / tcAngle; i++)
        {
            pos.z = tcRadius * Mathf.Cos(tcAngle * Mathf.Deg2Rad * i);
            pos.y = 32 + tcRadius * Mathf.Sin(tcAngle * Mathf.Deg2Rad * i);
            Level1Controller.psEvent11Positions.Add(pos);
        }
    }

    public void psEvent12PosGen()
    {
        Vector3 cirleCenter = new Vector3(65, 32, 0);
        Vector3 pos = new Vector3(65, 0, 0);
        int tQuantity = 8;
        int rngTCAngle = 7;
        int angle = 0;
        for (int i = 0; i < tQuantity; i++)
        {
            int rf = Random.Range(1, 6);
            int rAngle = rngTCAngle * rf;
            pos.z = tcRadius * Mathf.Cos((angle + rAngle) * Mathf.Deg2Rad);
            pos.y = 32 + tcRadius * Mathf.Sin((angle + rAngle) * Mathf.Deg2Rad);
            Level1Controller.psEvent12Positions.Add(pos);
            angle += tcAngle + rAngle;
        }
    }

    public void psEvent13PosGen()
    {
        int tQuantity = 15;
        int distanceThreshold = 5;
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

            Level1Controller.psEvent13Positions.Add(pos);
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
    }

    public void psEvent14PosGen()
    {
        int tQuantity = 15;
        int distanceThreshold = 5;
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

            Level1Controller.psEvent14Positions.Add(pos);
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
    }

    public void psEvent15PosGen()
    {
        int tQuantity = 15;
        int distanceThreshold = 5;
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

            Level1Controller.psEvent15Positions.Add(pos);
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
    }

    public void psEvent16PosGen()
    {
        int tQuantity = 15;
        float distanceThreshold = 1.5f;
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

            Level1Controller.psEvent16Positions.Add(pos);
        }
        void rgUniqueCoords()
        {
            pos.z = Random.Range(-6, 6);
            pos.y = 32 + Random.Range(-6, 6);
            for (int i = 0; i < tQuantity - 1; i++)
            {
                float distance = Mathf.Sqrt(Mathf.Pow(pos.z - rgPosZs[i], 2) + Mathf.Pow(pos.y - rgPosYs[i], 2));
                if (distance < distanceThreshold)
                {
                    rgUniqueCoords();
                }

            }
        }
    }

    public void psEvent17PosGen()
    {
        int tQuantity = 15;

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

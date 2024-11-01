using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMapGeneration
{


    public static float[,] GenerateNoiseMap(int mapDepth, int mapWidth, float scale)
    {
        //with the mapDepth and mapWidth coordinates
        float[,] noiseMap = new float[mapDepth, mapWidth];
        for (int zIndex = 0; zIndex < mapDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < mapWidth; xIndex++)
            {
                // calculate sample indices based on the coordinates and the scale
                float sampleX = xIndex / scale;
                float sampleZ = zIndex / scale;
                // generate noise value using PerlinNoise
                float noise = Mathf.PerlinNoise(sampleX, sampleZ);
                noiseMap[zIndex, xIndex] = noise;
            }
        }
        return noiseMap;
    }
}

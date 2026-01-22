using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bruit
{
    public static float[,] GenerateBruitMap(int mapWidth , int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {

        System.Random prng = new System.Random(seed);

        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000,100000) + offset.x;
            float offsetY= prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if(scale <=0)
        {
            scale = 0.0001f;
        }

        float maxBruitHeight = float.MinValue;
        float minBruitHeight = float.MaxValue;

        float halfWidth = mapWidth / 2;
        float halfHeight = mapWidth / 2;


        float[,] bruitMap = new float[mapWidth, mapHeight]; ;
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float bruitHeight = 0;

                for(int i=0; i<octaves; i++) { 

                    float sampleX = (x-halfWidth) / scale* frequency + octaveOffsets[i].x;
                    float sampleY = (y-halfHeight) / scale*frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    bruitHeight += perlinValue * amplitude;


                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if(bruitHeight > maxBruitHeight)
                {
                    maxBruitHeight=bruitHeight;
                } else if (bruitHeight < minBruitHeight)
                {
                    minBruitHeight=bruitHeight;
                }

                bruitMap[x,y]=bruitHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                bruitMap[x, y] = Mathf.InverseLerp(minBruitHeight, maxBruitHeight, bruitMap[x, y]);
            }
        }
        return bruitMap;
    }
        
}

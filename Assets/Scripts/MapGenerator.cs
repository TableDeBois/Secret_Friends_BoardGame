using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A retirer ?
[ExecuteInEditMode]
//######
public class MapGenerator : MonoBehaviour
{

    public enum DrawMode { BruitMap,ColourMap, Mesh}
    public DrawMode drawMode;


    const int mapChunkSize = 241;

    [Range(0,6)]
    public int niveauDetail;

    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    //A retirer ??#######
    // 2. Cette fonction est appelée automatiquement par Unity après une sauvegarde
    // ou une recompilation des scripts. C'est elle qui répare ta texture !
    private void OnEnable()
    {
        // On vérifie qu'on a bien des régions configurées pour éviter une erreur au tout premier ajout du script
        if (regions != null && regions.Length > 0)
        {
            GenerateMap();
        }
    }
    //#################

    public void GenerateMap()
    {
        float[,] bruitMap = Bruit.GenerateBruitMap(mapChunkSize, mapChunkSize, seed ,noiseScale,octaves,persistance,lacunarity, offset);


        Color[] colourMap = new Color[mapChunkSize*mapChunkSize];

        for (int y = 0; y < mapChunkSize; y++)
        {
            for(int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = bruitMap[x,y];
                for (int i=0; i < regions.Length; i++)
                {
                    if(currentHeight<= regions[i].height)
                    {
                        colourMap[y * mapChunkSize + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindAnyObjectByType<MapDisplay>();
        if (drawMode == DrawMode.BruitMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(bruitMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap,mapChunkSize,mapChunkSize));
        }
        else if(drawMode== DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(bruitMap, meshHeightMultiplier, meshHeightCurve, niveauDetail),TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));
        }
    }



    private void OnValidate()
    {
        if (lacunarity < 1) { lacunarity = 1;}
        if (octaves < 0) { octaves = 0;}

    }

}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}

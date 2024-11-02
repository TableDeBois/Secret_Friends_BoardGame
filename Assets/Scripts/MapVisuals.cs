using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapVisuals : MonoBehaviour
{

    private Grille<float> grille;
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;
    float[,] noisemap;
    private GameObject gridObject;

    public MapVisuals(Grille<float> grille, MeshFilter meshFilter)
    {
        this.grille = grille;

        gridObject = new GameObject("Grid Mesh");
        gridObject.AddComponent<MeshFilter>();
        gridObject.AddComponent<MeshRenderer>();


        MeshUtils.CreateEmptyMeshArrays(grille.getWidth() * grille.getHeigth(), out this.vertices, out this.uv, out this.triangles);

        int vertexIndex = 0;
        int trianglesIndex = 0;

        noisemap = NoiseMapGeneration.GenerateNoiseMap(grille.getHeigth(), grille.getWidth() , grille.getTileSize());



        for (int i = 0; i < grille.getWidth(); i++)
        {
            for (int j = 0; j < grille.getHeigth(); j++)
            {
                int index = i*grille.getHeigth()+j;

                Vector3 quadPos = new Vector3(i,0,j);
                float noise = noisemap[i,j];

                MeshUtils.AddToMeshArray(quadPos, noise,index,i,j,grille.getTileSize(), vertices, uv, triangles);

                grille.setValue(i,j, noise);

                vertexIndex += 4;     // Avance de 4 sommets par quad
                trianglesIndex += 6;   // Avance de 6 indices de triangles par quad
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        meshFilter.mesh = mesh;
    }

    public void SetGrille(Grille<float> grid)
    {
        this.grille = grid;
    }
}

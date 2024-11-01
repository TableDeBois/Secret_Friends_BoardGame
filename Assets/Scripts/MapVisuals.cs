using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapVisuals
{

    private Grille grille;
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;
    float[,] noisemap;
    private GameObject gridObject;

    public MapVisuals(Grille grille, MeshFilter meshFilter)
    {
        this.grille = grille;

        gridObject = new GameObject("Grid Mesh");
        gridObject.AddComponent<MeshFilter>();
        gridObject.AddComponent<MeshRenderer>();


        CreateEmptyMeshArrays(grille.getWidth() * grille.getHeigth(), out this.vertices, out this.uv, out this.triangles);

        int vertexIndex = 0;
        int trianglesIndex = 0;

        noisemap = NoiseMapGeneration.GenerateNoiseMap(grille.getHeigth(), grille.getWidth() , grille.getTileSize());



        for (int i = 0; i < grille.getWidth(); i++)
        {
            for (int j = 0; j < grille.getHeigth(); j++)
            {
                Vector3 quadPos = new Vector3(i,0,j);
                float noise = noisemap[i,j];

                AddToMeshArray(quadPos, noise, vertexIndex, trianglesIndex);

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


    private void AddToMeshArray(Vector3 quadPos, float noise, int vertexIndex, int triangleIndex)
    {
        // Positionne les sommets en utilisant la hauteur
        vertices[vertexIndex] = quadPos + new Vector3(0, noise, 0);             // Bas-gauche
        vertices[vertexIndex + 1] = quadPos + new Vector3(1, noise, 0);         // Bas-droit
        vertices[vertexIndex + 2] = quadPos + new Vector3(0, noise, 1);         // Haut-gauche
        vertices[vertexIndex + 3] = quadPos + new Vector3(1, noise, 1);         // Haut-droit

        // Assigne des UV simples pour chaque sommet
        uv[vertexIndex] = new Vector2(0, 0);                  // UV pour le bas-gauche
        uv[vertexIndex + 1] = new Vector2(1, 0);              // UV pour le bas-droit
        uv[vertexIndex + 2] = new Vector2(0, 1);              // UV pour le haut-gauche
        uv[vertexIndex + 3] = new Vector2(1, 1);              // UV pour le haut-droit

        // Définit deux triangles pour le quad
        triangles[triangleIndex] = vertexIndex;
        triangles[triangleIndex + 1] = vertexIndex + 2;
        triangles[triangleIndex + 2] = vertexIndex + 1;

        triangles[triangleIndex + 3] = vertexIndex + 1;
        triangles[triangleIndex + 4] = vertexIndex + 2;
        triangles[triangleIndex + 5] = vertexIndex + 3;
    }

    public static void CreateEmptyMeshArrays(int quadCount, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
    {
        vertices = new Vector3[4*quadCount];
        uvs = new Vector2[4*quadCount];
        triangles = new int[6*quadCount];
    }

}

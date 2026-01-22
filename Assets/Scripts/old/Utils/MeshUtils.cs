using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MeshUtils
{


    public static void AddToMeshArray(Vector3 quadPos, float noise, int index,int i,int j, float tilesize, Vector3[] vertices, Vector2[] uv, int[] triangles)
    {
        // Positionne les sommets en utilisant la hauteur
        vertices[index*4+0] = quadPos + new Vector3(tilesize*i,noise,tilesize*j);             // Bas-gauche
        vertices[index * 4 + 1] = quadPos + new Vector3(tilesize*i,noise,tilesize*(j+1));         // Bas-droit
        vertices[index * 4 + 2] = quadPos + new Vector3(tilesize*(i+1), noise, tilesize*(j+1));         // Haut-gauche
        vertices[index * 4 + 3] = quadPos + new Vector3(tilesize*(i+1), noise, tilesize*j);         // Haut-droit

        // Assigne des UV simples pour chaque sommet
        uv[index * 4+0] = new Vector2(0, 1);                  // UV pour le bas-gauche
        uv[index * 4 + 1] = new Vector2(1, 1);              // UV pour le bas-droit
        uv[index * 4 + 2] = new Vector2(0, 0);              // UV pour le haut-gauche
        uv[index * 4 + 3] = new Vector2(1, 0);              // UV pour le haut-droit

        // Définit deux triangles pour le quad
        triangles[index * 6 + 0] = index * 4 + 0;
        triangles[index * 6 + 1] = index * 4 + 1;
        triangles[index * 6 + 2] = index * 4 + 2;

        triangles[index * 6 + 3] = index * 4 + 2;
        triangles[index * 6 + 4] = index * 4 + 1;
        triangles[index * 6 + 5] = index * 4 + 3;
    }

    public static void CreateEmptyMeshArrays(int quadCount, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
    {
        vertices = new Vector3[4 * quadCount];
        uvs = new Vector2[4 * quadCount];
        triangles = new int[6 * quadCount];
    }
}


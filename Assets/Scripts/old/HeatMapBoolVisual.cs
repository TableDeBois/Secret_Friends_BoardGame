using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HeatMapBoolVisual : MonoBehaviour
{
    
    private Grille<bool> grid;
    private Mesh mesh;
    private bool updateMesh;


    private void Awake()
    {
        mesh=new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }


    public void SetGrid(Grille<bool> grid)
    {
        this.grid = grid;
        UpdateHeatMapVisual();

        grid.OnGridValueChanged += Grid_OnGridValueChanged;

    }

    private void Grid_OnGridValueChanged(object sender, Grille<bool>.OnGridValueChangedEventArgs e)
    {
        updateMesh = true;
    }


    private void LateUpdate()
    {
        if (updateMesh)
        {
            updateMesh = false;
            UpdateHeatMapVisual();
        }
    }

    private void UpdateHeatMapVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.getWidth() * grid.getHeigth(), out Vector3[] vertices, out Vector2[] uvs, out int[] triangles);

        for (int i = 0; i < grid.getWidth(); i++)
        {
            for (int j = 0; j < grid.getHeigth(); j++)
            {
                int index = i * grid.getHeigth() + j;
                Vector3 quadSize = new Vector3(1, 1) * grid.getTileSize();
                Vector3 quadPos = new Vector3(i, 0, j);
                bool gridValue = grid.getValue(i, j);
                //Normalisation de la valeur & such, ici que bool
                float gridValueNormalized = gridValue ? 1f : 0f;
                Vector2 gridValueUV = new Vector2(gridValueNormalized, 0f);
                MeshUtils.AddToMeshArray(quadPos, gridValueNormalized, index, i, j, grid.getTileSize(), vertices, uvs, triangles);


            }
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    //Ancienne solution en commentaire
    public void DrawTexture(Texture2D texture)
    {

        SetTexture(textureRenderer, texture);
        //textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale= new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        SetTexture(meshRenderer, texture);
        //meshRenderer.sharedMaterial.mainTexture= texture;
    }

    void SetTexture(Renderer renderer, Texture2D texture)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();

        // On récupère les propriétés actuelles (pour ne pas écraser d'autres réglages)
        renderer.GetPropertyBlock(propBlock);

        // "_MainTex" est le nom standard de la texture pour le Shader Standard.
        // Si tu utilises URP plus tard, ce sera peut-être "_BaseMap".
        propBlock.SetTexture("_MainTex", texture);

        // On applique le bloc au renderer
        renderer.SetPropertyBlock(propBlock);
    }

}

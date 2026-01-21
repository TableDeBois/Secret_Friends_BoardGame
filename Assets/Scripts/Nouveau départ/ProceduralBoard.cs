using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralBoard : MonoBehaviour
{
    [Header("Dimensions")]
    public int width = 50;
    public int depth = 50;

    [Header("Noise Settings")]
    public float scale = 20f;      // Zoom du bruit
    public float heightMult = 10f;  // Hauteur max des montagnes

    // C'est ici la nouveauté pour les zones plates !
    // Axe X de la courbe = Valeur du bruit (0 à 1)
    // Axe Y de la courbe = Hauteur réelle (0 à 1)
    public AnimationCurve heightCurve;

    public float offsetX = 100f;   // Pour varier la carte (seed)
    public float offsetY = 100f;

    [Header("Biomes (Couleurs)")]
    public Gradient terrainGradient; // On configurera ça dans l'inspecteur

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Color[] colors;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // Randomisation de la seed
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);

        CreateShape();
        UpdateMesh();

        // IMPORTANT : Ajouter le collider pour que le Raycast du PathGenerator marche
        if (GetComponent<MeshCollider>() == null) gameObject.AddComponent<MeshCollider>();
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void CreateShape()
    {
        vertices = new Vector3[(width + 1) * (depth + 1)];
        colors = new Color[vertices.Length];

        for (int i = 0, z = 0; z <= depth; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                // 1. Calcul du Perlin Noise de base (entre 0 et 1 théoriquement)
                // On ajoute 0.01f pour éviter les divisions par zéro ou les entiers ronds
                float xCoord = (float)x / width * scale + offsetX;
                float zCoord = (float)z / depth * scale + offsetY;

                // Mathf.Clamp01 force la valeur à rester entre 0 et 1 (règle le bug de couleur unique)
                float noiseValue = Mathf.Clamp01(Mathf.PerlinNoise(xCoord, zCoord));


                if (x == 0 && z == 0) Debug.Log($"Noise: {noiseValue} | Hauteur courbe: {heightCurve.Evaluate(noiseValue)}");

                // 2. Application de la COURBE (c'est ça qui crée les plateaux)
                // On demande à la courbe : "Si le bruit est à 0.4, quelle hauteur je dois mettre ?"
                float adjustedHeight = heightCurve.Evaluate(noiseValue);

                // 3. Position du sommet
                vertices[i] = new Vector3(x, adjustedHeight * heightMult, z);

                // 4. Couleur
                // On choisit la couleur en fonction de la hauteur ajustée par la courbe
                // Ainsi, tout ce qui est plat (océan) aura exactement la même couleur
                colors[i] = terrainGradient.Evaluate(adjustedHeight);

                i++;
            }
        }

        // 2. Création des Triangles (pour relier les points)
        triangles = new int[width * depth * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + width + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + width + 1;
                triangles[tris + 5] = vert + width + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors; // Assigner les couleurs aux sommets
        mesh.RecalculateNormals(); // Pour que la lumière réagisse bien
    }
}
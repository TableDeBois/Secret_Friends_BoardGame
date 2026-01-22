using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

    public Utils() { }

    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPos = default(Vector3), int fontSize = 40, 
        Color color=default(Color), TextAnchor textAnchor=TextAnchor.MiddleCenter, TextAlignment alignment=TextAlignment.Center, int sortingOrder=0)
    {
        if(color == null) color = Color.white;
        return CreateWorldText(parent,text, localPos, fontSize, color, textAnchor, alignment, sortingOrder);
    }

    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPos, int fonctSize, Color color, TextAnchor textAnchor, TextAlignment alignement, int sortingOrder)
    {
        GameObject go= new GameObject("World_Text", typeof(TextMesh));
        Transform transform = go.transform;
        transform.SetParent(parent,false);
        transform.localPosition = localPos;
        TextMesh textMesh = go.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = alignement;
        textMesh.text = text;
        textMesh.fontSize = fonctSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        return textMesh;
    }

    public static Vector3 getMouseWordPos()
    {
        /*Vector3 vec = getMouseWorldPosWithZ(Input.mousePosition, Camera.main);
        vec.y = 0f;*/

        Vector3 vec = getMouseWorldPosWithAtY0(0f, Camera.main);
        Debug.Log(vec);
        return vec;
    }


    private static Vector3 getMouseWorldPosWithAtY0(float y,Camera worldCam)
    {
        Ray ray = worldCam.ScreenPointToRay(Input.mousePosition);

        //Calcul de la position pour y = 0

        Plane ground = new Plane(Vector3.up, new Vector3(0, y, 0));

        if(ground.Raycast(ray,out float distance))
        {
            return ray.GetPoint(distance); // Point d'impact du rayon
        }

        return Vector3.zero; //Si pas d'impact
    }

    public static Vector3 getMouseWorldPosWithZ()
    {
        return getMouseWorldPosWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 getMouseWorldPosWithZ(Camera worldCamera)
    {
        return getMouseWorldPosWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 getMouseWorldPosWithZ(Vector3 screenPos,Camera worldCamera)
    {
        return worldCamera.ScreenToWorldPoint(screenPos);
    }
}

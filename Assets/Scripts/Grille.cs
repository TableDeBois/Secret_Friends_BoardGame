using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grille
{
    private int width;
    private int heighth;

    private float tileSize;
    private int[,] grilleArray;
    private TextMesh[,] debugArray;



    public Grille(int width, int heighth,float tileSize)
    {
        this.width = width;
        this.heighth = heighth;
        this.tileSize = tileSize;


        grilleArray=new int[width,heighth];
        debugArray = new TextMesh[width, heighth];

        Debug.Log(width + " " + heighth);



        for(int i = 0; i < grilleArray.GetLength(0); i++)
        {
            for(int j = 0; j < grilleArray.GetLength(1); j++)
            {
                Debug.Log(i + " , " + j);
                debugArray[i,j] = Utils.CreateWorldText(grilleArray[i,j].ToString(),null, getWorldPosistion(i,j)+new Vector3(tileSize,0,tileSize)* .5f,20,Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(getWorldPosistion(i, j), getWorldPosistion(i, j + 1), Color.white, 100f);
                Debug.DrawLine(getWorldPosistion(i, j), getWorldPosistion(i+1, j), Color.white, 100f);
            }
        }
        Debug.DrawLine(getWorldPosistion(0, heighth), getWorldPosistion(width,heighth), Color.white, 100f);
        Debug.DrawLine(getWorldPosistion(width, 0), getWorldPosistion(width, heighth), Color.white, 100f);


        setValue(2, 1, 56);
    }


    private Vector3 getWorldPosistion(int x, int y)
    {
        return new Vector3(x,0,y)*tileSize;
    }

    public void setValue(int x,int y, int value)
    {

        Debug.Log("setValue : " + x + " , " + y);
        if (x >= 0 && y >= 0 && x<width && y< heighth)
        {
            grilleArray[x, y] = value;
            debugArray[x,y].text= grilleArray[x,y].ToString();
        }
    }

    private void getXZ(Vector3 worldPos,out int x, out int z)
    {
        x = Mathf.FloorToInt(worldPos.x /tileSize);
        z = Mathf.FloorToInt(worldPos.z / tileSize);

        Debug.Log("getXY : " + x + " , " +z);
    }

    public void setValue(Vector3 worldPos,int value)
    {
        int x, z;
        getXZ(worldPos,out x,out z);
        setValue(x,z,value);

    }

    public void modifyValue(int x, int y)
    {
        Debug.Log("setValue : " + x + " , " + y);
        if (x >= 0 && y >= 0 && x < width && y < heighth)
        {
            grilleArray[x, y] += 1;
            debugArray[x, y].text = grilleArray[x, y].ToString();
        }
    }

    public void modifyValue(Vector3 worldPos)
    {
        int x, z;
        getXZ(worldPos, out x, out z);
        modifyValue(x, z);
    }

    public Grille()
    {
        this.width = 50;
        this.heighth = 50;
    }
}

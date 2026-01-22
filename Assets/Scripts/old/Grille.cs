using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Grille<TGridObj>
{

    public const float MAP_MAX_VALUE = 1;
    public const float MAP_MIN_VALUE = 0;
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }


    private int width;
    private int heigth;
    private Vector3 origin;
    private float tileSize;
    private TGridObj[,] grilleArray;
    private TextMesh[,] debugArray;



    public Grille(int width, int heighth,float tileSize,Vector3 origin)
    {
        this.width = width;
        this.heigth = heighth;
        this.tileSize = tileSize;
        this.origin = origin;

        grilleArray=new TGridObj[width,heighth];
        debugArray = new TextMesh[width, heighth];

        Debug.Log(width + " " + heighth);

        bool debug = true;
        if (debug)
        {
            for (int i = 0; i < grilleArray.GetLength(0); i++)
            {
                for (int j = 0; j < grilleArray.GetLength(1); j++)
                {
                    Debug.Log(i + " , " + j);
                    debugArray[i, j] = Utils.CreateWorldText(grilleArray[i, j].ToString(), null, getWorldPosistion(i, j) + new Vector3(tileSize, 0, tileSize) * .5f, 20, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(getWorldPosistion(i, j), getWorldPosistion(i, j + 1), Color.white, 100f);
                    Debug.DrawLine(getWorldPosistion(i, j), getWorldPosistion(i + 1, j), Color.white, 100f);
                }
            }
            Debug.DrawLine(getWorldPosistion(0, heighth), getWorldPosistion(width, heighth), Color.white, 100f);
            Debug.DrawLine(getWorldPosistion(width, 0), getWorldPosistion(width, heighth), Color.white, 100f);

            //Add on event parameters
            OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
            {
                debugArray[eventArgs.x, eventArgs.y].text = grilleArray[eventArgs.x, eventArgs.y].ToString();
            };

            //setValue(2, 1, 56);
        }


    }

    public int getWidth() { return this.width; }
    public int getHeigth() { return this.heigth; }

    public float getTileSize() { return this.tileSize; }

    public Vector3 getOrigin() { return this.origin; }

    private Vector3 getWorldPosistion(int x, int y)
    {
        return new Vector3(x,0,y)*tileSize+origin;
    }

    public void setValue(int x,int y, TGridObj value)
    {

        Debug.Log("setValue : " + x + " , " + y);
        if (x >= 0 && y >= 0 && x<width && y< heigth)
        {
            grilleArray[x, y] = value;
            debugArray[x,y].text= grilleArray[x,y].ToString();
            if(OnGridValueChanged != null) OnGridValueChanged(this,new OnGridValueChangedEventArgs { x=x, y=y });
        }
    }

    private void getXZ(Vector3 worldPos,out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPos - origin).x /tileSize);
        z = Mathf.FloorToInt((worldPos - origin).z / tileSize);

        Debug.Log("getXY : " + x + " , " +z);
    }

    public void setValue(Vector3 worldPos, TGridObj value)
    {
        int x, z;
        getXZ(worldPos,out x,out z);
        setValue(x,z,value);

    }

    public void modifyValue(int x, int y)
    {
        Debug.Log("setValue : " + x + " , " + y);
        if (x >= 0 && y >= 0 && x < width && y < heigth && grilleArray[x,y].GetType()==typeof(int))
        {
            //grilleArray[x, y] += (TGridObj) 1;
            debugArray[x, y].text = grilleArray[x, y].ToString();
        }
    }

    public void modifyValue(Vector3 worldPos)
    {
        int x, z;
        getXZ(worldPos, out x, out z);
        modifyValue(x, z);
    }

    public TGridObj getValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < heigth)
        {
            return grilleArray[x, y];
          
        } else
        {
            return default(TGridObj);
        }
    }

    public TGridObj getValue(Vector3 worldPos)
    {
        int x, z;
        getXZ(worldPos, out x, out z);
        return getValue(x, z);
    }

    public Grille()
    {
        this.width = 50;
        this.heigth = 50;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestingGrid : MonoBehaviour
{
    private Grille grid;

    // Start is called before the first frame update
    private void Start()
    {
        grid = new Grille(4, 3, 10f, new Vector3(20,0));

    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.modifyValue(Utils.getMouseWordPos());
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.getValue(Utils.getMouseWordPos()));
        }
    }
}

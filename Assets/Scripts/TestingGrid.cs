using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestingGrid : MonoBehaviour
{
    [SerializeField] private MapVisuals mapVisual;
    private Grille<bool> grid;

    // Start is called before the first frame update
    private void Start()
    {
        grid = new Grille<bool>(4, 3, 10f, new Vector3(0,0));

        //mapVisual.SetGrille(grid);

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

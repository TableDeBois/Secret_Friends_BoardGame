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
        grid = new Grille(20,20, 10f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.setValue(Utils.getMouseWordPos(), 56);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitGrid : MonoBehaviour
{
    //create a 2-d array of cell objects, each index of the array contains an instance of a Cell obj
    //set number of cells to display 
    private Cell[,] GridArr;


    //updates the grid based on if any two adjacent cells have been updated

    public void updateGrid()
    {
        throw new System.NotImplementedException();
    }

    public void drawGrid()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Cell cell = transform.GetChild(i).gameObject.GetComponent<Cell>();
            int x = cell.x;
            int y = cell.y;
            GridArr[x, y] = cell;
        }        

    }


    void Start()
    {
        int rows = 4;
        int columns = 6;
        GridArr = new Cell[rows, columns];
        drawGrid();
    }


}

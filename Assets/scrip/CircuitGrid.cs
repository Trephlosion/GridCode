using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitGrid : MonoBehaviour
{
    //create a 2-d array of cell objects, each index of the array contains an instance of a Cell obj
    //set number of cells to display 
    private Cell[,] GridArr;

    

// TODO: if and only if cell full occupying then check for which is which
//fix re call errors
    public void AddConnection(ElectricComponent component)
    {
        GridArr[component.head.x, component.head.y]= component.head;
        GridArr[component.tail.x, component.tail.y]= component.tail;
    }
    
    public void RemoveConnection(ElectricComponent component)
    {
        if (GridArr[component.head.x, component.head.y] == component.head)
        {
            GridArr[component.head.x, component.head.y] = null;
        }
        if (GridArr[component.tail.x, component.tail.y] == component.tail)
        {
            GridArr[component.tail.x, component.tail.y] = null;
        }
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
        int rows = 5;
        int columns = 5;
        GridArr = new Cell[rows, columns];
        drawGrid();
    }

    private void FixedUpdate()
    {
        foreach (ElectricComponent component in FindObjectsOfType<ElectricComponent>())
        {
            AddConnection(component);
        }
    }
}

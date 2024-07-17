using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CircuitGrid : MonoBehaviour
{
    //create a 2-d array of cell objects, each index of the array contains an instance of a Cell obj
    //set number of cells to display 
    private Cell[,] GridArr;
    void Start()
    {
        //make the rows and columns of the array
        int rows = 5;
        int columns = 5;
        GridArr = new Cell[rows, columns];
        //create the initial grid (might not need)
        drawGrid();
        //listen for event triggers in cells
        foreach (Cell cell in GridArr)
        {
            cell.onTriggerEnterEvent.AddListener(UpdateGrid);
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
    
    public void UpdateGrid(Cell cell)
    {
        // Redundant?
        // int x = cell.x;
        // int y = cell.y;
        // GridArr[x, y] = cell;
        
        //print a representation of the currently occupied grid cells using 
        
        PrintGrid();
    }
    
    public void PrintGrid()
    {
        string gridRepresentation = "";
        for (int i = 0; i < GridArr.GetLength(0); i++)
        {
            for (int j = 0; j < GridArr.GetLength(1); j++)
            {
                if (GridArr[i, j] != null)
                {
                    gridRepresentation += GridArr[i, j].isOverlapped ? "0 " : "X ";
                }
                // else
                // {
                //     gridRepresentation += "O ";
                // }
            }
            gridRepresentation += "\n";
        }
        Debug.Log(gridRepresentation);
    }


// TODO: if and only if cell full occupying then check for which is which

    }



using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridSingleton : MonoBehaviour
{

    enum ElectronicComponents: int 
    {
        None = -1,
        Wire = 0,
        LED = 1
    }

    public static GridSingleton Instance;
    private static Cell[,] grid = new Cell[4,4];

//public 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializingGrid();
            
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
            
    }

    public static void StatusGrid()
    {
        for (int i =0; i<4;i++)
        {
                print(grid[i,0].atached.ToString()+","+grid[i,1].atached.ToString()+","+grid[i,2].atached.ToString()+","+grid[i,3].atached.ToString()+",");
        }

        for (int i =0; i<4;i++)
        {
            for (int j=0;j<4;j++)
            {
                if (grid[i,j].componentAttached == (int)ElectronicComponents.Wire)
                {
                    print("There is a wire attached in positions "+i.ToString()+","+j.ToString());
                }
            }
        }
    }

    private static (int x, int y) ReturnCoordinates(string gameObjectTag)
    {

        print(gameObjectTag);
        //print("the name tag is "+gameObjectTag.Substring(8,gameObjectTag.Length-8));

        int gameObjectNumber=int.Parse(gameObjectTag.Substring(8,gameObjectTag.Length-8));

        switch (gameObjectNumber)
        {
            case 0: return (0,0);
            case 1: return (0,1);
            case 2: return (0,2);
            case 3: return (0,3);
            case 4: return (1,0);
            case 5: return (1,1);
            case 6: return (1,2);
            case 7: return (1,3);
            case 8: return (2,0);
            case 9: return (2,1);
            case 10: return (2,2);
            case 11: return (2,3);
            case 12: return (3,0);
            case 13: return (3,1);
            case 14: return (3,2);
            case 15: return (3,3);
            default: return (0,0);
        }
    }

    public static void AddConnectionCell(GameObject gridCell, int electronicComponent)
    {

        int x = 0;
        int y = 0;
        var coordinates = (x,y);
        coordinates = ReturnCoordinates(gridCell.tag);

        Cell newCell = new Cell();
        newCell.atached = 1;
        newCell.componentAttached = electronicComponent;
        grid[coordinates.x, coordinates.y]=newCell;
    }

    public static void InitializingGrid()
    {
        for (int rows=0;rows<4;rows++)
        {
            for (int cols=0;cols<4;cols++)
            {
                Cell cellNew = new Cell();
                cellNew.atached = 0;
                cellNew.componentAttached = (int)ElectronicComponents.None;
                grid[rows, cols]= cellNew;
            }
        }
    }
    
}

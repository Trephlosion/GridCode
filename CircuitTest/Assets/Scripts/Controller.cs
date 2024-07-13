using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private const int V = 4;
    private GameObject[] grid = new GameObject[16];
    //Now that I've gotten this working with an array, I'll move on to Dictionaries
    private Dictionary<GameObject, bool> gridDict = new Dictionary<GameObject, bool>();
    

    // Start is called before the first frame update
    void Start()
    {
        GameObject gridObj = GameObject.FindGameObjectWithTag("grid");
        if (gridObj.name.Equals("Grid")) 
        {
            Debug.Log(gridObj.name);
        }
        int i = 0;
        foreach (Transform child in transform) 
        {
            grid[i] = child.gameObject;
            gridDict.Add(child.gameObject, false);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (var gridKey in gridDict.Keys.ToList())
        {
            if (gridKey.transform.GetComponent<Renderer>().material.color == Color.green)
            {
                gridDict[gridKey] = gridKey.GetComponent<GridCell>().inUse;
            }
            else if (gridKey.transform.GetComponent<Renderer>().material.color == Color.black)
            {
                gridDict[gridKey] = false;
            }
            Debug.Log($"Key: {gridKey.ToString()}. $Value: {gridDict[gridKey].ToString()}");
        }

    }

    void ToPrint() 
    { 
            
    }
}

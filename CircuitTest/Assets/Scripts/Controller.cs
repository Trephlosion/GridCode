using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private const int V = 4;
    private GameObject[] grid = new GameObject[4];
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject gridObj = GameObject.Find("Grid");
        foreach (Transform child in transform) 
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToPrint() 
    { 
            
    }
}

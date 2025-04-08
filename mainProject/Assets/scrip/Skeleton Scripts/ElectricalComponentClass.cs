using System;
using System.Collections;
using System.Collections.Generic;
using SpiceSharp;
using UnityEngine;
[System.Serializable]
public class ElectricalComponentClass:MonoBehaviour {
    // Whether the Electrical Component is a wire, a resistor, a switch, etc.
    public string type;

    // The two attach points of each object
    public GameObject attachPointPositive;
    public GameObject attachPointNegative;

    private float resistance;
    private bool inCircuit;
    // Start is called before the first frame update
    void Start()
    {
        type = this.gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This function is to be run once when an XRGrabInteractor (representing one of the attachPoints) is connected to the XRSocketInteractor (representing a tile in the grid/a hole in the breadboard.))
    /// This function will determine if the object was successfully added to the grid (by connecting all attachPoints to the Grid). Once it is added to the grid, the circuit will run functionality to validate the project. 
    /// </summary>
    /// <returns>
    /// True if all attachPoints have been successfully added to the grid, False if one or more attachPoints are missing.
    /// </returns>
    public bool AddToGrid()
    {
        return inCircuit;
    }

    // Getters and Setters
    public GameObject getAttachPointPositive()
    {
        return attachPointPositive;
    }
    public GameObject getAttachPointNegative()
    {
        return attachPointNegative;
    }
}

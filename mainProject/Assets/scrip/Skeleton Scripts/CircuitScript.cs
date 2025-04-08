using System.Collections;
using System.Collections.Generic;
using SpiceSharp;
using UnityEngine;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using SpiceSharp.Entities;
using System;
using SpiceSharp.Validation;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Object = UnityEngine.Object;

public class CircuitScript : ElectricalComponentClass
{
    public Circuit circuit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentCircuitStatus();
    }
    /*
     * This function helps the user spawn a grid with a set number of rows and columns. This grid is spawned only once, with socket interactors on top of the function. 
     * The grid will only spawn once on start, and not on update.
     * 
     * INPUT:
     * int rows: the number of rows to instantiate at the beginning of the level or on start. 
     * int columns: the number of columns to instantiate at the beginning of the level or on start. 
     * 
     * OUTPUT
     * If the grid is successfully spawned at the beginning of the program, return true. Otherwise, return false.
     */

    public bool GridSpawn(int rows, int columns)
    {
        // If successfully spawned, return true. Otherwise, return false.
        return false;
    }

    /*
     * This function is run on every frame (aka in the Update() function) and is used to determine the current status of the object in the circuit. The circuit will return a float value indicating the 
     * amount of current flowing through the circuit at any point in time, and will reference two other methods to add new components to the circuit or remove components from the circuit. 
     * 
     * INPUT: None
     * 
     * OUTPUT: 
     * A float value representing the amount of current flowing through the circuit at any given point in time.
     */
    public float CurrentCircuitStatus()
    {
        // Returns the amount of current flowing through the circuit at any given point in time. 
        return 0.0f;
    }

    /*
     * This function returns a string value which lists the types of components present in the 
     * 
     * INPUT: None
     * 
     * OUTPUT: 
     * A string value representing a list of components in the circuit at any given point in time.
     */
    public string CircuitComponents()
    {
        return "";
    }
}

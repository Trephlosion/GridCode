using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;

using UnityEngine.Events;

[System.Serializable]
public class CellEvent : UnityEvent<Cell> { }

public class Cell : MonoBehaviour
{
    // Contains all inherit parts of each cell on the grid

    public CellEvent onTriggerEnterEvent;
    public int x, y;
    public bool isPowered, isGrounded, isInactive;
    private int occLevel = 0;
    public bool isOverlapped = false;
    private Color curColor = Color.black;

    public GameObject headptr=null, tailptr = null;
    public GameObject[] nodeOcc = new GameObject[2];
    //TODO: gameOBJ pointer that points to the head and tail occupants

    public float current, resistance, instantaneousVoltage;
    private Renderer _renderer;
    private CircuitGrid circuitGrid;
    

    //TODO: reference to object properties
    // if occupied, then take component properties

    // TODO: make reference to grid object (tail)

    // TODO: make reference to grid object (head)

    // float partResistance, voltageRequired;
    
    
    private void Start()
    {
        _renderer = this.transform.GetComponent<Renderer>();
        switch (x)
        {
            case 0:
                isPowered = true;
                break;
            case 1:
                isGrounded = true;
                break;
            default:
                isGrounded = false;
                isPowered = false;
                isInactive = true;
                break;
        }

        circuitGrid = FindObjectOfType<CircuitGrid>();
        curColor = _renderer.material.color;
        PrintCellDetails();
        
    }
    

//TODO: check for multiple grid entries
    private void OnTriggerEnter(Collider other)
    {   
         
        // Set Material of Cell
        if (other.gameObject.CompareTag("Head") && headptr == null && nodeOcc.GetLength(0) < 2)
        {
            Debug.Log("Head attached at Cell: " + gameObject.name + " with coordinates x: " + x + ", y: " + y);
            _renderer.material.color = occLevel >= 2 ? Color.green : curColor;
            occLevel++;
            headptr = other.gameObject.transform.parent.gameObject;
            nodeOcc.SetValue(headptr, nodeOcc.GetLength(0)-1);
        }

        if (other.gameObject.CompareTag("Tail") && tailptr == null && nodeOcc.GetLength(0) < 2)
        {
            Debug.Log("Tail attached at Cell: " + gameObject.name + " with coordinates x: " + x + ", y: " + y);
            _renderer.material.color = occLevel >= 2 ? Color.green : curColor;
            occLevel++;
            tailptr = other.gameObject.transform.parent.gameObject;
            nodeOcc.SetValue(tailptr, nodeOcc.GetLength(0)-1);
        }
        
        isOverlapped = true;
        onTriggerEnterEvent?.Invoke(this);
         
    }



    private void OnTriggerExit(Collider other)
    {
       
        
        if (other.gameObject == headptr)
        {
            occLevel--;
            headptr = null; // Delete the reference
            Debug.Log("Head detached from Cell: " + gameObject.name);
        }
        else if (other.gameObject == tailptr)
        {
            occLevel--;
            tailptr = null; // Delete the reference
            Debug.Log("Tail detached from Cell: " + gameObject.name);
        }
        
        isOverlapped = false;
        
        _renderer.material.color = occLevel >= 2 ? Color.green : curColor;
    }
    
    public void PrintCellDetails()
    {
        if (isOverlapped)
        {
            Debug.Log($"Cell Updated: {gameObject.name} with coordinates x: {x}, y: {y}");
            Debug.Log($"isPowered: {isPowered}, isGrounded: {isGrounded}, isInactive: {isInactive}");
            // Add any other properties you want to print
        }
    }
}

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

    public int x, y;
    public bool isPowered, isGrounded, isInactive;
    public bool Occ = false;
    private int occLevel = 0;
    public bool isUpdated = false;

    private GameObject headptr, tailptr;
    //TODO: gameOBJ pointer that points to the head and tail occupants

    public float current, resistance, instantaneousVoltage;
    private Renderer _renderer;
    private CircuitGrid circuitGrid;
    private ElectricComponent Ec;
    private Material personalMats;

    //TODO: reference to object properties
    // if occupied, then take component properties

    // TODO: make reference to grid object (tail)

    // TODO: make reference to grid object (head)

    // float partResistance, voltageRequired;

    public CellEvent onTriggerEnterEvent;
    
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
        
    }


    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (occLevel >= 2)
        {
            Occ = true;
            occLevel = 2;
        }
        
        
    }

//TODO: check for multiple grid entries
private void OnTriggerEnter(Collider other)
{   
     
    if (other.gameObject.CompareTag("Head") && headptr == null)
    {
        headptr = other.gameObject;
        Debug.Log("Head attached at Cell: " + gameObject.name + " with coordinates x: " + x + ", y: " + y);
        _renderer.material.color = occLevel >= 2 && Occ ? Color.green : Color.white;
        
    }

    if (other.gameObject.CompareTag("Tail") && tailptr == null)
    {
        tailptr = other.gameObject;
        Debug.Log("Tail attached at Cell: " + gameObject.name + " with coordinates x: " + x + ", y: " + y);
        _renderer.material.color = occLevel >= 2 && Occ ? Color.green : Color.white;
    }
    
    onTriggerEnterEvent?.Invoke(this);
    isUpdated = true;
    onTriggerEnterEvent?.Invoke(this);
    
     // Ec = other.gameObject.GetComponent<ElectricComponent>(); 
     // personalMats = other.gameObject.GetComponent<Material>();
    
}

private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Head"))
        {
            occLevel++;
            headptr = other.gameObject.transform.parent.gameObject;
            // other.gameObject.GetComponent<ElectricComponent>().setPos(x, y);

        }

        if (other.gameObject.CompareTag("Tail"))
        {
            occLevel++;
            tailptr = other.gameObject.transform.parent.gameObject;
            //other.gameObject.GetComponent<ElectricComponent>().setPos(x, y);
           

        }

        if (other.gameObject.CompareTag("Head") || other.gameObject.CompareTag("Tail"))
        {
            ElectricComponent component = other.gameObject.GetComponent<ElectricComponent>();
          //  circuitGrid.AddConnection(component);
        }

        _renderer.material.color = occLevel >= 2 && Occ ? Color.green : Color.white;

    }

    private void OnTriggerExit(Collider other)
    {
        occLevel--;
        if (other.gameObject.CompareTag("Head") || other.gameObject.CompareTag("Tail"))
        {
            ElectricComponent component = other.gameObject.GetComponent<ElectricComponent>();
            //circuitGrid.RemoveConnection(component);
        }
        
        if (other.gameObject == headptr)
        {
            headptr = null; // Delete the reference
            Debug.Log("Head detached from Cell: " + gameObject.name);
        }
        else if (other.gameObject == tailptr)
        {
            tailptr = null; // Delete the reference
            Debug.Log("Tail detached from Cell: " + gameObject.name);
        }

        Ec = null;
        personalMats = null;
        isUpdated = false;
        _renderer.material.color = occLevel >= 2 && Occ ? Color.green : Color.white;
    }

}

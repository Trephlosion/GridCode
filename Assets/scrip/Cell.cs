using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;

public class Cell : MonoBehaviour
{
    // Contains all inherit parts of each cell on the grid

    public int x, y;
    public bool isPowered, isGrounded, isInactive;
    public bool Occ = false;
    private int occLevel = 0;

    public float current, resistance, instantaneauosVoltage;
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
    }








    private void Update()
    {
        _renderer.material.color = occLevel >= 2 && Occ ? Color.green : Color.white;
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
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Head"))
        {
            occLevel++;
            other.gameObject.GetComponent<ElectricComponent>().setPos(x, y);
            Debug.Log("head attatched");
        }

        if (other.gameObject.CompareTag("Tail"))
        {
            occLevel++;
            other.gameObject.GetComponent<ElectricComponent>().setPos(x, y);
            Debug.Log("tail attatched");

        }

        if (other.gameObject.CompareTag("Head") || other.gameObject.CompareTag("Tail"))
        {
            ElectricComponent component = other.gameObject.GetComponent<ElectricComponent>();
            circuitGrid.AddConnection(component);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        occLevel--;
        if (other.gameObject.CompareTag("Head") || other.gameObject.CompareTag("Tail"))
        {
            ElectricComponent component = other.gameObject.GetComponent<ElectricComponent>();
            circuitGrid.RemoveConnection(component);
        }
    }

}

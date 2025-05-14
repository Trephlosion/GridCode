using System.Collections;
using SpiceSharp;
using System.Collections.Generic;
using SpiceSharp.Components;
using UnityEngine;

public class WireScript : ElectricalComponentClass
{
    public float currentFlow;
    
    //[SerializeField] private Material _overheatMaterial;
    //[SerializeField] private Material _curMaterial;

    private SpiceSharp.Components.Resistor _wireSource;
    private CircuitScript _circuit;
    private bool _added = false;


    // Start is called before the first frame update
    void Start()
    {
        _wireSource = CreateWire();
        _circuit = FindObjectOfType<CircuitScript>();
    }


    // Update is called once per frame
    void Update()
    {
        if (this.AddToGrid() && !_added)
        {
            _circuit.circuit.Add(_wireSource);
            _added = false;
            return;
        }
        else if (!this.AddToGrid() && _added) { 
            _circuit.circuit.Remove(_wireSource);
            _added = false;
            return;
        }
        if (currentFlow >= 2.0f) 
        {
            Overheat();
        }
        else
        {
            Normal();
        }
    }

    public SpiceSharp.Components.Resistor GetVoltageSource()
    {
        return _wireSource;
    }

    public SpiceSharp.Components.Resistor CreateWire()
    {
        return new SpiceSharp.Components.Resistor(this.name, this.attachPointPositive.name, this.attachPointNegative.name, this.GetResistance());
    }

    public void Overheat()
    {
        this.gameObject.GetComponent<LineRenderer>().material.color = Color.red;
    }

    public void Normal()
    {
        this.gameObject.GetComponent<LineRenderer>().material.color = Color.black;
    }
}

using System.Collections;
using System.Collections.Generic;
using SpiceSharp;
using SpiceSharp.Components;
using Unity.VisualScripting;
using UnityEngine;

public class BatteryScript : ElectricalComponentClass
{
    public float currentFlow;

    //[SerializeField] private Material _overheatMaterial;
    //[SerializeField] private Material _curMaterial;

    private VoltageSource _voltageSource;
    private CircuitScript _circuit;
    private bool _added = false;

    // Start is called before the first frame update
    void Start()
    {
        _voltageSource = CreateVoltageSource();
        _circuit = FindObjectOfType<CircuitScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.AddToGrid() && !_added)
        {
            _circuit.circuit.Add(_voltageSource);
            _added = true;
            return;
        }
        else if (!this.AddToGrid() && _added)
        {
            _circuit.circuit.Remove(_voltageSource);
            _added = false;
            return;
        }
    }

    public VoltageSource GetVoltageSource()
    {
        return _voltageSource;
    }

    public VoltageSource CreateVoltageSource()
    {
        return new VoltageSource(this.name, this.attachPointPositive.name, this.attachPointNegative.name, this.GetResistance());
    }

    
}

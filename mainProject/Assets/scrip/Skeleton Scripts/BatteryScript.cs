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

    // Start is called before the first frame update
    void Start()
    {
        _voltageSource = CreateVoltageSource();
        _circuit = FindObjectOfType<CircuitScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.AddToGrid())
        {
            _circuit.circuit.Add(_voltageSource);
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

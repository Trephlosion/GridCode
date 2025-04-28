using System.Collections;
using System.Collections.Generic;
using SpiceSharp.Components;
using UnityEngine;

public class WireScript : ElectricalComponentClass
{
    public float currentFlow;
    
    //[SerializeField] private Material _overheatMaterial;
    //[SerializeField] private Material _curMaterial;

    private VoltageSource _voltageSource;

    // Start is called before the first frame update
    void Start()
    {
        _voltageSource = CreateVoltageSource();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFlow >= 2.0f) 
        {
            Overheat();
        }
        else
        {
            Normal();
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

    public void Overheat()
    {
        this.gameObject.GetComponent<LineRenderer>().material.color = Color.red;
    }

    public void Normal()
    {
        this.gameObject.GetComponent<LineRenderer>().material.color = Color.black;
    }
}

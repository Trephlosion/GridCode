using System.Collections;
using SpiceSharp;
using SpiceSharp.Simulations;
using SpiceSharp.Components;
using System.Collections.Generic;
using UnityEngine;

namespace SpiceSharp
{
    public class ResistorScript : ElectricalComponentClass
    {
        private bool _added = false;
        private Components.Resistor _resistanceSource;
        // Start is called before the first frame update
        void Start()
        {
            _resistanceSource = CreateResistorSource();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public Components.Resistor GetResistorSource()
        {
            return _resistanceSource;
        }

        public Components.Resistor CreateResistorSource()
        {
            return new Components.Resistor(this.name, this.attachPointPositive.name, this.attachPointNegative.name, this.GetResistance());
        }
    }

}

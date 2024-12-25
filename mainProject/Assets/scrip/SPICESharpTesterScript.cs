using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiceSimulation
{
    public class SPICESharpTesterScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            SPICETest();
        }

        public void SPICETest()
        {
            // Build the circuit
            var ckt = new Circuit(
                new VoltageSource("V1", "in", "0", 1.0),
                new SpiceSharp.Components.Resistor("R1", "in", "out", 1.0e4),
                new SpiceSharp.Components.Resistor("R2", "out", "0", 2.0e4)
                );
            var dc = new DC("dc", "V1", 0.0, 5.0, 0.001);

            // Run the simulation
            foreach (int exportType in dc.Run(ckt))
            {
                //Debug.Log("SPICE# says:" + dc.GetVoltage("out"));
                //Console.WriteLine(dc.GetVoltage("out"));
            }
            
        }
    }
}

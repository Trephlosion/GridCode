using SpiceSharp.Components;
using SpiceSharp.Simulations;
using SpiceSharp.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using SpiceSharp.Validation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Object = UnityEngine.Object;

namespace SpiceSharp
{
    public class Grid1 : MonoBehaviour
    {
        public GameObject groundTile;
        public GameObject powerTile;
        public GameObject normalTile;
        public GameObject batteryObject;
        public GameObject wire;

        private Circuit currentCircuit;
        
        
        //TODO: ADD NUMBERED VALUE TO PASS IN FOR EACH OF THE GRID 
        public int HEIGHT = 5;
        public int WIDTH = 5;
            
        //TODO: ADD A TRANSFORM TO WHERE THE GRID WILL BE LOCATED
        Vector3 gridOrigin = new Vector3(0, 0, 0);

        public static bool GetMouseButtonDown(int button)
        {
            throw new NotImplementedException();
        }




        // Start is called before the first frame update
        void Start()
        {
            // Create the breadboard
            // Spawn an array of objects that will be the wires and nodes
            GridSpawn(HEIGHT, WIDTH, gridOrigin);

        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                OnMouseDown();
            }

        }

        public void GridSpawn(int h, int w, Vector3 gridOrigin)
        {
            int HEIGHT = h;
            int WIDTH = w;
            
            
            
            
            // Create an empty GameObject to hold the grid
            GameObject gridParent = new GameObject("Grid");

            //Create a battery GameObject
            GameObject battery = Instantiate(batteryObject);
            battery.transform.position = new Vector3(1.0f, 1.0f, -1.5f);
            battery.transform.rotation = Quaternion.Euler(0, 90, 0);

            // Spawn an array of objects that will be the wires and nodes
            // 5 x 5 base breadboard
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    // Spawn the nodes
                    GameObject node = new GameObject();
                    //GameObject node = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    if (i == 4)
                    {
                        node = Instantiate(groundTile);
                    }
                    else if (i == 0)
                    {
                        node = Instantiate(powerTile);
                    }
                    else {
                        node = Instantiate(normalTile);
                    }
                    
                    node.transform.position = new Vector3(i, 0, j);
                    node.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    node.name = "Node: " + i + ":" + j;
                    node.transform.parent = gridParent.transform;
                    node.AddComponent<NodeChecker>();

                    // Spawn column wires (might not be needed)
                    /*if (i < 4)
                    {
                        GameObject columnWire = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        columnWire.transform.position = new Vector3(i + 0.5f, 0, j);
                        columnWire.transform.rotation = Quaternion.Euler(90, 0, 90);
                        columnWire.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                        columnWire.name = "ColumnWire: " + i + ":" + j + " to " + (i + 1) + ":" + j;
                        columnWire.transform.parent = gridParent.transform;
                        Debug.Log("New Column Wire: " + columnWire);
                    }*/
                    

                    if (j < 4)
                    {
                        // Create the rowed wires
                        //GameObject rowWire = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        GameObject rowWire = Instantiate(wire);

                        rowWire.transform.position = new Vector3(i, 1f, j + 0.5f);
                        //rowWire.transform.position = new Vector3(i, 0, j + 0.5f);
                        //rowWire.transform.rotation = Quaternion.Euler(90, 0, 0);
                        rowWire.transform.rotation = Quaternion.Euler(0, 90, 0);
                        rowWire.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                        rowWire.name = "RowWire: " + i + ":" + j + " to " + i + ":" + (j + 1);
                        rowWire.transform.parent = gridParent.transform;
                        rowWire.AddComponent<SpiceWireComponent1>();

                        // Create the resistor component separately
                        rowWire.GetComponent<SpiceWireComponent1>().nombre =
                            "R: " + i + ":" + j + " to " + i + ":" + (j + 1);
                        rowWire.GetComponent<SpiceWireComponent1>().posNode = "Node: " + i + ":" + j;
                        rowWire.GetComponent<SpiceWireComponent1>().negNode = "Node: " + i + ":" + (j + 1);


                        var newWire = new Components.Resistor(rowWire.GetComponent<SpiceWireComponent1>().nombre,
                            rowWire.GetComponent<SpiceWireComponent1>().posNode,
                            rowWire.GetComponent<SpiceWireComponent1>().negNode,
                            rowWire.GetComponent<SpiceWireComponent1>().innateResistance);

                        if (currentCircuit == null)
                        {
                            currentCircuit = new Circuit();
                        }

                        currentCircuit.Add(newWire);
                        Debug.Log("New Row Wire: " + rowWire + "\nNew SpiceSharp Wire: " + newWire);

                        /*// Make the cylinder glow
                        Renderer renderer = rowWire.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            Material material = renderer.material;
                            material.EnableKeyword("_EMISSION");
                            material.SetColor("_EmissionColor", Color.green);
                        }*/
                    }
                }
            }
        }

        public void OnClickTest()
        {
            // Clone the existing circuit
            Circuit clonedCircuit = (Circuit)currentCircuit.Clone();

            // Build the circuit
            // Add a voltage source to the cloned circuit
            VoltageSource battery = new VoltageSource("V1", "Node: 0:0", "Node: 4:0", 5.0);
            clonedCircuit.Add(battery);

            // Add 2 resistors to the cloned circuit
            Components.Resistor resistor1 = new Components.Resistor("R1", "Node: 0:1", "Node: 2:1", 1.0e4);
            Components.Resistor resistor2 = new Components.Resistor("R2", "Node: 2:2", "Node: 4:1", 1.0e4);

            DC tester = new DC("dc", "V1", 5.0, 5.0, 0);

            // Run the simulation
            foreach (int exportType in tester.Run(clonedCircuit))
            {
                // Choose a random node to test voltage
                int randomNodeIndex = UnityEngine.Random.Range(0, 5 * 5); // Assuming a 5x5 grid
                string randomNodeName = "Node: " + (randomNodeIndex / 5) + ":" + (randomNodeIndex % 5);


                //Debug.Log("SPICE# says: Current Voltage: " + tester.GetVoltage(randomNodeName) + " @ Node: " + (randomNodeIndex / 5) + ":" + (randomNodeIndex % 5));
            }
        }

        public void OnClickTest2()
        {
            // Test the circuit to check to see if it has a Voltage Source
            // if the circuit has a voltage source, then run a simulation
            // if the circuit does not have a voltage source, then throw an error and turn the wires red until a new simulation is run.

        }

        // public void OnClick()
        // {
        //     OnClickTest();
        // }


        void OnMouseDown()
        {
            // Clone the existing circuit
            Circuit clonedCircuit = (Circuit)currentCircuit.Clone();

            // Build the circuit
            // Add a voltage source to the cloned circuit
            VoltageSource battery = new VoltageSource("V1", "Node: 0:0", "Node: 4:0", 1.0);
            clonedCircuit.Add(battery);

            // Add 2 resistors to the cloned circuit
            Components.Resistor resistor1 = new Components.Resistor("R1", "Node: 0:1", "Node: 2:1", 1.0e4);
            Components.Resistor resistor2 = new Components.Resistor("R2", "Node: 2:2", "Node: 4:1", 1.0e4);
            clonedCircuit.Add(resistor1);
            clonedCircuit.Add(resistor2);

            DC tester = new DC("dc", "V1", 0.0, 5.0, 0.1);

            // Validate the circuit
            //var rules = new SpiceSharp.Validation.IRules();
            //var violations = tester.Validate(rules, clonedCircuit);

            //if (tester.BaseRules.ViolationCount > 0)

            IRules rulesViolated = clonedCircuit.Validate();
            
            if (rulesViolated.ViolationCount > 0)
            {
                foreach (var violation in rulesViolated.Violations)
                {
                    // Right now, the most common errors are FloatingNodeRuleViolations, with one VariablePresenceRuleViolation.
                    if(violation is SpiceSharp.Validation.FloatingNodeRuleViolation)
                    {
                        Debug.Log("SIN COMMITTED (FloatingNodeRuleViolation): " + ((FloatingNodeRuleViolation)violation).FloatingVariable.Name);
                    }
                    if (violation is SpiceSharp.Validation.VariablePresenceRuleViolation)
                    {
                        Debug.Log("SIN COMMITTED (VariablePresenceRuleViolation): " + ((VariablePresenceRuleViolation)violation).Variable.Name);
                    }

                }
                return; // Exit if there are validation errors
            }

            // Run the simulation
            foreach (int exportType in tester.Run(clonedCircuit))
            {
                // Choose a random node to test voltage
                int randomNodeIndex = UnityEngine.Random.Range(0, 5 * 5); // Assuming a 5x5 grid
                string randomNodeName = "Node: " + (randomNodeIndex / 5) + ":" + (randomNodeIndex % 5);

                Debug.Log("SPICE# says: Current Voltage: " + tester.GetVoltage(randomNodeName)
                                                       + " @ Node: " + (randomNodeIndex / 5)
                                                       + ":" + (randomNodeIndex % 5));


                //CHEC THE VALIDIDITY OF THE CIRCUIT
                //IRule.Violations.get() = tester.Validate(clonedCircuit);

            }
        }

        public void SPICETest()
        {
            // Build the circuit
            var ckt = new Circuit(
                new VoltageSource("V1", "in", "0", 1.0),
                new Components.Resistor("R1", "in", "out", 1.0e4),
                new Components.Resistor("R2", "out", "0", 2.0e4)
            );
            var dc = new DC("dc", "V1", 0.0, 5.0, 0.001);

            // Run the simulation
            foreach (int exportType in dc.Run(ckt))
            {
                Debug.Log("SPICE# says:" + dc.GetVoltage("out"));
                Console.WriteLine(dc.GetVoltage("out"));
            }
        }


    }

    //public class SpiceWireComponent1 : MonoBehaviour
    //{
    //    public string nombre;
    //    public string posNode;
    //    public string negNode;
    //    public double innateResistance = 1.0e-4;
    //}
}

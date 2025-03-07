using SpiceSharp.Components;
using SpiceSharp.Simulations;
using SpiceSharp.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using SpiceSharp.Simulations.Base;
using SpiceSharp.Validation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Object = UnityEngine.Object;

namespace SpiceSharp
{
    public class Grid_Snapper : MonoBehaviour
    {
        
        private Circuit currentCircuit;

        public static bool GetMouseButtonDown(int button)
        {
            throw new NotImplementedException();
        }


        // Start is called before the first frame update
        void Start()
        {
            // Create the breadboard
            // Spawn an array of objects that will be the wires and nodes
            GridSpawn();

        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                OnMouseDown();
            }

        }

        public GameObject spawnXRSocket(float x, float y, float z)
        {
            // The basic example demonstrates the required components to create a socket interaction:
            // an XR Socket Interactor and a Collider with Is Trigger enabled.
            // In addition, the Attach Transform is set, which indicates where an object will snap to.
            // This is not required but will give much more consistent results.
            // The Socket Interactor will automatically find the nearest socket and snap to it.
            // The Socket Interactor will also automatically detach when the object is moved too far away.
            // The Socket should only interact with one object at a time (no highlighting or multi-object interactions).
            // The Socket Interactor will automatically find the nearest socket and snap to it.
            
            // Create the rowed wires
            GameObject NewZone = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            NewZone.transform.position = new Vector3(x, y, z + 0.5f);
            NewZone.transform.rotation = Quaternion.Euler(90, 0, 0);
            NewZone.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            NewZone.name = "RowWire: " + x + ":" + z + " to " + x + ":" + (z + 1);
            // NewZone.GetComponent<MeshRenderer>().material.SetActive(false);
            NewZone.AddComponent<SpiceWireComponent>();

            // Create the resistor component separately
            NewZone.GetComponent<SpiceWireComponent>().nombre =
                "R: " + x + ":" + z + " to " + x + ":" + (z + 1);
            NewZone.GetComponent<SpiceWireComponent>().posNode = "Node: " + x + ":" + z;
            NewZone.GetComponent<SpiceWireComponent>().negNode = "Node: " + x + ":" + (z + 1);


            var newWire = new Components.Resistor(NewZone.GetComponent<SpiceWireComponent>().nombre,
                NewZone.GetComponent<SpiceWireComponent>().posNode,
                NewZone.GetComponent<SpiceWireComponent>().negNode,
                NewZone.GetComponent<SpiceWireComponent>().innateResistance);
            
            NewZone.GetComponent<SpiceWireComponent>().resistor = newWire;
            
            //TODO: CHANGE THE SHAPE OF THE COLLIDER TO BE OPTIMAL FOR THE CIRCUIT.
            
            NewZone.GetComponent<Collider>().isTrigger = true;
            // Add the XR Socket Interactor
            NewZone.AddComponent<XRSocketInteractor>();
            NewZone.GetComponent<XRSocketInteractor>().attachTransform = NewZone.transform;
            
            //RETURN THE OBJECT
            return NewZone;

        }

        public void GridSpawn()
        {
            // Create an empty GameObject to hold the grid
            GameObject gridParent = new GameObject("Grid");
            var gridsize = 5;
            // GENERATE OBJECTS IN THE SCENE AND IMPORT THE CLEAR VALUES FOR THE LEVELS

            // Spawn an array of objects that will be the wires and nodes
            // 5 x 5 base breadboard
            for (int i = 0; i < gridsize; i++)
            {
                for (int j = 0; j < gridsize; j++)
                {
                    // Spawn the nodes
                    GameObject node = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    node.transform.position = new Vector3(i, 0, j);
                    node.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    node.name = "Node: " + i + ":" + j;
                    node.transform.parent = gridParent.transform;
                    // node.AddComponent<NodeChecker>();
                    

                    if (j < gridsize - 1)
                    {
                        GameObject newSocket = spawnXRSocket(i,0,j); 
                        newSocket.transform.parent = gridParent.transform;

                        if (currentCircuit == null)
                        {
                            currentCircuit = new Circuit();
                        }
                     
                        currentCircuit.Add(newSocket.GetComponent<SpiceWireComponent>().resistor);
                        
                        Debug.Log("New Row Wire: " + newSocket + "\nNew SpiceSharp Wire: " + newSocket.GetComponent<SpiceWireComponent>().resistor);

                        
                    }

                    if (j == gridsize - 1)
                    {
                        var BIGPLASTIC  = new Components.Resistor("Insulator: " + i + ":" + j, "Node: " + i + ":" + j, "0", 1.0e9);
                        currentCircuit.Add(BIGPLASTIC);
                    }
                    
                }
            }
            // add a ground wire
            
        }


        void OnMouseDown()
        {
            // Clone the existing circuit
            Circuit clonedCircuit = (Circuit)currentCircuit.Clone();

            //Create A Ground
            Components.Resistor GroundWire = new Components.Resistor("Ground", "Node: 4:0", "0", 1.0e-5);
            clonedCircuit.Add(GroundWire);
            // Build the circuit
            // Add a voltage source to the cloned circuit
            
            VoltageSource battery = new VoltageSource("V1", "Node: 0:0", "0", 5.0);
            clonedCircuit.Add(battery);

            // Add 2 resistors to the cloned circuit
            Components.Resistor resistor1 = new Components.Resistor("NewWire1", "Node: 0:3", "Node: 2:3", 10);
            Components.Resistor resistor2 = new Components.Resistor("NewWire2", "Node: 2:1", "Node: 4:1", 10);
            clonedCircuit.Add(resistor1);
            clonedCircuit.Add(resistor2);
            
            

            DC tester = new DC("dc", "V1", 5.0, 5.0, 0.1);
            
            var currentExport = new RealPropertyExport( tester, "NewWire1", "i");


            // Validate the circuit

            IRules rulesViolated = clonedCircuit.Validate();
            
            if (rulesViolated.ViolationCount > 0)
            {
                foreach (var violation in rulesViolated.Violations)
                {
                    // Right now, the most common errors are FloatingNodeRuleViolations, with one VariablePresenceRuleViolation.
                    // *Lemongrab voice* I am going to print out the name of each Node that is committing the SINFUL CRIME of being a floating node, so we can point at them and laugh.
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
                
                Debug.Log("SPICE# says: Current through resistor1: " + currentExport.Value);
                // Debug.Log("SPICE# says: Current through resistor2: " + tester.GetCurrent(resistor2));
               // .Voltage = tester.GetVoltage("Node: 0:3");
                //TODO: STORE ERRTYHIN IN A ARRAY THAT HAS ALL OF THE VALUES OF EACH OBJECT IN THE CIRCUIT
                //CHEC THE VALIDIDITY OF THE CIRCUIT
                //IRule.Violations.get() = tester.Validate(clonedCircuit);

            }
        }
    

        public class SpiceWireComponent : MonoBehaviour
        {
            public string nombre;
            public string posNode;
            public string negNode;
            public double innateResistance = 1.0e-4;
            public Components.Resistor resistor;
        }
    }

  
}

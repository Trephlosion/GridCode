using SpiceSharp.Components;
using SpiceSharp.Simulations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Object = UnityEngine.Object;

namespace SpiceSharp
{

    public class NodeChecker : MonoBehaviour
    {
        //remember this objects position as well as the 4 adjacent objects positions as well as the object that collided with this object
        //private Vector3 thisObjectPosition;
        //private Vector3[] adjacentObjectPositions = new Vector3[4];
        private GameObject mostRecentCollidingObject;
        private Circuit currentCircuit;

        // Start is called before the first frame update
        void Start()
        {
            //thisObjectPosition = transform.position;
            //StoreAdjacentObjectPositions();

        }

        // Update is called once per frame
        void Update()
        {

        }
        
        //private void StoreAdjacentObjectPositions()
        //{
        //    adjacentObjectPositions[0] = transform.position + Vector3.forward;  // Front
        //    adjacentObjectPositions[1] = transform.position + Vector3.back;     // Back
        //    adjacentObjectPositions[2] = transform.position + Vector3.left;     // Left
        //    adjacentObjectPositions[3] = transform.position + Vector3.right;    // Right
        //}
        
        /*
         */
        public void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("wire"))
            {
                Debug.Log("collision occuring at" + "");
                if (this.gameObject.CompareTag("Ground"))
                {
                    ChangeColor(gameObject, Color.green);
                }
                else if (this.gameObject.CompareTag("Power"))
                {
                    ChangeColor(gameObject, Color.red);
                }
                else if (this.gameObject.CompareTag("Cell"))
                {
                    ChangeColor(gameObject, Color.blue);
                }
                ChangeColor(collision.gameObject, Color.yellow);

                //if (gameObject.CompareTag("Component"))
                //{
                //    // AddComponentToCircuit(collider.gameObject);
                //}
            }
            
        }


        public void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.CompareTag("wire"))
            {
                Debug.Log("collision occuring at" + "");
                if (this.gameObject.CompareTag("Ground"))
                {
                    ChangeColor(gameObject, Color.white);
                }
                else if (this.gameObject.CompareTag("Power"))
                {
                    ChangeColor(gameObject, Color.white);
                }
                else if (this.gameObject.CompareTag("Cell"))
                {
                    ChangeColor(gameObject, Color.white);
                }
                ChangeColor(collision.gameObject, Color.white);

                //if (gameObject.CompareTag("Component"))
                //{
                //    // AddComponentToCircuit(collider.gameObject);
                //}
            }

        }
        //private void OnCollisionExit(Collision collision)
        //{
        //    if (collision.gameObject == mostRecentCollidingObject)
        //    {
        //        ChangeColor(gameObject, Color.white);
        //        foreach (Vector3 adjacentPosition in adjacentObjectPositions)
        //        {
        //            Collider[] colliders = Physics.OverlapSphere(adjacentPosition, 0.1f);
        //            foreach (Collider collider in colliders)
        //            {
        //                if (collider.CompareTag("Node"))
        //                {
        //                    ChangeColor(collider.gameObject, Color.white);
        //                }
        //            }
        //        }
        //    }
        //}

        private void ChangeColor(GameObject obj, Color color)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }

        private void AddComponentToCircuit(GameObject adjacentNode)
        {
            string currentNodeName = gameObject.name;
            string adjacentNodeName = adjacentNode.name;

            if (mostRecentCollidingObject.CompareTag("VoltageSource"))
            {
                VoltageSource voltageSource = mostRecentCollidingObject.GetComponent<VoltageSource>();
                if (voltageSource != null)
                {
                    VoltageSource newVoltageSource = new VoltageSource(mostRecentCollidingObject.name, currentNodeName, adjacentNodeName, 5.0f);
                    currentCircuit.Add(newVoltageSource);
                }
            }
            else if (mostRecentCollidingObject.CompareTag("Resistor"))
            {
                Resistor resistor = mostRecentCollidingObject.GetComponent<Resistor>();
                if (resistor != null)
                {
                    Components.Resistor newResistor = new Components.Resistor(mostRecentCollidingObject.name, currentNodeName, adjacentNodeName, 5.0f);
                    currentCircuit.Add(newResistor);
                }
            }
        }
    




























        // I have an idea
        public void IHaveAnIdea()
        {
            // I have an idea
            Debug.Log("I have an idea");
        }

    }
}
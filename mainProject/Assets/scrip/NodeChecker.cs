using SpiceSharp.Components;
using SpiceSharp.Simulations;
using System;
using TMPro;
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
        private bool ground = false;
        private bool power = false;
        private bool tile = false;
        private List<GameObject> connections;

        //private TMP_Text text = null;
       
        private GameObject mostRecentCollidingObject;
        private Circuit currentCircuit;

        // Start is called before the first frame update
        void Start()
        {
            connections = new List<GameObject>();

            if (this.gameObject.CompareTag("Ground")) { ground = true; }
            if (this.gameObject.CompareTag("Power")) { power = true; }
            if (this.gameObject.CompareTag("Cell")) { tile = true; }

            //Debug.Log("Ground"+ ground + ", Power" + power + ", Cell" + tile);
            //text = this.gameObject.GetComponentInChildren<TMP_Text>();
            //if (text)
            //{
            //    text.text = ("Accessed Text!");
            //}else if (text == null)
            //{
            //text.text = ("Did not access Text!");
            //}
        }

        // Update is called once per frame
        void Update()
        {
            //text.text = "Num. of connections:" + connections.Count;
        }
        
        public void OnTriggerEnter(Collider collision)
        {
            if(connections.Count <= 4)
            {
                connections.Add(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("wire"))
            {
                //Debug.Log("collision occuring at" + "");
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
             
            }
            
        }


        public void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.CompareTag("wire"))
            {
                //Debug.Log("collision occuring at" + "");
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
                ResistorScript resistor = mostRecentCollidingObject.GetComponent<ResistorScript>();
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
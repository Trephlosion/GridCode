using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * This is a public class to detect whether the end of a wire has collided with one of the cells in our grid
 */
public class WireEnd : MonoBehaviour
{
    bool insertedInGrid = false;
    string returnVal = "No Value yet";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (insertedInGrid)
        {
            Debug.Log(returnVal);
            this.transform.parent.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            this.transform.parent.GetComponent<Renderer>().material.color = Color.white;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "gridcell")
        {

            insertedInGrid = true;
            returnVal = "OnTriggerEnter worked!";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "gridcell")
        {
            insertedInGrid = false;
            returnVal = "OnTriggerExit worked!";
        }
    }
}

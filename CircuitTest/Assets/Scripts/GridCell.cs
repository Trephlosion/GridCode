using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    bool inUse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inUse)
        {
            this.transform.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            this.transform.GetComponent<Renderer>().material.color = Color.black;

        }
    }

    private void OnTriggerEnter(Collider other)
    {


            inUse = true;
        
    }
    private void OnTriggerExit(Collider other)
    {
        
            inUse = false;
        
    }
}

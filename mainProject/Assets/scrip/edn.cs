using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edn : MonoBehaviour
{
    private bool active = false;

    //private string checker = "work";
    // Start is called before the first frame update
    private void Update()
    {
        if(active)
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
        if (other.gameObject.CompareTag("celltest"))
        {
            active = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("celltest"))
        {
            active = false;
        }
    }
}

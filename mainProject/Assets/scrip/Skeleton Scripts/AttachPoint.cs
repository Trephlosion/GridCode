using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject attachedToTile;
    public bool attachedTile;
    // Start is called before the first frame update
    void Start()
    {
        attachedTile = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void OnTriggerEnter(Collider other)
    {
        
        // If colliding with the cell, record the colliding tile with the system
        if (other.gameObject.tag == "Cell")
        {
            Debug.Log("Entered Cell!");
            attachedTile = true;
            attachedToTile = other.gameObject;
        }
        else if (other.gameObject.tag == "Power")
        {
            Debug.Log("Entered Power!");
            attachedTile = true;
            attachedToTile = other.gameObject;

        }
        else if (other.gameObject.tag == "Ground")
        {
            attachedTile = true;
            attachedToTile = other.gameObject;

        }
    }

    //public void OnCollisionExit(Collision collision)
    //{
    //    // If colliding with the cell, record the colliding tile with the system
    //    if (collision.gameObject.tag == "Cell")
    //    {
    //        attachedTile = false;
    //    }
    //    else if (collision.gameObject.tag == "Power")
    //    {
    //        attachedTile = false;

    //    }
    //    else if (collision.gameObject.tag == "Ground")
    //    {
    //        attachedTile = false;

    //    }
        
    //}
}

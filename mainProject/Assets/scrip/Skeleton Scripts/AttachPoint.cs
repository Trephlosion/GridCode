using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject attachedToTile;
    public bool attachedTile { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        attachedTile = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void OnCollisionEnter(Collision collision)
    {
        // If colliding with the cell, record the colliding tile with the system
        if (collision.gameObject.tag == "Cell")
        {
            attachedTile = true;
            attachedToTile = collision.gameObject.GetComponent<GameObject>();
        }
        else if (collision.gameObject.tag == "Power")
        {
            attachedTile = true;
            attachedToTile = collision.gameObject.GetComponent<GameObject>();

        }
        else if (collision.gameObject.tag == "Ground")
        {
            attachedTile = true;
            attachedToTile = collision.gameObject.GetComponent<GameObject>();

        }
    }

    public void OnCollisionExit(Collision collision)
    {
        // If colliding with the cell, record the colliding tile with the system
        if (collision.gameObject.tag == "Cell")
        {
            attachedTile = false;
        }
        else if (collision.gameObject.tag == "Power")
        {
            attachedTile = false;

        }
        else if (collision.gameObject.tag == "Ground")
        {
            attachedTile = false;

        }
        
    }
}

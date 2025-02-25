using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public string gridTileName = "Unset";
    public bool isPowered, isGrounded, isInactive = false;

    // Start is called before the first frame update
    void Start()
    {
        isPowered = false;
        isGrounded = false;
        isInactive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        gridTileName = other.gameObject.name;

        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (other.gameObject.CompareTag("Power"))
        {
            isPowered = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        gridTileName = "None";
        isInactive = true;
    }
    
    /* Returns the following values in the following order: 
     * isPowered, isGrounded, isInactive
     * */
    public bool[] SendInfo()
    {
        return new bool[] { isPowered, isGrounded, isInactive };
    }

    public string ConnectedTo()
    {
        return gridTileName;
    }
}

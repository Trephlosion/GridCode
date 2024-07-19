using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNAP : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wire"))
        {
            BroadcastMessage("wireSnapped");
            Debug.Log("wire snapped");
            
        }

        if (other.gameObject.CompareTag("resistor"))
        {
            BroadcastMessage("resistorSnapped");
            Debug.Log("resistor snapped");
        }
    }
}

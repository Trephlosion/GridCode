using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreePointJointChain : MonoBehaviour
{
    public GameObject cylinderPrefab; // Assign a cylinder prefab in the Inspector
    private GameObject cylinder1;
    private GameObject cylinder2;
    private CableProceduralSimple cableScript;

    private bool isGrabbed = false; // Simulate grab state (replace with actual grab logic)

    void Start()
    {
        // Spawn the first cylinder
        cylinder1 = Instantiate(cylinderPrefab, transform.position, Quaternion.identity);
        Rigidbody rb1 = cylinder1.AddComponent<Rigidbody>(); // Add Rigidbody for physics interaction

        // Spawn the second cylinder slightly offset
        Vector3 offset = new Vector3(0, 1, 0); // Adjust offset as needed
        cylinder2 = Instantiate(cylinderPrefab, transform.position + offset, Quaternion.identity);
        Rigidbody rb2 = cylinder2.AddComponent<Rigidbody>();

        // Add a ConfigurableJoint to the first cylinder and connect it to the second
        ConfigurableJoint joint = cylinder1.AddComponent<ConfigurableJoint>();
        joint.connectedBody = rb2;

        // Configure the joint settings
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;

        joint.angularXMotion = ConfigurableJointMotion.Limited;
        joint.angularYMotion = ConfigurableJointMotion.Limited;
        joint.angularZMotion = ConfigurableJointMotion.Limited;

        // Add the CableProceduralSimple script to the first cylinder
        cableScript = cylinder1.AddComponent<CableProceduralSimple>();
        cableScript.endPointTransform = cylinder2.transform; // Set the second cylinder as the endpoint
    }

    void Update()
    {
        // Simulate grab state (replace with actual grab logic)
        if (Input.GetKeyDown(KeyCode.G)) // Press 'G' to toggle grab state
        {
            isGrabbed = !isGrabbed;
        }

        // Enable or disable the cable script based on grab state
        if (cableScript != null)
        {
            cableScript.enabled = !isGrabbed;
        }
    }
}
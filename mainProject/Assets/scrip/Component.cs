using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Component : MonoBehaviour
{
    public string componentType;
    public GameObject headptr = null, tailptr = null;

    private TMP_Text textVal;
    private Connector headptrConnections;
    private Connector tailptrConnections;

    // Start is called before the first frame update
    void Start()
    {
        textVal = this.gameObject.GetComponentInChildren<TMP_Text>();
        headptr = this.gameObject.GetNamedChild("head");
        tailptr = this.gameObject.GetNamedChild("tail");

        if (textVal)
        {
            textVal.text = ("Accessed Text!");
        }
        //else if (textVal == null)
        //{
        //    textVal.text = ("Did not access Text!");
        //}

        if (headptr && tailptr)
        {
            headptrConnections = headptr.GetComponent<Connector>();
            tailptrConnections = tailptr.GetComponent<Connector>();
        }
        else
        {
            textVal.text = "No head or tail!";
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if(headptrConnections && tailptrConnections)
        {
            textVal.text = "Head: "
             + headptrConnections.ConnectedTo().ToString()
            + "Tail:" + tailptrConnections.ConnectedTo().ToString()
            ;
        }
        else
        {
            textVal.text = "Unable to get the head and tail scripts";
        }
        //TextVal();
        
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }
    //    if (other.gameObject.CompareTag("Power"))
    //    {
    //        isPowered = true;
    //    }
        
    //}

    private void TextVal()
    {

        
    }
}

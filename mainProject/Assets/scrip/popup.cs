using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<Canvas>().gameObject.SetActive(true);
        // this.gameObject.GetComponent<Collider>().enabled = false;
    }

   public void OnClickywicky()
    {
        Destroy(this.gameObject);
    }
}

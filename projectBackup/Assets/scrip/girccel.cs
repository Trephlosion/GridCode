using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class girccel : MonoBehaviour
{
   //set a bool

   private bool Occ;
   
   //fixed updatea
   private void Update()
   {
      if(Occ)
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
      Occ = true;
   }

   private void OnTriggerExit(Collider other)
   {
      Occ = false;
   }
}

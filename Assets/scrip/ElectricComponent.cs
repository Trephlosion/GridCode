using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricComponent : MonoBehaviour
{
   
   public  Cell head,tail;
   //private Material properties;
   //private Special functions;
   
   private bool active = false;
   private Renderer _renderer;

   //private string checker = "work";
   // Start is called before the first frame update
   private void Start()
   {
      _renderer = this.transform.GetComponent<Renderer>();
   }

   private void Update()
   {
      //TODO: change this to make an outline

      _renderer.material.color = active ? Color.green : Color.white;
   }

   public void setPos(int xpos, int ypos)
   {
      if (this.gameObject.CompareTag("Head"))
      {
         head.x = xpos;
         head.y = ypos;
         
      }

      if (this.gameObject.CompareTag("Tail"))
      {
         tail.x = xpos;
         tail.y = ypos;
      }
      
   }


   private void OnTriggerStay(Collider other)
   {
      if (other.gameObject.CompareTag("Cell"))
      {
         active = true;
         
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.gameObject.CompareTag("Cell"))
      {
         active = false;
      }
   }

}

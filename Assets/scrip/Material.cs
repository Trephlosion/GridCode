using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI.BodyUI;

public class Material : MonoBehaviour
{
   public float innateResistance, intakeVoltage, capacitance, inductance;
   public bool isLed = false, isResistor = false, isLive = false, isWeak = false, isBurst = false;
   public string nombre;
   private XRDirectInteractor interactor = null;

   //popup window function
   //this function can be by being called in each child class
   public void popUp()
   {
      if (isLed)
      {
         
      }
      
      //menu has:
      //name of object (LED, Wire, Resistor)
      //resistance (ohms)
      //Voltage 
      //extra info
   }

   
   //can choose the desired component in the inspector
   public void defineComponent()
   {
      if (isLed)
      {
         this.gameObject.AddComponent<LED>();
         
      }

      else if (isResistor)
      {
         this.gameObject.AddComponent<Resistor>();
      }
   }

   public void setStatus()
   {
      if (this.gameObject.GetComponent<Cell>().instantaneousVoltage >= intakeVoltage)
      {
         isWeak = false;
         isLive = false;
         isBurst = true;
      }
      else if (Mathf.Approximately(this.gameObject.GetComponent<Cell>().instantaneousVoltage, intakeVoltage))
      {
         isWeak = false;
         isLive = true;
         isBurst = false;
      }
      else if (this.gameObject.GetComponent<Cell>().instantaneousVoltage <= intakeVoltage)
      {
         isWeak = true;
         isLive = true;
         isBurst = false;
      }
   }
   
}

public class LED : Material
{ 
   //LED Specific Script
   
   private bool isOn;

   private float R, G, B;
   
// initialize public variables here
   public void Start()
   {
      nombre = "LED";
      innateResistance = 4.0f;
      intakeVoltage = 3.0f;
      popUp();
   }
   
   
}

public class Resistor : Material
{
   public Resistor()
   {
      nombre = "Resistor";
      innateResistance = 2.0f;
   }
}

public class Capacitor : Material
{
   public Capacitor()
   {
      nombre = "Capacitor";
      innateResistance = 1.0f;
      intakeVoltage = 0.5f;
      capacitance = 0.01f;
   }
}

public class Inductor : Material
{
   public Inductor()
   {
      nombre = "Inductor";
      innateResistance = 3.0f;
      intakeVoltage = 2.0f;
      inductance = 0.5f;
   }
   
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Material : MonoBehaviour
{
   public float innateResistance, intakeVoltage;
   public bool isLive = false, isWeak = false, isBurst = false;
   
   


   public void setStatus()
   {
      if (this.gameObject.GetComponent<Cell>().instantaneauosVoltage >= intakeVoltage)
      {
         isWeak = false;
         isLive = false;
         isBurst = true;
      }
      else if (Mathf.Approximately(this.gameObject.GetComponent<Cell>().instantaneauosVoltage, intakeVoltage))
      {
         isWeak = false;
         isLive = true;
         isBurst = false;
      }
      else if (this.gameObject.GetComponent<Cell>().instantaneauosVoltage <= intakeVoltage)
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
   public LED()
   {
      innateResistance = 4.0f;
      intakeVoltage = 3.0f;
   }
   
   
}

public class Resistor : Material
{
   public Resistor()
   {
      innateResistance = 2.0f;
   }
}

public class Capacitor : Material
{
   private float capacitance;

   public Capacitor()
   {
      innateResistance = 1.0f;
      intakeVoltage = 0.5f;
      capacitance = 0.01f;
   }
}

public class Inductor : Material
{
   private float inductance;

   public Inductor()
   {
      innateResistance = 3.0f;
      intakeVoltage = 2.0f;
      inductance = 0.5f;
   }
}

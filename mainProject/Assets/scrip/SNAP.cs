using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using Unity.VisualScripting;
using Color = System.Drawing.Color;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Collections;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit.Utilities;


public class SNAP : MonoBehaviour
{
    public float lampresistance = 3.0f;
    [SerializeField] private GameObject grnLEDParticle, explosion, lightning, lightning2;

    public void Start()
    {
        grnLEDParticle.SetActive(false);
        explosion.SetActive(false);
    }

    
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("conductor"))
        {
            // BroadcastMessage("wireSnapped");
            lightning.SetActive(true);
            Debug.Log("wire snapped");
            explosion.SetActive(true);
            explosion.GetComponent<ParticleSystem>().Play();
            //starting the lightning when the wire comes into contact with the grid
           
        }

        if (other.gameObject.CompareTag("resistor"))
        {
            lightning2.SetActive(true);
            // BroadcastMessage("resistorSnapped");
            Debug.Log("resistor snapped");
            
            //validate resistance
            if (other.gameObject.GetComponent<Material>().innateResistance >= lampresistance)
            {
                //start weak lamp
                grnLEDParticle.SetActive(true);
                grnLEDParticle.GetComponent<ParticleSystem>().Play();

            }
            else if (Mathf.Approximately(other.gameObject.GetComponent<Material>().innateResistance,
                         lampresistance))
            {
                // start active lamp
                grnLEDParticle.SetActive(true);
                grnLEDParticle.GetComponent<ParticleSystem>().Play();
                // particle.GetComponent<ParticleSystem>().main.startColor = Color.FromArgb(130, 255, 53);

            }
            else if (other.gameObject.GetComponent<Material>().innateResistance <= lampresistance)
            {
                // start explosion
                explosion.SetActive(true);
                explosion.GetComponentInChildren<ParticleSystem>().Play();
            }

        }
    }


    public void STOPPARTY()
    {
        //desacivate led
        grnLEDParticle.GetComponent<ParticleSystem>().Pause();
        grnLEDParticle.SetActive(false);
        grnLEDParticle.GetComponent<ParticleSystem>().Stop();
        //decativate expllosion
        explosion.GetComponentInChildren<ParticleSystem>().Pause();
        explosion.SetActive(false);
        explosion.GetComponentInChildren<ParticleSystem>().Stop();
        
        //deactivate lightning
        lightning.SetActive(false);
        lightning2.SetActive(false);
    }


    public void elecSense()
    {
        
    }
    
    
    


    // public void OnTriggerStay(Collider other)
    //     {
    //
    //         if (other.gameObject.CompareTag("conductor"))
    //         {
    //             BroadcastMessage("wireSnapped");
    //             Debug.Log("wire snapped");
    //             particle.SetActive(true);
    //             particle.GetComponent<ParticleSystem>().Play();
    //         }
    //
    //         if (other.gameObject.CompareTag("resistor"))
    //         {
    //             BroadcastMessage("resistorSnapped");
    //             Debug.Log("resistor snapped");
    //             //validate resistance
    //             if (other.gameObject.GetComponent<Material>().innateResistance >= lampresistance)
    //             {
    //                 //start weak lamp
    //                 particle.SetActive(true);
    //                 particle.GetComponent<ParticleSystem>().Play();
    //
    //             }
    //             else if (Mathf.Approximately(other.gameObject.GetComponent<Material>().innateResistance,
    //                          lampresistance))
    //             {
    //                 // start active lamp
    //                 particle.SetActive(true);
    //                 particle.GetComponent<ParticleSystem>().Play();
    //                 // particle.GetComponent<ParticleSystem>().main.startColor = Color.FromArgb(130, 255, 53);
    //
    //             }
    //             else if (other.gameObject.GetComponent<Material>().innateResistance <= lampresistance)
    //             {
    //                 // start explosion
    //                 particle.SetActive(true);
    //                 particle.GetComponent<ParticleSystem>().Play();
    //             }
    //
    //
    //     }
    //
    //
    //     public void OnTriggerExit(Collider other)
    //     {
    //        ;
    //         
    //     }
}

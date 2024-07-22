using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class streamer : MonoBehaviour
{
    public float resisNeeded = 3.0f;
    public void wireSnapped()
    {
        Debug.Log("wire snapped");
        this.gameObject.SetActive(true);
        this.gameObject.GetComponent<ParticleSystem>().Play();
    }

    public void resistorSnapped(float resis)
    {
        
        Debug.Log("resistor snapped");
        //validate resistance
        if (resis >= resisNeeded)
        {
            //start weak lamp
            this.gameObject.SetActive(true);
            this.gameObject.GetComponent<ParticleSystem>().Play();

        }
        else if (Mathf.Approximately(resis, resisNeeded))
        {
            // start active lamp
            this.gameObject.SetActive(true);
            this.gameObject.GetComponent<ParticleSystem>().Play();
            // particle.GetComponent<ParticleSystem>().main.startColor = Color.FromArgb(130, 255, 53);

        }
        else if (resis <= resisNeeded)
        {
            // start explosion
            this.gameObject.SetActive(true);
            this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
    
    public void STOPPARTY()
    {
        //desacivate led
        this.gameObject.GetComponent<ParticleSystem>().Pause();
        this.gameObject.SetActive(false);
        this.gameObject.GetComponent<ParticleSystem>().Stop();
        //decativate expllosion
        this.gameObject.GetComponentInChildren<ParticleSystem>().Pause();
        this.gameObject.SetActive(false);
        this.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        
    }
    
}


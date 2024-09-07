using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMacActivator : MonoBehaviour
{

    public bool PlayerInRange = false;

    //public bool canInteract = false;

    public float speed = 1;

    public float totalTime = 5;

    public bool on = false;

    public bool doorClose = true;

    public WashingMacHazard wash;

    public ParticleSystem FogEffect;
    public ParticleSystem BubbleEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            WashingTimer();
        }

        if (doorClose)
        {
            FogEffect.Stop();
            BubbleEffect.Stop();
        }

        if (!doorClose)
        {
            FogEffect.Play();
            BubbleEffect.Play();
        }

        // When target is hit
        if (PlayerInRange && !on)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                on = true;
               
                //canInteract = false;

                wash.goWash = true;

                //FogEffect.Play();
                //BubbleEffect.Play();

                if (!doorClose)
                {
                    FogEffect.Play();
                    BubbleEffect.Play();
                }
            }
        }
        else if (PlayerInRange && on)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                on = false;

                //canInteract = false;

                wash.goWash = false;

                FogEffect.Stop();
                BubbleEffect.Stop();

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        // When target is hit
        if (other.gameObject.name == "ExternalChecker")
        {
            PlayerInRange = true;
            //canInteract = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {

        // When target is hit
        if (other.gameObject.name == "ExternalChecker")
        {
            PlayerInRange = false;
        }
    }


    void WashingTimer()
    {
        if (totalTime > 0 && on)
        {

            // Subtract elapsed time every frame
            totalTime -= 1 * Time.deltaTime;


        }
        else
        {
            totalTime = 0;

        }

        if (totalTime == 0)
        {

            wash.goWash = false;

            on = false;

            FogEffect.Stop();
            BubbleEffect.Stop();

            totalTime = 5;

        }


    }


}

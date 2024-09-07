using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracDoorOven : MonoBehaviour
{
    public bool PlayerInRange = false;

    public bool canInteract = true;

    public bool doorIsOpen = false;

    public OvenDoor door;

    public AudioSource Open;
    public AudioSource Close;

    public Light light;
    public ParticleSystem fireFront;
    public ParticleSystem fireBack;
    public ParticleSystem fireLeft;
    public ParticleSystem fireRight;

    void Start()
    {
        light.enabled = false;
    }

    void Update()
    {
        //if (!doorIsOpen)
        //{
            
        //}
        

        if (PlayerInRange && !doorIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                door.open = true;
                door.rotate = false;

                doorIsOpen = true;

                light.enabled = true;
                fireFront.Play();
                fireBack.Play();
                fireLeft.Play();
                fireRight.Play();

                Open.Play();
            }
        }

        else if (PlayerInRange && doorIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                door.open = false;

                door.rotate = true;

                doorIsOpen = false;

                light.enabled = false;
                fireFront.Stop();
                fireBack.Stop();
                fireLeft.Stop();
                fireRight.Stop();

                Close.Play();
            }
        }


    }



    private void OnTriggerEnter(Collider other)
    {

        // When target is hit
        if (other.gameObject.name == "ExternalChecker")
        {
            PlayerInRange = true;
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
}

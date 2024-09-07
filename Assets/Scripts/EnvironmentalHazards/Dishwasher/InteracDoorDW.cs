using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UIElements;

public class InteracDoorDW : MonoBehaviour
{
    public bool PlayerInRange = false;

    public bool canInteract = true;

    public bool doorIsOpen = false;

    public DishwasherDoor door;

    public AudioSource Open;
    public AudioSource Close;

    public ParticleSystem mistFront;
    public ParticleSystem mistCenter;

    void Update()
    {
        if (PlayerInRange && !doorIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                door.open = true;
                door.rotate = false;

                doorIsOpen = true;

                mistFront.Play();
                mistCenter.Play();

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

                mistFront.Stop();
                mistCenter.Stop();

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

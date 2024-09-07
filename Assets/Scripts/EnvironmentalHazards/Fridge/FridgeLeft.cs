using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class FridgeLeft : MonoBehaviour
{
    public bool PlayerInRange = false;

    public bool canInteract = true;

    public bool doorIsOpen = false;

    public AudioSource Open;

    public FridgeDoor door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (PlayerInRange && !doorIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                door.isOpeningL = true;

                door.rotateL = false;

                doorIsOpen = true;

                Open.Play();

            }
        }

        else if (PlayerInRange && doorIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                door.isOpeningL = false;

                door.rotateL = true;

                doorIsOpen = false;

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

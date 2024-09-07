using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeRight : MonoBehaviour
{
    public bool PlayerInRange = false;

    public bool canInteract = true;

    public bool doorIsOpen = false;

    public FridgeDoor door;

    public AudioSource Open;

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
                door.isOpeningR = true;

                door.rotateR = false;

                doorIsOpen = true;

                Open.Play();

            }
        }

        else if (PlayerInRange && doorIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                door.isOpeningR = false;

                door.rotateR = true;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryerOpenDoor : MonoBehaviour
{
    public bool PlayerInRange = false;

    public bool canInteract = true;

    public bool doorIsOpen = false;

    public DryerDoor door;

    public DryerActivator cantInteract;

    public ParticleSystem SteamOut;
    public ParticleSystem Steaming;

    public AudioSource Open;
    public AudioSource Close;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange && !doorIsOpen && canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                door.isOpening = true;

                door.rotate = false;

                doorIsOpen = true;

                cantInteract.door = true;

                SteamOut.Play();
                Steaming.Play();

                Open.Play();
            }
        }

        else if (PlayerInRange && doorIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                door.isOpening = false;

                door.rotate = true;

                doorIsOpen = false;

                cantInteract.door = false;

                Steaming.Stop();

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

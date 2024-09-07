using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMacDoor : MonoBehaviour
{
    public bool PlayerInRange = false;

    public bool canInteract = true;

    public bool hoodIsOpen = false;

    //public float speed = 1;

    //public float totalTime = 5;

    //public bool on = false;

    public WashingMacHood wash;

    public AudioSource Open;
    public AudioSource Close;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange && !hoodIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                wash.isWashing = true;

                wash.rotate = false;

                hoodIsOpen = true;

                Open.Play();

                Debug.Log("Open Hood");
            }
        }
        else if (PlayerInRange && hoodIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                wash.isWashing = false;

                wash.rotate = true;

                hoodIsOpen = false;

                Close.Play();

                Debug.Log("Close Hood");
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

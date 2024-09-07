using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryerActivator : MonoBehaviour
{
    public DryerHazard On;

    public DryerOpenDoor canOpen;

    public bool PlayerInRange = false;

    public bool canInteract = true;

    public float totalTime = 4;

    public bool door = false;

    public AudioSource Open;
    public AudioSource Close;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canInteract)
        {
            DryerTimer();
                
        }

        if (canInteract && PlayerInRange && !door)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                On.On = true;
                canInteract = false;
                canOpen.canInteract= false;

                Open.Play();

                //Debug.Log("ACTIVATE WAFFLEMAKER");
            }
        }
        else if (canInteract && PlayerInRange && door)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Dryer can only Activate if Door is Closed");
                Close.Play();
            }
        }
    }

    void DryerTimer()
    {
        if (totalTime > 0)
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
            totalTime = 4;
            On.On = false;
            canInteract = true;
            canOpen.canInteract = true;
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

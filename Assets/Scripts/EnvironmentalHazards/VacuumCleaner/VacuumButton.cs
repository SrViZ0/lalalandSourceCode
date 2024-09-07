using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumButton : MonoBehaviour
{
    public bool Vacc_Status = false;
    public bool PlayerInRange = false;

    public VacuumForce theSuction;

    public AudioSource Sucking;


    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange && !Vacc_Status)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {


                Debug.Log("Vacuum is Switched ON");
                Vacc_Status = true;
                theSuction.vacuumPower = true;

                Sucking.Play();
            }
        }

        else if (PlayerInRange && Vacc_Status)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Vacuum is Switched OFF");
                Vacc_Status = false;
                theSuction.vacuumPower = false;

                Sucking.Stop();
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

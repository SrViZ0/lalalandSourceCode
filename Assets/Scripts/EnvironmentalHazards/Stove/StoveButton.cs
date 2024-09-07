using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveButton : MonoBehaviour
{
    public bool Stove_Status = false;
    public bool PlayerInRange = false;

    public ParticleSystem fire;
    public GameObject button;

    public Stove theFire;
    public AudioSource sound;


    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerInRange && !Stove_Status)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Stove is Switched ON");
                Stove_Status = true;
                theFire.stoveFire = true;
                button.transform.localRotation = Quaternion.Euler(0, 0, 90);

                fire.Play();
                sound.Play();
            }
        }

       else if (PlayerInRange && Stove_Status)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Stove is Switched OFF");
                Stove_Status = false;
                theFire.stoveFire = false;
                fire.Stop();
                sound.Stop();
                button.transform.localRotation = Quaternion.Euler(0, 0, 0);
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

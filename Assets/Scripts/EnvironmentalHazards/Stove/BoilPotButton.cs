using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilPotButton : MonoBehaviour
{
    public bool Stove_Status = false;
    public bool PlayerInRange = false;
    public bool Play = false;

    public ParticleSystem fire;
    public GameObject button;

    public Stove theFire;
    public BoilingWater boilWater;
    public ParticleSystem BoilingEffect;

    void Start()
    {
        //fireEffect.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Play)
        {
            
        }
        else if (!Play)
        {
            
        }

        if (PlayerInRange && !Stove_Status)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Stove is Switched ON");
                Stove_Status = true;
                theFire.stoveFire = true;
                boilWater.isBoiling = true;
                BoilingEffect.Play();
                fire.Play();

                button.transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
        }

        else if (PlayerInRange && Stove_Status)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Stove is Switched OFF");
                Stove_Status = false;
                theFire.stoveFire = false;
                boilWater.isBoiling = false;
                BoilingEffect.Stop();
                fire.Stop();
                
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

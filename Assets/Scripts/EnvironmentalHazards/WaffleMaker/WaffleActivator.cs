using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class WaffleActivator : MonoBehaviour
{
    [Serialize]
    public float totalTime = 2;

    public bool canInteract = true;

    public bool PlayerInRange = false;

    public bool on = false;

    public bool enemyPresent = false;


    public Transform waffleSpawnPoint;

    public WaffleMaker maker;
    public WaffleCooking cooking;

    public ParticleSystem smoke;

    public AudioSource Sizzling;

    [Header("Waffle Variety")]
    public GameObject WaffleEggplant;
    public GameObject WafflePencil;
    public GameObject WaffleMantis;
    public GameObject WafflePumpkin;
    public GameObject WaffleBear;
    public GameObject WaffleKamenTank;

    public bool Eggplant_W = false;
    public bool Pencil_W = false;
    public bool Mantis_W = false;
    public bool Pumpkin_W = false;
    public bool Bear_W = false;
    public bool KamenTank_W = false;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            WaffleCookTimer();
            
        }

        // When target is hit
        if (!on && canInteract && PlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                on = true;
                maker.isWaffling = true;
                cooking.isCooking = true;
                canInteract = false;
                Sizzling.Play();

                Debug.Log("ACTIVATE WAFFLEMAKER");
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

    void WaffleCookTimer()
    {
        if (totalTime > 0 && on)
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
           

            maker.isWaffling = false;
            cooking.isCooking = false;

            maker.rotate = true;

            on = false;

            totalTime= 2;

            Sizzling.Stop();

            if (enemyPresent)
            {
                if (Eggplant_W)
                {
                    Instantiate(WaffleEggplant, waffleSpawnPoint.transform.position, Quaternion.identity);
                    Eggplant_W = false;
                }
                if (Pencil_W)
                {
                    Instantiate(WafflePencil, waffleSpawnPoint.transform.position, Quaternion.identity);
                    Pencil_W = false;
                }
                if (Mantis_W)
                {
                    Instantiate(WaffleMantis, waffleSpawnPoint.transform.position, Quaternion.identity);
                    Mantis_W = false;
                }
                if (Pumpkin_W)
                {
                    Instantiate(WafflePumpkin, waffleSpawnPoint.transform.position, Quaternion.identity);
                    Pumpkin_W = false;
                }
                if (Bear_W)
                {
                    Instantiate(WaffleBear, waffleSpawnPoint.transform.position, Quaternion.identity);
                    Bear_W = false;
                }
                if (KamenTank_W)
                {
                    Instantiate(WaffleKamenTank, waffleSpawnPoint.transform.position, Quaternion.identity);
                    KamenTank_W = false;
                }


                smoke.Play();
                Sizzling.Stop();
                enemyPresent = false;
            }

        }


    }

}

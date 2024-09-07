using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanButton : MonoBehaviour
{
    public bool PlayerInRange = false;

    public bool lidOpen = false;

    public TrashCanLid trashLid;


    public float totalTime = 2;

    public float openTime = 8;

    public bool on = false;

    public bool onTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (onTimer)
        {
            OpenTimer();
        }

        if (on)
        {
            Timer();
        }

        if (PlayerInRange && !lidOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                lidOpen = true;
                onTimer = true;
            }
        }

        //else if (PlayerInRange && lidOpen)
        //{
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
                
        //    }
        //}

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


    void Timer()
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

            trashLid.openLid = false;
            trashLid.rotate = true;

            on = false;

            totalTime = 2;

            

        }


    }

    void OpenTimer()
    {
        if (openTime > 0)
        {

            openTime -= 1 * Time.deltaTime;

        }
        else
        {
            openTime = 0;

        }

        if (openTime == 0)
        {

            trashLid.openLid = true;
            trashLid.throwPlayer = true;

            on = true;
            onTimer = false;

            openTime = 8;

        }

    }

}

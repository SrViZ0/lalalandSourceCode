using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletFlushActivator : MonoBehaviour
{
    public bool PlayerInRange = false;

    //public bool canInteract = false;

    //public float speed = 1;

    //public float totalTime = 5;

    public bool on = false;

    //public AudioSource Flush;

    public ToiletHazard flushing;

    public AudioSource Flushing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                flushing.flushPower = true;
                if (!Flushing.isPlaying)
                {
                    Flushing.Play();
                }

                //Flush.Play();
            }
        }
        //else if (PlayerInRange  )
        //{
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        flushing.flushPower = false;
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {

        // When target is hit
        if (other.gameObject.name == "ExternalChecker")
        {
            PlayerInRange = true;
            //canInteract = true;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeBottom : MonoBehaviour
{
    public bool open = false;

    public float speed = 0.05f;

    public bool PlayerInRange = false;

    public bool isOpen = false;

    public Transform drawer1;

    public AudioSource Open;

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                open = true;
                isOpen = true;

                Open.Play();
            }
        }
        else if (PlayerInRange && isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                open = false;
                isOpen = false;
            }
        }

        if (open)
        {
            // 4.266661f, 1.69773f, 0.009642899f
            Vector3 startLocalPosition = new Vector3(0, 0, 0);
            //
            Vector3 endLocalPosition = new Vector3(-0.08f, 0, 0);

            drawer1.transform.localPosition = Vector3.MoveTowards(drawer1.transform.localPosition, endLocalPosition, speed);
        }
        else if (!open)
        {

            Vector3 startLocalPosition = new Vector3(0, 0, 0);

            drawer1.transform.localPosition = Vector3.MoveTowards(drawer1.transform.localPosition, startLocalPosition, speed);
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


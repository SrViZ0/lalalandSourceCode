using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyDrawer2 : MonoBehaviour
{
    public bool open = false;

    public float speed;

    public bool PlayerInRange = false;

    public bool isOpen = false;

    public Transform drawer1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                open = true;
                isOpen = true;
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
            Vector3 endLocalPosition = new Vector3(0.04f, 0, 0);

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

        //if (other.gameObject == Drawer1)
        //{
        //    PlayerInRange = true;
        //}

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
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WashingMacHood : MonoBehaviour
{
    public Quaternion startQuaternion;

    public float lerpTime = 5;

    public bool rotate = false;

    public bool isWashing;

    public Transform pivot;

    public WashingMacActivator door;

    [Serialize]
    public float rotateTimer = 2;



    // Start is called before the first frame update
    void Start()
    {
        startQuaternion = transform.rotation;


    }

    // Update is called once per frame
    void Update()
    {


        if (rotate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startQuaternion, Time.deltaTime * lerpTime);
            door.doorClose = true;


            //if (rotateTimer > 0)
            //{

            //    rotateTimer -= 1 * Time.deltaTime;


            //}
            //else
            //{
            //    rotateTimer = 0;


            //}

            //if (rotateTimer == 0)
            //{
            //    rotate = false;

            //    rotateTimer = 2;
            //    canInteract.canInteract = true;

            //}

        }

        if (isWashing)
        {


            Quaternion goWashing = Quaternion.Lerp(transform.rotation, Quaternion.Euler(327.68f, 90.00001f, 270f), Time.deltaTime * lerpTime);

            pivot.transform.rotation = goWashing;

            door.doorClose = false;
        }



    }

    public void snapRotation()
    {
        transform.rotation = startQuaternion;
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashCanLid : MonoBehaviour
{
    public Quaternion startQuaternion;

    public float lerpTime = 50;

    public float lidCloseTime = 5;

    public bool rotate = false;

    public bool openLid = false;

    public bool throwPlayer = false;

    public Transform pivot;

    public TrashCanButton canInteract;

    public TrashCanJump jump;

   

    [Serialize]
    public float rotateTimer = 2;



    // Start is called before the first frame update
    void Start()
    {
        startQuaternion = transform.rotation;


    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (rotate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startQuaternion, Time.deltaTime * lidCloseTime);



            if (rotateTimer > 0)
            {

                // Subtract elapsed time every frame
                rotateTimer -= 1 * Time.deltaTime;


            }
            else
            {
                rotateTimer = 0;


            }

            if (rotateTimer == 0)
            {
                rotate = false;

                rotateTimer = 2;
                canInteract.lidOpen = false;
                jump.canThrow = true;

            }

        }

        if (openLid)
        {

            // 315 - 90.00001 - 270
            Quaternion openingLid = Quaternion.Lerp(transform.rotation, Quaternion.Euler(315f, 90.00001f, 270f), Time.deltaTime * lerpTime);

            pivot.transform.rotation = openingLid;

            jump.ThrowForce();

            jump.canThrow = false;

            //if (throwPlayer)
            //{
            //    jump.ThrowForce();

            //}

        }


    }

    public void snapRotation()
    {
        transform.rotation = startQuaternion;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaffleMaker : MonoBehaviour
{

    public Quaternion startQuaternion;

    public float lerpTime = 5;

    public bool rotate = false;

    public bool isWaffling;

    public Transform pivot;

    public WaffleActivator canInteract;

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
            


            if (rotateTimer > 0)
            {
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
                canInteract.canInteract = true;
            }

        }

        if (isWaffling)
        {
            Quaternion goCooking = Quaternion.Lerp(transform.rotation, Quaternion.Euler(1.999965f, 0.006807f, 4.124122f), Time.deltaTime * lerpTime);
            pivot.transform.rotation = goCooking;
        }

    }

    public void snapRotation()
    {
        transform.rotation = startQuaternion;
    }

}

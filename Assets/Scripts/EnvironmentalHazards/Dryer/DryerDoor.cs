using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryerDoor : MonoBehaviour
{
    public Quaternion startQuaternion;

    public float lerpTime = 5;

    public bool rotate;

    public bool isOpening;

    public bool door;



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
            
        }

        if (isOpening)
        {
            Quaternion goDrying = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 267.9426f, 0f), Time.deltaTime * lerpTime);

            transform.rotation = goDrying;
            
        }
    }


    public void snapRotation()
    {
        transform.rotation = startQuaternion;
    }
}

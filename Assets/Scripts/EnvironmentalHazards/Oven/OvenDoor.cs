using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenDoor : MonoBehaviour
{
    public Quaternion startQuaternion;
    public float lerpTime = 5;
    public bool rotate = false;

    public bool open = false;

    void Start()
    {
        startQuaternion = transform.rotation;


    }

    public void snapRotation()
    {
        transform.rotation = startQuaternion;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startQuaternion, Time.deltaTime * lerpTime);

        }

        if (open)
        {
            Quaternion goCooking = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, 69.99999f), Time.deltaTime * lerpTime);
            transform.rotation = goCooking;
        }
    }
}

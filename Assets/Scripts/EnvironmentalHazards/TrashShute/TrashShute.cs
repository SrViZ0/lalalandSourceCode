using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashShute : MonoBehaviour
{


    public Quaternion startQuaternion;

    public float lerpTime = 5;

    public bool rotate;

    public bool isOpening;

    public bool closed;  

    public ParticleSystem Stink;

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
            Stink.Play();
        }

        if (isOpening)
        {


            Quaternion goCooking = Quaternion.Lerp(transform.rotation, Quaternion.Euler(320f, 90f, 90f), Time.deltaTime * lerpTime);

            transform.rotation = goCooking;

            Stink.Play();
        }



    }

    public void snapRotation()
    {
        transform.rotation = startQuaternion;
    }
}

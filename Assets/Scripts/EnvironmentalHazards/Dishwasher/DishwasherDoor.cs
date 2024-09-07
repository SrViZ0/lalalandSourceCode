using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UIElements;

public class DishwasherDoor : MonoBehaviour
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
            Quaternion goCooking = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 290), Time.deltaTime * lerpTime);
            transform.rotation = goCooking;
        }
    }

}

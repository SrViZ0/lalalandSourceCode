using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFan : MonoBehaviour
{
    public bool deadly = true;

    public float rotateSpeed;

    public float killingSpeed = 400;

    public float passiveSpeed = 70;

    Vector3 rotationDirection = new Vector3(0f, -1f, 0f);

    // Fan main position = -9.208843, 4.79, -28.87357 

    //void Start()
    //{
    //    rotateSpeed = killingSpeed;
    //}

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeed * rotationDirection * Time.deltaTime);

        if (!deadly)
        {
            rotateSpeed = passiveSpeed;
        }
        else if (deadly)
        {
            rotateSpeed = killingSpeed;
        }
    }
}

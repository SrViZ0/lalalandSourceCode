using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FridgeDoor : MonoBehaviour
{
    public Quaternion leftQuaternion;
    public Quaternion rightQuaternion;

    public float lerpTime = 5;

    public bool rotateL;
    public bool rotateR;

    public bool isOpeningL;
    public bool isOpeningR;

    public Transform hingeL;
    public Transform hingeR;



    // Start is called before the first frame update
    void Start()
    {
        leftQuaternion = hingeL.transform.rotation;
        rightQuaternion = hingeR.transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {

        if (rotateL)
        {
            hingeL.transform.rotation = Quaternion.Lerp(hingeL.transform.rotation, leftQuaternion, Time.deltaTime * lerpTime);

        }
        if (rotateR)
        {
            hingeR.transform.rotation = Quaternion.Lerp(hingeR.transform.rotation, leftQuaternion, Time.deltaTime * lerpTime);

        }

        if (isOpeningL)
        {
            Quaternion openL = Quaternion.Lerp(hingeL.transform.rotation, Quaternion.Euler(270f, 301.9835f, 0f), Time.deltaTime * lerpTime);

            hingeL.transform.rotation = openL;
        }
        if (isOpeningR)
        {
            Quaternion openR = Quaternion.Lerp(hingeR.transform.rotation, Quaternion.Euler(270f, 57.98346f, 0f), Time.deltaTime * lerpTime);

            hingeR.transform.rotation = openR;

        }
    }


    public void snapRotation()
    {
        transform.rotation = leftQuaternion;
        transform.rotation = rightQuaternion;
    }
}

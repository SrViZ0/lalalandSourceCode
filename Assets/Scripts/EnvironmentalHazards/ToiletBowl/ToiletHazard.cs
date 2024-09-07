using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletHazard : MonoBehaviour
{
    public LayerMask pullableLayers;

    public float pullStrength = 50;

    public float totalTime = 5;

    public Collider[] targetObjects;

    public Transform toiletHolePoint;

    public bool flushPower = false;

    public FlushDestroy flush;

    void Update()
    {
        if (flushPower)
        {
            Timer();
            flush.flushIsOn= true;
            PullForce();
        }
    }

    public void PullForce()
    {
        if (targetObjects != null && targetObjects.Length > 0)
        {
            foreach (Collider col in targetObjects)
            {
                if (col.GetComponent<Rigidbody>())
                {
                    col.GetComponent<Rigidbody>().velocity = (toiletHolePoint.position - col.transform.position) * pullStrength * Time.deltaTime;
                }
            }
        }
    }

    private void GetINSIDE()
    {
        targetObjects = null;
        targetObjects = Physics.OverlapBox(gameObject.transform.position, new Vector3(gameObject.transform.localScale.x / 2, gameObject.transform.localScale.y / 2, gameObject.transform.localScale.z / 2), Quaternion.identity, pullableLayers);


    }

    private void OnTriggerStay(Collider other)
    {
        GetINSIDE();
    }



    void Timer()
    {
        if (totalTime > 0)
        {

            // Subtract elapsed time every frame
            totalTime -= 1 * Time.deltaTime;


        }
        else
        {
            totalTime = 0;

        }

        if (totalTime == 0)
        {
            flushPower= false;
            flush.flushIsOn = false;
            totalTime = 5;
        }
    }

}

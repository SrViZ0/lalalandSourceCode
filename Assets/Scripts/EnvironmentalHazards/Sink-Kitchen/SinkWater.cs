using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SinkWater : MonoBehaviour
{
    //public NewHealthSystem drown;

    public int hydroDamage = 10;

    public LayerMask pullableLayers;

    public float pullStrength = 50;

    public Collider[] targetObjects;
    public Transform sinkHolePoint;



    public void PullForce()
    {
        if (targetObjects != null && targetObjects.Length > 0)
        {
            foreach (Collider col in targetObjects)
            {
                if (col.GetComponent<Rigidbody>())
                {
                    col.GetComponent<Rigidbody>().velocity = (sinkHolePoint.position - col.transform.position) * pullStrength * Time.deltaTime;
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
        PullForce();
    }


}

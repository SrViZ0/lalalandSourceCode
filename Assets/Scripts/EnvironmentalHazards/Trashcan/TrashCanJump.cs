using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanJump : MonoBehaviour
{

    public LayerMask pullableLayers;

    public float pushStrength = 5;

    public Collider[] targetObjects;
    
    public float toastTime;
    public float ejectTime;

    public bool canThrow = true;

    public void ThrowForce()
    {
        if (targetObjects != null && targetObjects.Length > 0)
        {
            foreach (Collider col in targetObjects)
            {
                if (col.GetComponent<Rigidbody>())
                {
                    col.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                    col.GetComponent<Rigidbody>().AddForce(Vector3.up * pushStrength, ForceMode.Impulse);
                }
            }
        }
    }

    private void GetINSIDE()
    {
        targetObjects = null;
        targetObjects = Physics.OverlapBox(gameObject.transform.position, new Vector3(gameObject.transform.localScale.x / 3, gameObject.transform.localScale.y / 3, gameObject.transform.localScale.z / 3), Quaternion.identity, pullableLayers);


    }

    private void OnTriggerStay(Collider other)
    {
        if (canThrow)
        {
            GetINSIDE();
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slippery : MonoBehaviour
{
    private Rigidbody targetObject;
    private Vector3 intertia;
    public float slipFactor;

    private void Start()
    {
        intertia = Vector3.one;
    }
    private void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        targetObject = other.gameObject.GetComponent<Rigidbody>();

        if (targetObject.velocity == Vector3.zero)
        {

        }
        else
        {

            targetObject.AddForce(new Vector3(targetObject.velocity.x, targetObject.velocity.y * 0, targetObject.velocity.z) * slipFactor);
            intertia = targetObject.velocity;
        }

        Debug.Log(targetObject.name + " Inti Velo:" + targetObject.velocity + "\n Final velo: " + targetObject.velocity * 10);


    }
}

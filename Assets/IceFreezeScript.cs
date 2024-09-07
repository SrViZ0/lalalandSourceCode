using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFreezeScript : MonoBehaviour
{
    public float slipFactor;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            Rigidbody rb =  other.GetComponent<Rigidbody>();
            rb.AddRelativeForce(new Vector3(rb.velocity.x, rb.velocity.y * 0, rb.velocity.z) * slipFactor);
            Debug.Log(rb.gameObject.name + rb.velocity + " Accel!!");
        }
    }
}

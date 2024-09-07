using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceApply : MonoBehaviour
{
    public float playerForce;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MovementScript.BounceForce = playerForce;
            MovementScript.canSlam = false;
            MovementScript.canBounce = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MovementScript.canBounce = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPivotDetection : MonoBehaviour
{
    public NewInputManager newinputManager;
    public bool isColliding;
    void Start()
    {
        newinputManager = FindObjectOfType<NewInputManager>();
    }

    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            newinputManager.aim_Input = false;
            isColliding = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isColliding = false;
        }
    }
}

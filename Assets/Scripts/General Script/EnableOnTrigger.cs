using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnTrigger : MonoBehaviour
{
    public GameObject objectToEnable;
    private void OnTriggerEnter(Collider other)
    {
        objectToEnable.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroyObject : MonoBehaviour
{
    public GameObject objToDestroy;
    public float destroyTime;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 1f);
    }
    private void OnDestroy()
    {
        Destroy(objToDestroy, destroyTime);
    }
}

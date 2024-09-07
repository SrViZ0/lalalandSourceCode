using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObject : MonoBehaviour
{
    private Rigidbody rb;
    public bool isCollected;
    public string collectPoint;
    public Transform collectedPos;

    public GameObject frontPart;
    public GameObject midPart;
    public GameObject backPart;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isCollected)
        {
            transform.rotation = collectedPos.rotation;
            frontPart.GetComponent<PathCreation.Examples.PathFollower>().enabled = true;
            midPart.GetComponent<PathCreation.Examples.PathFollower>().enabled = true;
            backPart.GetComponent<PathCreation.Examples.PathFollower>().enabled = true;
            Destroy(rb);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == collectPoint)
        {
            isCollected = true;
            other.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombCollectible : MonoBehaviour
{
    private Rigidbody rb;
    private CombCollection cc;

    public string combColPos;


    public bool isCollected;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == combColPos)
        {
            cc = GameObject.Find("CombCollectStep(Clone)").GetComponent<CombCollection>();
            gameObject.transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            Destroy(rb);
            Destroy(other.gameObject);
            isCollected = true;
            cc.ObjectiveCompeleteCheck();
        }
    }
}

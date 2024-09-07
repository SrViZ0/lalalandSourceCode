using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCollectible : MonoBehaviour
{
    private Rigidbody rb;
    private KnifeCollection kc;

    public string knifeColPos1;
    public string knifeColPos2;
    public string knifeColPos3;

    public bool isCollected;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == knifeColPos1 || other.gameObject.name == knifeColPos2 || other.gameObject.name == knifeColPos3)
        {
            kc = GameObject.Find("KnifeCollectedStep(Clone)").GetComponent<KnifeCollection>();
            gameObject.transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            Destroy(rb);
            Destroy(other.gameObject);
            isCollected = true;
            kc.ObjectiveCompeleteCheck();
        }
    }
}

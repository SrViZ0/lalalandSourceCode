using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawersPush : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float forceApplied;
    public float forceUp = 50;

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision!");

        //col.gameObject.GetComponent<Rigidbody>().AddForce(0, forceApplied, 0);
        col.gameObject.GetComponent<Rigidbody>().AddForce(-forceApplied, forceUp, 0);

    }


    //public void OnCollisionEnter(Collision c)
    //{

    //    float force = 500;


    //    if (c.gameObject.tag == "Player")
    //    {

    //        Vector3 dir = c.contacts[0].point - transform.position;

    //        dir = -dir.normalized;


    //        GetComponent<Rigidbody>().AddForce(dir * force);
    //    }
    //}
}

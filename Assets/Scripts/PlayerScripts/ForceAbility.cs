using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceAbility : MonoBehaviour
{
    public ExternalInteractions externalInteractions;

    public Camera cam;
    public LayerMask pullableLayers;

    public bool pullForce;
    //public ShootingScript shootingScript;

    public float pullStrength, pushStrength;
    public float pullRange;
    public float pullRadius, pushRadius;

    public Collider[] targetObjects;
    public Transform holdPosition, pushPosition;
    private void Start()
    {

    }
    private void Update()
    {
        //Button Down (Pull - Gather)
        if (Input.GetKeyDown(KeyCode.E))
        {
            pullForce = true;
            //shootingScript.enabled = false;
            GetPullObjects();
            Debug.Log("Pulling");
        }
        //Holding Button (Pulling)
        if (Input.GetKey(KeyCode.E) && pullForce && !Input.GetKey(KeyCode.Q))
        {
            PullForce();
        }
        //Release (Let it fall)
        if (Input.GetKeyUp(KeyCode.E))
        {
            pullForce = false;
            //ReleaseEnemies();
            //shootingScript.enabled = true;
        }
        //Throw (LMB - Down)
        if (Input.GetKey(KeyCode.E) && Input.GetButtonDown("Fire1"))
        {
            if (pullForce)
            {
                //Throw
                ThrowForce();
                pullForce = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //ReleaseEnemies();
            PushForce();
        }
    }
    public void PushForce()
    {
        GetPushObjects();
        ThrowForce();
        targetObjects = null;
    }
    public void ThrowForce()
    {
        if (targetObjects != null && targetObjects.Length > 0)
        {
            foreach (Collider col in targetObjects)
            {
                if (col.GetComponent<Rigidbody>())
                {
                    col.GetComponent<Rigidbody>().AddForce(cam.transform.TransformDirection(Vector3.forward) * pushStrength, ForceMode.Impulse);
                }
            }
        }
    }
    public void PullForce()
    {
        if (targetObjects != null && targetObjects.Length > 0)
        {
            foreach (Collider col in targetObjects)
            {
                if (col.gameObject.tag != "Enemy")
                {
                    if (col.GetComponent<Rigidbody>())
                    {
                        col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        col.gameObject.GetComponent<Rigidbody>().velocity = (holdPosition.position - col.transform.position) * pullStrength * Time.deltaTime;
                    }
                }
                else if (col.gameObject.GetComponent<GruntBehave>())
                {
                    Debug.Log("Enemy Grabbed");
                    col.gameObject.GetComponent<GruntBehave>().isGrabbed = true;
                    col.gameObject.GetComponent<GruntBehave>().hasFallen = false;
                    col.gameObject.GetComponent<GruntBehave>().canStand = false;
                    col.gameObject.GetComponent<Rigidbody>().velocity = (holdPosition.position - col.transform.position) * pullStrength * Time.deltaTime;
                }
                else if (col.gameObject.GetComponent<TankBehave>())
                {
                    Debug.Log("Enemy Grabbed");
                    col.gameObject.GetComponent<TankBehave>().isGrabbed = true;
                    col.gameObject.GetComponent<TankBehave>().hasFallen = false;
                    col.gameObject.GetComponent<TankBehave>().canStand = false;
                    col.gameObject.GetComponent<Rigidbody>().velocity = (holdPosition.position - col.transform.position) * pullStrength * Time.deltaTime;
                }
            }
        }
    }

    //public void ReleaseEnemies()
    //{
    //    if (targetObjects != null && targetObjects.Length > 0)
    //    {
    //        foreach (Collider col in targetObjects)
    //        {
    //            if (col.gameObject.GetComponent<GruntBehave>())
    //            {
    //                Debug.Log("Enemy Grabbed");
    //                col.gameObject.GetComponent<GruntBehave>().isGrabbed = false;
    //                col.gameObject.GetComponent<Rigidbody>().velocity = (holdPosition.position - col.transform.position) * pullStrength * Time.deltaTime;
    //            }
    //            else if (col.gameObject.GetComponent<TankBehave>())
    //            {
    //                Debug.Log("Enemy Grabbed");
    //                col.gameObject.GetComponent<TankBehave>().isGrabbed = false;
    //                col.gameObject.GetComponent<Rigidbody>().velocity = (holdPosition.position - col.transform.position) * pullStrength * Time.deltaTime;
    //            }
    //        }
    //    }
    //}

    public void GetPullObjects()
    {
        targetObjects = null;
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, pullRange, pullableLayers))
        {
            targetObjects = Physics.OverlapSphere(hit.point, pullRadius, pullableLayers);
        }
    }
    public void GetPushObjects()
    {
        targetObjects = null;
        targetObjects = Physics.OverlapSphere(pushPosition.position, pushRadius, pullableLayers);
    }


}

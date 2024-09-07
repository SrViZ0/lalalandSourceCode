using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumForce : DamageScript
{

    public LayerMask pullableLayers;

    public bool pullForce;
    public bool vacuumPower = false;
    //public ShootingScript shootingScript;

    public float pullStrength;
    public float pullRange;
    public float pullRadius;

    public Collider[] targetObjects;
    public Transform pullArea, mouth;

    
    // Update is called once per frame
    void Update()
    {

        if (vacuumPower)
        {
            GetPullObjects();
            PullForce();
        }

    }

    public void PullForce()
    {
        if (targetObjects != null && targetObjects.Length > 0)
        {
            foreach (Collider col in targetObjects)
            {
                if (col.GetComponent<Rigidbody>())
                {
                    col.GetComponent<Rigidbody>().velocity = (mouth.position - col.transform.position) * pullStrength * Time.deltaTime;
                }
            }
        }
    }
    public void GetPullObjects()
    {
        targetObjects = null;
        
        targetObjects = Physics.OverlapSphere(pullArea.position, pullRadius, pullableLayers);
       
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pullArea.position, pullRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Enemy" && vacuumPower)
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Enemy" && vacuumPower)
        {
            DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject); //Call this to deal dmg 
        }
    }
}

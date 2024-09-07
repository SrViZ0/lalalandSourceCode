using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterCook : DamageScript
{
    public ToasterActivator toasterActivator;
    public LayerMask pullableLayers;

    public float pushStrength = 10;

    public Collider[] targetObjects;
    public bool isCooking = false;
    public float toastTime;
    public float ejectTime;


    



    public void ThrowForce()
    {
        if (targetObjects != null && targetObjects.Length > 0)
        {
            foreach (Collider col in targetObjects)
            {
                if (col.GetComponent<Rigidbody>() && col.gameObject.tag != "Ammo")
                {
                    col.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                    col.GetComponent<Rigidbody>().AddForce(Vector3.up * pushStrength, ForceMode.Impulse);
                }
            }
        }
    }

    private void GetINSIDE()
    {
        targetObjects = null;
        targetObjects = Physics.OverlapBox(gameObject.transform.position, new Vector3(gameObject.transform.localScale.x/ 2, gameObject.transform.localScale.y / 2, gameObject.transform.localScale.z/ 2), Quaternion.identity, pullableLayers);
    }

    private void OnTriggerStay(Collider other)
    {
        GetINSIDE();

        if (isCooking)
        {

            if (other.gameObject.tag == "Enemy")
            {
                DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject);

            }



        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlushDestroy :  DamageScript
{
    public bool flushIsOn = false;

    private void OnTriggerEnter(Collider other)
    {

        if (flushIsOn && other.gameObject.tag == "Enemy")
        {
            DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject);
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && flushIsOn)
        {
            other.GetComponent<NewHealthSystem>().TakeDamage(100);
        }
    }
}

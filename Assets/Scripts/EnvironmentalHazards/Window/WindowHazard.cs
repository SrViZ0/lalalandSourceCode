using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowHazard : DamageScript
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject); //Call this to deal dmg 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<NewHealthSystem>().TakeDamage(100);
        }
    }
}

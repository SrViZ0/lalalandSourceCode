using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingFanHazard : DamageScript
{
    public NewHealthSystem fanDmg;

    public int fanDamage = 10;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fanDmg.TakeDamage(fanDamage);
            Debug.Log("Dieded");
        }

        if (other.gameObject.tag == ("Enemy"))
        {
            DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject); //Call this to deal dmg 
            Debug.Log("Dieded");
        }

    }
}

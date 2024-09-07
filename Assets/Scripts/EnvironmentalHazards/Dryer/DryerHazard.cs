using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryerHazard : DamageScript
{
    //public NewHealthSystem dmg;

    //public int Damage = 2;
    public bool On = false;

    //public bool hazard = false;

    private void OnTriggerStay(Collider other)
    {
        if (On)
        {
            if (other.gameObject.tag == "Enemy")
            {
                DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject);

            }

            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<NewHealthSystem>().TakeDamage(100);
            }
        }


    }



}

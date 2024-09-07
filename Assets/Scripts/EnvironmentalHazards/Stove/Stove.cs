using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : DamageScript
{
    public bool stoveFire = false;

    public NewHealthSystem Dmg;

    public int burnDamage = 2;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stoveFire)
        {
            // When target is hit
            if (other.gameObject.tag == "Enemy")
            {
                DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject);

            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (stoveFire)
        {

            if (other.gameObject.tag == "Player" && stoveFire)
            {
                Dmg.TakeDamage(burnDamage);

            }
        }


    }

}

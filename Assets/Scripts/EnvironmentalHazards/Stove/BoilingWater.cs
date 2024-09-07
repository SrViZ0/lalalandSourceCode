using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilingWater : DamageScript
{
    public NewHealthSystem BoilingDmg;

    public int boilDamage = 2;
    public bool isBoiling = false;

    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && isBoiling)
        {
            BoilingDmg.TakeDamage(boilDamage);

          
        }

        if (other.gameObject.tag == "Enemy" && isBoiling)
        {
            DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject);

        }
    }


}

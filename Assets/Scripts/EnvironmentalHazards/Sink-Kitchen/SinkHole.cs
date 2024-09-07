using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkHole : DamageScript
{
    public NewHealthSystem BoilingDmg;

    public int boilDamage = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            BoilingDmg.TakeDamage(boilDamage);


        }

        if (other.gameObject.tag == "Enemy")
        {
            DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject);

        }
    }


}

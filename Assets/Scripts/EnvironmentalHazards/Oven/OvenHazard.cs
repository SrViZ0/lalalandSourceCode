using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenHazard : DamageScript
{
    public PlayerMovement playerMovement;
    public NewHealthSystem FrostingDmg;

    public int frostDamage = 3;
    public bool isFrosting = true;
    public float totalTime = 2;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && isFrosting)
        {

            if (totalTime > 0)
            {
                // Subtract elapsed time every frame
                totalTime -= 1 * Time.deltaTime;
            }
            else
            {
                totalTime = 0;

            }

            if (totalTime == 0)
            {
                FrostingDmg.TakeDamage(frostDamage);
                totalTime = 3;
            }

        }

        if (other.gameObject.tag == "Enemy" && isFrosting)
        {
            DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject);

        }
    }
}

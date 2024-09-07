using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeHazard : DamageScript
{
    public PlayerMovement playerMovement;
    public NewHealthSystem FrostingDmg;

    public int frostDamage = 1;
    public bool isFrosting = true;
    public float totalTime = 3;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && isFrosting)
        {

            playerMovement.walkSpeed = 0.5f;
            playerMovement.sprintSpeed = 2;

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

        

        //playerMovement.jumpForce = 2.5f;
        //playerMovement.jumpCooldown = 0.4f;

        if (other.gameObject.tag == "Enemy")
        {
            DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject); //Call this to deal dmg 
            StunTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject); // Call this if you want to stun  


        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerMovement.walkSpeed = 3;
        playerMovement.sprintSpeed = 5;

        //playerMovement.jumpForce = 8.5f;
        //playerMovement.jumpCooldown = 0.25f;

    }

}

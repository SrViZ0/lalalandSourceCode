using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewHealthSystem : MonoBehaviour
{
    NewInputManager newinputManager;
    public NewUiManager newuserinterface;


    [Header("Health Variables")]
    public int playerHealth;
    public int maxHealth;
    public int currentHealth;
    public int enemyDamage;
    public float timeTrack;
    public float hitInterval;
    public bool invincible;

    [Header("Regeneration Variables")]
    public bool damageTaken;
    public float RtimeTrack;
    public float regenTime;
    public bool startReg;
    public float HtimeTrack;
    public float healRate;

    void Start()
    {
        playerHealth = maxHealth;
        newinputManager = GetComponent<NewInputManager>();
    }
    private void Update() 
    {
        if (playerHealth <= 0)
        {
            newuserinterface.EndScreen();
            Cursor.lockState = CursorLockMode.None;
        }
        if (invincible)
        {
            timeTrack += Time.deltaTime;
            if (timeTrack > hitInterval)
            {
                invincible = false;
                timeTrack = 0f;
            }
        }
        RegenHealth();
    }
    //public void HandleShooting()
    //{
    //    rubberSource.PlayOneShot(rubberSfx);
    //    Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);
    //}

    public void CheckDamage()
    {
        if (playerHealth != currentHealth)
        {
            Debug.Log("Health Changed");
            damageTaken = true;
            startReg = false;
            RtimeTrack = 0f;
            currentHealth = playerHealth;
        }
    }
    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            playerHealth -= damage;
            invincible= true;
            timeTrack += Time.deltaTime;
            CheckDamage();
        }
    }
    public void RegenHealth()
    {
        if (damageTaken)
        {
            RtimeTrack += Time.deltaTime;
            if (RtimeTrack > regenTime)
            {
                damageTaken = false;
                startReg = true;
                RtimeTrack = 0f;
            }
        }
        if (playerHealth == maxHealth)
        {
            startReg = false;
        }
        else if (startReg)
        {
            if (playerHealth < maxHealth)
            {
                HtimeTrack += Time.deltaTime;
                if (HtimeTrack > healRate)
                {
                    playerHealth += 1;
                    HtimeTrack = 0f;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage(enemyDamage);
        }

        
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        invincible = false;
    //        timeTrack = 0f;
    //    }
    //}


}

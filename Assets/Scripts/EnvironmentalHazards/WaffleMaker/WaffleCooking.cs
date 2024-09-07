using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleCooking : DamageScript
{

    public bool isCooking = false;

    public WaffleActivator enemyPresent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (enemyPresent = true)
        //{
        //    Debug.Log("Enemy Waffling");
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (isCooking)
        {
            if (other.gameObject.tag == "Enemy")
            {
                DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject);
                
                //Destroy(other);
            }



        }
    }

}

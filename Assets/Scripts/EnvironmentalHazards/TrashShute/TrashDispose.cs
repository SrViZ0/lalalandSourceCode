using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDispose : DamageScript
{
    public bool throwAway = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (throwAway)
        {
            if (other.gameObject.tag == "Enemy")
            {
                DamageTarget(other.gameObject.GetComponent<DeathChecker>(), other.gameObject, this.gameObject);
                Debug.Log("YUM YUM YUM");
            }
            if (other.gameObject.tag == "Ammo")
            {
                Destroy(other.gameObject);
            }
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<NewHealthSystem>().TakeDamage(100);
            }
        }
    }
}

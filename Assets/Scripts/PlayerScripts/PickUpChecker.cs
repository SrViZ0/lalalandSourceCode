using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            if (AmmoChecker.ammoCount < AmmoChecker.maxAmmo)
            {
                AmmoChecker.ammoCount += 1;
                Destroy(other.gameObject);
            }
            else if (AmmoChecker.ammoCount >= AmmoChecker.maxAmmo)
            {
                AmmoChecker.ammoCount = AmmoChecker.maxAmmo;
            }
        }
    }
}

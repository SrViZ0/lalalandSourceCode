using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTimer : MonoBehaviour
{
    WeaponSwitching weaponSwitching;
    TestGrapple testGrapple;
    public GameObject activeWeap;
    public GameObject invSlots;
    public GameObject chargeUI;


    public Image uiFill;
    void Start()
    {
        invSlots = GameObject.Find("InvSlots");
        weaponSwitching = invSlots.GetComponent<WeaponSwitching>();
        chargeUI = GameObject.Find("Backfill");
    }

    // Update is called once per frame
    void Update()
    {


        activeWeap = invSlots.transform.GetChild(weaponSwitching.selectedWeapon).GetChild(0).gameObject;

        if (activeWeap.name == "GrappleHook(Clone)(Clone)")
        {
            chargeUI.SetActive(true);
            testGrapple = activeWeap.transform.GetChild(0).GetComponent<TestGrapple>();
            uiFill.fillAmount = testGrapple.valToTrack;

        }
        else if (uiFill.fillAmount == 0 || activeWeap.name != "GrappleHook(Clone)(Clone)")
        { 
            chargeUI.SetActive(false);
            //Debug.Log("false");
        }

    }
}

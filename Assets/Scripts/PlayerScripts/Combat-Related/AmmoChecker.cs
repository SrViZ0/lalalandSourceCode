using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoChecker : MonoBehaviour
{
    public static int maxAmmo;
    public int ammoTracked;

    public TextMeshProUGUI ammoUI;

    public static int ammoCount;
    private void Start()
    {
        maxAmmo = ammoTracked;
        ammoCount = ammoTracked;
    }
    void Update()
    {
        ammoTracked = ammoCount;
        ammoUI.text = "Ammo: " + ammoCount + " / " + maxAmmo;
    }
}

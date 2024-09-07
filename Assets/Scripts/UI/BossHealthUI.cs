using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] BossHealthManager bhm;

    [SerializeField] RectMask2D healthMask;

    List<Image> shield = new List<Image>();

    int lastSP;
    int maxSP;//Maybe i'll use this? idk

    private void OnEnable()
    {
        //GameEventsManager.instance.enemyEvent.onShieldUpdate += UpdateShieldHUD; //4gor i got nothing to call this evnt
        bhm = MonoBehaviour.FindAnyObjectByType<BossHealthManager>();

        maxSP = bhm.shieldPoints;
        foreach (Transform child in this.transform)
        {
            shield.Add(child.GetChild(0).GetComponent<Image>());
        }
    }

    private void OnDisable()
    {
        //GameEventsManager.instance.enemyEvent.onShieldUpdate -= UpdateShieldHUD;
    }

    private void UpdateShieldHUD()
    {
        if (bhm.shieldPoints > 0)
        {
            shield[bhm.shieldPoints].enabled = false;
        }
    }

    private void Update()
    {
        if (bhm.shieldPoints < lastSP) 
        { 
            UpdateShieldHUD();
        }
        lastSP = bhm.shieldPoints;

        healthMask.padding = new Vector4(0,0, 657* (bhm.maxHealth - bhm.health)/bhm.maxHealth ,0); //(x,y,z,w) 657 is max;
    }


}

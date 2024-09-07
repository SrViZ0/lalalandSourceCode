using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementHUBManager : MonoBehaviour
{
    //[SerializeField] private QuestInfo questInfo;
    //private Quest quest;
    //private TextMeshProUGUI textDisplay;
    //[SerializeField] private QuestSystemMaster qsm;
    //[SerializeField] private Image compIconSlot;
    //[SerializeField] private Texture2D finish, unfinished;

    [Header("Achievment Parents")]
    public GameObject bedroomAch;
    public GameObject livingRoomAch;
    public GameObject kitchenAch;
    public GameObject toiletAch;

    [Header("Misc Parents")]
    public GameObject weaponsAch;
    public GameObject cosmeticsAch;

    [Header("Weapon Information Parents")]
    [SerializeField] private ItemInfoSO itemInfo1;
    [SerializeField] private ItemInfoSO itemInfo2;
    [SerializeField] private ItemInfoSO itemInfo3;
    [SerializeField] private ItemInfoSO itemInfo4;
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    void Start()
    {
        ShowGeneral();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HideAll()
    {
        bedroomAch.SetActive(false);
        livingRoomAch.SetActive(false);
        kitchenAch.SetActive(false);
        toiletAch.SetActive(false);
        weaponsAch.SetActive(false);
        cosmeticsAch.SetActive(false);
    }

    public void ShowGeneral()
    {
        bedroomAch.SetActive(true);
        livingRoomAch.SetActive(true);
        kitchenAch.SetActive(true);
        toiletAch.SetActive(true);
        weaponsAch.SetActive(false);
        cosmeticsAch.SetActive(false);
        HideWeaponInfo();
    }

    public void ShowBedroom()
    {
        bedroomAch.SetActive(true);
        livingRoomAch.SetActive(false);
        kitchenAch.SetActive(false);
        toiletAch.SetActive(false);
        weaponsAch.SetActive(false);
        cosmeticsAch.SetActive(false);
        HideWeaponInfo();
    }
    public void ShowLivingRoom()
    {
        bedroomAch.SetActive(false);
        livingRoomAch.SetActive(true);
        kitchenAch.SetActive(false);
        toiletAch.SetActive(false);
        weaponsAch.SetActive(false);
        cosmeticsAch.SetActive(false);
        HideWeaponInfo();
    }
    public void ShowKitchen()
    {
        bedroomAch.SetActive(false);
        livingRoomAch.SetActive(false);
        kitchenAch.SetActive(true);
        toiletAch.SetActive(false);
        weaponsAch.SetActive(false);
        cosmeticsAch.SetActive(false);
        HideWeaponInfo();
    }
    public void ShowToilet()
    {
        bedroomAch.SetActive(false);
        livingRoomAch.SetActive(false);
        kitchenAch.SetActive(false);
        toiletAch.SetActive(true);
        weaponsAch.SetActive(false);
        cosmeticsAch.SetActive(false);
        HideWeaponInfo();
    }
    public void ShowWeapons()
    {
        bedroomAch.SetActive(false);
        livingRoomAch.SetActive(false);
        kitchenAch.SetActive(false);
        toiletAch.SetActive(false);
        weaponsAch.SetActive(true);
        cosmeticsAch.SetActive(false);
        HideWeaponInfo();
    }
    public void ShowCosmetics()
    { 
        bedroomAch.SetActive(false);
        livingRoomAch.SetActive(false);
        kitchenAch.SetActive(false);
        toiletAch.SetActive(false);
        weaponsAch.SetActive(false);
        cosmeticsAch.SetActive(true);
        GameEventsManager.instance.pointsEvent.UnlockSkins();
        HideWeaponInfo();
    }

    public void HideWeaponInfo()
    {
        weapon1.SetActive(false);
        weapon2.SetActive(false);
        weapon3.SetActive(false);
    }
    
    public void ShowWeapon1()
    {
        if (itemInfo1.unlocked)
        {
            HideAll();
            weapon1.SetActive(true);
        }
    }
    public void ShowWeapon2()
    {
        if (itemInfo2.unlocked)
        {
            HideAll();
            weapon2.SetActive(true);
        }
    }
    public void ShowWeapon3()
    {
        if (itemInfo3.unlocked)
        {
            HideAll();
            weapon3.SetActive(true);
        }
    }
    public void ShowWeapon4()
    {
        if (itemInfo4.unlocked)
        {
            HideAll();
            weapon4.SetActive(true);
        }
    }
}

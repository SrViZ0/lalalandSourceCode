using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[RequireComponent(typeof(HubSlot))]
[Serializable] public class SkinList : SerializableDictionary<int, GameObject> { }
public class SkinUnlock : MonoBehaviour
{
    public SkinList skinsToUnlock;
    HubSlot currentHubSlot;
    //SkinSlot currentSkinSlot;
    PointsSystemMaster pointManager;
    bool weapon;
    // Start is called before the first frame update

    private void OnEnable()
    {
        GameEventsManager.instance.pointsEvent.onUnlockSkin += CheckSkinUnlock;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.pointsEvent.onUnlockSkin -= CheckSkinUnlock;
    }
    void Start()
    {
        currentHubSlot = this.GetComponent<HubSlot>();
        if (currentHubSlot is not null) 
        {
            weapon = true;
        }
        else
        {
            //currentSkinSlot = this.GetComponent<SkinSlot>();
            weapon = false;
        }

        pointManager = GameObject.Find("PointsManager").GetComponent<PointsSystemMaster>();

    }

    public void CheckSkinUnlock() // Todo - Run this every time interact with Hub menu through event
    {
        foreach ( int scoreReq in skinsToUnlock.Keys )
        { 
            if (pointManager.points >= scoreReq)//Check if score is higher than req
            {
                Debug.Log(gameObject.name + "MonkeyBalls");
                if (weapon) UnlockWeaponSkin(scoreReq); else UnlockCharaSkin(scoreReq);
            }
        }
    }

    void UnlockWeaponSkin(int scoreReq)
    {
        foreach (GameObject skin in currentHubSlot.itemInfoSO.skins) //Check for dupe skins
        {
            if (skin == skinsToUnlock[scoreReq])
            {
                return; //exit if dupe
            }
            //Otheriwise run following block
            //Check if item is unlokced
            if (!currentHubSlot.itemInfoSO.unlocked) return;

            currentHubSlot.itemInfoSO.skins.Add(skinsToUnlock[scoreReq]);

            UnlockablesItemMaster itemMaster = GameObject.Find("ItemMaster").GetComponent<UnlockablesItemMaster>();
            StartCoroutine(itemMaster.LoadSkins(itemMaster.GetItemByID(currentHubSlot.itemInfoSO.id), this.gameObject)); //Im gunna mincraft muself. da fak is this shit even??? 
                                                                                                                         //RunLoadskin Corutine ig???
        }
    }
    void UnlockCharaSkin(int scoreReq)
    {
        skinsToUnlock[scoreReq].GetComponent<SkinSlot>().characterSkinInfo.UnlockSkin(true);
    }
}

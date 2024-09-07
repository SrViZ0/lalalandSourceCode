using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using TMPro;

public class HubSlot : MonoBehaviour
{
    public ItemInfoSO itemInfoSO;
    public ExternalInteractions externalInteractions;
    public TextMeshProUGUI equipText;
    [SerializeField] private bool selected;
    private void Start() // cannot use awake othersie idk, unsubbed events or sm shit
    {
        //extInter = GameObject.FindGameObjectWithTag("Player").GetComponent<ExternalInteractions>();
        externalInteractions = GameObject.FindGameObjectWithTag("Player").GetComponent<ExternalInteractions>();
        InstentiateOnStart();
    }

    void Update()
    {
        if (externalInteractions.isInteracting && externalInteractions.interactPressed &&!uiManager.isPausing && selected) 
        {
            ButtonLeft();
            ButtonRight();
        }
    }
    public void ButtonLeft()
    {
        SkinLeft(CalculateIndex().Item1, CalculateIndex().Item2);
    }
    public void ButtonRight()
    {
        SkinRight(CalculateIndex().Item1, CalculateIndex().Item2);
    }
    private void InstentiateOnStart()
    {
        Instantiate(itemInfoSO.lockedModelPrefab, this.transform.position, this.transform.rotation, this.transform);
        if (itemInfoSO.unlocked)
        {
            GameEventsManager.instance.itemEvents.UnlockItem(itemInfoSO.id);
        }
    }

    private void UpdateSkin()
    {
        foreach(Transform child in this.transform)
        {
            if(child.gameObject == itemInfoSO.activeModel)
            {
                child.gameObject.SetActive(true);
            }
            else { child.gameObject.SetActive(false); } 
        }

        Transform invSlots = GameObject.Find("InvSlots").transform;
        foreach (Transform child in invSlots)
        {
            child.GetComponent<WeaponScript>().LoadModel();
        }
    }

    private GameObject GetActiveChild()
    {
        GameObject temp = null;
        foreach (Transform skin in this.transform)
        {
            if (skin.gameObject.activeInHierarchy == true)
            {
                temp = skin.gameObject;
            }
        }
        return temp;
    }

    private int GetActiveChildIndex()
    {
        int activeChildIndex = 0;
        GameObject activeChild = GetActiveChild();
        if (activeChild != null) { activeChildIndex = activeChild.transform.GetSiblingIndex();}
        return activeChildIndex;
    }

    private (int , int ) CalculateIndex()
    {
        int index = GetActiveChildIndex();
        int prevIndx = index - 1;
        int nextIndx = index + 1;

        if ( prevIndx < 0 )     
        { 
            prevIndx = itemInfoSO.skins.Count-1;
        }
        if (nextIndx > itemInfoSO.skins.Count-1) 
        { 
            nextIndx = 0;
        }

        return (prevIndx, nextIndx);
    }

    public void SkinLeft(int nextIndx, int prevIndx)
    {
        itemInfoSO.activeModel = this.transform.GetChild(nextIndx).gameObject;
        UpdateSkin();
    }
    public void SkinRight(int nextIndx, int prevIndx)
    {
        itemInfoSO.activeModel = this.transform.GetChild(prevIndx).gameObject;
        UpdateSkin();
    }

    private bool NullCheck()
    {
        int index = GetActiveChildIndex();
        int prevIndx = index - 1;
        int nextIndx = index + 1;

        if (nextIndx >= this.transform.childCount && prevIndx < 0)
        {
            return true;
        }
        return false;
    }

    public void EquipItem() 
    {
        if (!itemInfoSO.unlocked)
        {
            //TODO - play some kind of sfx/vfx here.
            return;
        }
        Transform invSlots = GameObject.Find("InvSlots").transform;
        foreach (Transform child in invSlots)
        {
            if (child.gameObject.activeInHierarchy && !Check4Dupes(invSlots))
            {
                //Debug.Log(child.gameObject.GetComponent<WeaponScript>().ItemInfo);

                child.GetComponent<WeaponScript>().ItemInfo = itemInfoSO;

                child.GetComponent<WeaponScript>().LoadModel(); //TODO - find a better way to do this

                equipText.text = "Equipped";

                CheckIfActive();
                //Debug.Log(child.gameObject.GetComponent<WeaponScript>().ItemInfo + " Equipped");
            }
        }

    }

    public void CheckIfActive()
    {
        //Transform invSlots = GameObject.Find("InvSlots").transform;
        //foreach (Transform child in invSlots)
        //{
        //    if (child.GetComponent<WeaponScript>().ItemInfo != itemInfoSO)
        //    {
        //        equipText.text = "Equip";
        //    }
        //}
    }

    private bool Check4Dupes(Transform invSlots)
    {
        foreach (Transform child in invSlots)
        {
            if (child.gameObject.GetComponent<WeaponScript>().ItemInfo == itemInfoSO)
            {
                Debug.LogWarning("Item already equipped you fuckin clown. Honk Honk");
                equipText.text = "Equipped";
                return true;
            }
        }

        return false;
    }
}

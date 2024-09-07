using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public ItemInfoSO info;

    private GameObject[] skinList;
    private GameObject activeModel;
    private bool unlocked;
    private bool equipped;

    public Item(ItemInfoSO itemInfo)
    {
        this.info = itemInfo;
        this.activeModel = new GameObject();
        unlocked = false;
        equipped = false;
        this.skinList = new GameObject[info.skins.Count];
    }

    public Item(ItemInfoSO itemInfo, bool unlocked, bool equipped, GameObject activeModel, GameObject[] skinList)
    {
        this.info=itemInfo;
        this.unlocked = unlocked;
        this.equipped = equipped;
        this.activeModel = activeModel;
        this.skinList = skinList;


    }
    public void GetItemData()
    {
        /*        return new iteminfoso(iteminfoso(unlocked));
        *///todo -return all so values  //uncomment later
    }
}

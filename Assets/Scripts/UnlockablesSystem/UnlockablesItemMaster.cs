using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UnlockablesItemMaster : MonoBehaviour
{


    private Dictionary<string, Item> itemMap;

    [Header("Config")]
    public UnlockablesCollection itemPool;

    private void Awake()
    {
        itemMap = CreateItemMap();
        //itemId = GetComponent<ItemInfoSO>();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.itemEvents.onItemUnlock += UnlockWeapon;
        GameEventsManager.instance.itemEvents.onItemUnlock += ApplySkin;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.itemEvents.onItemUnlock -= UnlockWeapon;
        GameEventsManager.instance.itemEvents.onItemUnlock -= ApplySkin;

    }

    private void UnlockWeapon(string id)
    {
        Item item = GetItemByID(id);
        GameObject hubSlot = GameObject.Find(item.info.hubSlot.gameObject.ToSafeString());


        //Debug.Log(item.info.activeModel + " 1 " + hubSlot.transform.GetChild(0).gameObject);

        StartCoroutine(LoadSkins(item,hubSlot));

        //Debug.Log(item.info.activeModel + " 2 " + hubSlot.transform.GetChild(1).gameObject);

        //Debug.Log(item.info.activeModel+ " " + hubSlot.transform.GetChild(1).gameObject);


        item.info.unlocked = true;
    }

    public IEnumerator LoadSkins(Item item, GameObject hubSlot)
    {
        Debug.Log(hubSlot.name + "Hubslot test");
        foreach (Transform child in hubSlot.transform)
        {
            Destroy(child.gameObject);
        }

        hubSlot.transform.DetachChildren(); //maybe this

        yield return null;

        foreach (GameObject skin in item.info.skins)
        {
            Instantiate(skin.gameObject, hubSlot.transform.position, hubSlot.transform.rotation, hubSlot.transform);
        }

        for (int i = 0; i < hubSlot.transform.childCount; i++)
        {
            hubSlot.transform.GetChild(i).gameObject.SetActive(false);
        }

        yield return null;

        item.info.activeModel = hubSlot.transform.GetChild(0).gameObject;

        item.info.activeModel.SetActive(true);
    }

    private void ApplySkin(string id)
    {
        Item item = GetItemByID(id);
        GameObject hubSlot = GameObject.Find(item.info.hubSlot.gameObject.ToSafeString());
        int index;
        foreach (Transform child in hubSlot.transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                index = hubSlot.transform.GetSiblingIndex();

                //Debug.Log("child Inactive: " + child.name);

                item.info.activeModel = child.gameObject;

            }
        }
    }


    private Dictionary<string,Item> CreateItemMap()
    {
        ItemInfoSO[] allItems = itemPool.itemList; //put everything in a array in a SO and load that the array into this array... man. 

        Dictionary<string, Item> idToItemMap = new Dictionary<string, Item>();

        // Create the quest map
        foreach (ItemInfoSO itemInfo in allItems)
        {
            if (idToItemMap.ContainsKey(itemInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + itemInfo.id);
            }
            //idToQuestMap.Add(itemInfo.id, LoadItem(questInfo)); TODO implement with item list system
            idToItemMap.Add(itemInfo.id, new Item(itemInfo));


        }
        return idToItemMap;
    }
    public Item GetItemByID(string id)
    {
        Item item = itemMap[id];

        if (item == null)
        {
            Debug.LogError("ID not found in the Quest Map: " + id);
        }

        return item;
    }



}

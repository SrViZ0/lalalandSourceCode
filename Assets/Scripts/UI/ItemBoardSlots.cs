using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoardSlots : MonoBehaviour
{
    public Transform stateObject;
    [SerializeField] private Image compIconSlot;

    [SerializeField] private ItemInfoSO itemInfo;
    private Item item;
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private UnlockablesItemMaster uim;
    [SerializeField] private Sprite finish, unfinished;
    void Awake()
    {
        Invoke(nameof(FindItemInfo), 0.5f);
        Debug.Log(itemInfo.id);
    }
    void FindItemInfo()
    {
        uim = GameObject.Find("ItemMaster").GetComponent<UnlockablesItemMaster>();
        item = uim.GetItemByID(itemInfo.id);
    }

    // Update is called once per frame
    void Update()
    {
        if (item != null)
        {
            textDisplay.text = itemInfo.displayName;
            if (itemInfo.unlocked == true)
            {
                compIconSlot.sprite = finish;
            }
            else
            {
                compIconSlot.sprite = unfinished;
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotUI : MonoBehaviour
{
    private GameObject invSlots;
    WeaponSwitching weaponSwitching;

    public GameObject slot1;
    public RectTransform rect1;
    public Color col1;
    Vector2 defaultPos1;


    public GameObject slot2;
    public RectTransform rect2;
    public Color col2;
    Vector2 defaultPos2;

    public GameObject slot3;
    public RectTransform rect3;
    public Color col3;
    Vector2 defaultPos3;

    void Start()
    {
        invSlots = GameObject.Find("InvSlots");
        weaponSwitching = invSlots.GetComponent<WeaponSwitching>();

        rect1 = slot1.GetComponent<RectTransform>();
        rect2 = slot2.GetComponent<RectTransform>();
        rect3 = slot3.GetComponent<RectTransform>();

        //col1 = slot1.GetComponent<Color>();
        //col2 = slot2.GetComponent<Color>();
        //col3 = slot3.GetComponent<Color>();

        defaultPos1 = rect1.transform.position;
        defaultPos2 = rect2.transform.position;
        defaultPos3 = rect3.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponSwitching.previousSelectedWeapon != weaponSwitching.selectedWeapon)
        {
            UpdateUI();
        }
    }
    void UpdateUI()
    {
        if (weaponSwitching.selectedWeapon == 0)
        {
            //col2.a = 0.6f;
            rect2.transform.position = defaultPos2;
            //col3.a = 0.6f;
            rect3.transform.position = defaultPos3;

            //col1.a = 1f;
            rect1.transform.position = new Vector2(rect1.transform.position.x, rect1.transform.position.y + 50f);
        }
        else if (weaponSwitching.selectedWeapon == 1)
        {
            //col1.a = 0.6f;
            rect1.transform.position = defaultPos1;
            //col3.a = 0.6f;
            rect3.transform.position = defaultPos3;

            //col2.a = 1f;
            rect2.transform.position = new Vector2(rect2.transform.position.x, rect2.transform.position.y + 50f);
        }
        else if (weaponSwitching.selectedWeapon == 2)
        {
            //col1.a = 0.6f;
            rect1.transform.position = defaultPos1;
            //col2.a = 0.6f;
            rect2.transform.position = defaultPos2;

            //col3.a = 1f;
            rect3.transform.position = new Vector2(rect3.transform.position.x, rect3.transform.position.y + 50f);
        }
    }
}
